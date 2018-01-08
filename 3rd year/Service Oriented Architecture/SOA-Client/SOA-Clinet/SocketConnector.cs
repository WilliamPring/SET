using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace SOA_Clinet
{
    class SocketConnector
    {
        private RegistryBuilder rBuilder;
        public List<QueryServiceResponse> qsr = new List<QueryServiceResponse>();
        public RootObject RootConfigObj { get; set; }

        public SocketConnector()
        {
            rBuilder = new RegistryBuilder();
            using (StreamReader reader = new StreamReader(@"SOAClinet.JSON"))
            {
                string json = reader.ReadToEnd();
                RootConfigObj = new JavaScriptSerializer().Deserialize<RootObject>(json);
            }
        }

        public bool StartService()
        {
            bool status = false;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse(RootConfigObj.clientSetting.IPAddress), Int32.Parse(RootConfigObj.clientSetting.RegisterPort));
            status = rBuilder.RegisterTeamName(tcpClient, "StevenAndFriends", this);
            if (!status)
            {
                return status;
            }


            return status;
        }

        public bool QuerryService(string tagName)
        {
            bool status = false;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse(RootConfigObj.clientSetting.IPAddress), Int32.Parse(RootConfigObj.clientSetting.RegisterPort));
            status = rBuilder.QueryServicesMessage(tcpClient, RootConfigObj.clientSetting.TeamName, RootConfigObj.clientSetting.TeamId, tagName, this);

            if (!status)
            {
                return status;
            }


            return status;
        }
        
        public void ExecuteServiceMessage()
        {
            bool status = false;
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse(RootConfigObj.ServiceToConsume.ipaddress), Int32.Parse(RootConfigObj.ServiceToConsume.port));
            status = rBuilder.ExecuteServicesMessage(tcpClient, RootConfigObj.clientSetting.TeamName, RootConfigObj.clientSetting.TeamId, RootConfigObj.ServiceToConsume.ServiceName, RootConfigObj.ServiceToConsume.argumentNumbers, qsr, this);



        }


        private void AcceptConnection(object argClientObj)
        {
            TcpClient tcpSClient = (TcpClient)argClientObj;

            using (NetworkStream nStream = tcpSClient.GetStream())
            {
                string recieved = "";

                // Recieve (blocking)
                byte[] bytesToRead = new byte[tcpSClient.ReceiveBufferSize];
                int bytesRead = nStream.Read(bytesToRead, 0, tcpSClient.ReceiveBufferSize);
                recieved = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

                // Parse the data
                Console.WriteLine(recieved);
                Console.ReadKey();
            }
        }


    }
}