using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SOA_Clinet
{
    class RegistryBuilder
    {
        public string ContactRegistry(TcpClient tcpClient, string sMessage)
        {
            string response = "";
            try
            {
                using (NetworkStream stream = tcpClient.GetStream())
                {
                    byte[] sData = ASCIIEncoding.ASCII.GetBytes(sMessage);

                    // Send
                    stream.Write(sData, 0, sData.Length);

                    // Recieve (blocking)
                    byte[] bytesToRead = new byte[tcpClient.ReceiveBufferSize];
                    int bytesRead = stream.Read(bytesToRead, 0, tcpClient.ReceiveBufferSize);
                    response = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);

                    // Print data, change status
                    Console.WriteLine(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }
        public bool RegisterTeamName(TcpClient tcpClient, string teamName, SocketConnector sConnObj)
        {
            string sMessage = "";
            sMessage += (char)11;
            sMessage += "DRC|REG-TEAM|||";
            sMessage += (char)13;
            sMessage += "INF|" + teamName + "|||";
            sMessage += (char)13;
            sMessage += (char)28;
            sMessage += (char)13;

            // Call
            string response = ContactRegistry(tcpClient, sMessage);

            // Parse the response output            
            return Parser.ParseOutputTeamRegister(response, sConnObj);
        }
        public bool QueryServicesMessage(TcpClient tcpClient, string teamName, string teamId, string tagName, SocketConnector sConnObj)
        {
            string sMessage = "";
            sMessage += (char)11;
            sMessage += "DRC|QUERY-SERVICE|"+ teamName + "|" + teamId+ "| ";
            sMessage += (char)13;
            sMessage += "SRV|" + tagName + "||||||";
            sMessage += (char)13;
            sMessage += (char)28;
            sMessage += (char)13;

            // Call
            string response = ContactRegistry(tcpClient, sMessage);

            // Parse the response output            
            return Parser.ParseOutputQueryServiceMessage(response, sConnObj);
        }
        public bool ExecuteServicesMessage(TcpClient tcpClient, string teamName, string teamId, string serviceName, string numsArgs, List<QueryServiceResponse> qsr, SocketConnector sConnObj)
        {
            string sMessage = "";
            sMessage += (char)11;
            sMessage += "DRC|EXEC-SERVICE|" + teamName + "|" + teamId + "| ";
            sMessage += (char)13;
            sMessage += "SRV||" + serviceName + "||"+ numsArgs+ "|||";
            sMessage += (char)13;
            foreach(QueryServiceResponse res in qsr)
            {
                if(res.type =="ARG")
                {
                    sMessage += "ARG|" + res.pos + "|" + res.name+ "|"+res.datatype + "||"+ res.value + "|";
                    sMessage += (char)13;
                }
            }

            sMessage += (char)28;
            sMessage += (char)13;

            // Call
            string response = ContactRegistry(tcpClient, sMessage);

            //// Parse the response output            
            return Parser.ParseOutputResultsServiceMessage(response, sConnObj);
        }

    }
}
