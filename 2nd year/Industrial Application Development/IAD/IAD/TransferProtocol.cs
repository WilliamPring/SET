using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IADtransfer
{
    class TransferProtocol
    {
        private const int timeout = 500;
        private const int defBuffer = 256;
        public const int UnknownState = 1;
        public const int ClientState = 2;
        public const int ServerState = 3;
        public const string newServerToConnectiMessage = "!newServerToConnectIs";
        public const string clientToServerUpgradeMessage = "!youAreNowServerFromDenys";

        private int port;
        private int portToRead;
        private string ipAddress;
        // 1 - unknown
        // 2 - client
        // 3 - server
        private int state;
        private Socket sockUdp;
        private Socket sockTcp;
        private Dictionary<int,Socket> dictionary;
        private int userCounter;
        private string serverIpAddress;


        public TransferProtocol()
        {
            this.dictionary = new Dictionary<int, Socket>();
            this.userCounter = 1;

            this.sockUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            this.sockUdp.ReceiveTimeout = TransferProtocol.timeout;

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach(IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    this.ipAddress = ip.ToString();
                    break;
                }                
            }

            this.port = 11045;
            this.portToRead = 11044;

            this.state = 1;
        }


        public int SendBroadcast()
        {
            int result = 1;
            bool gotMsg = false;

            IPAddress broadcastAddr = IPAddress.Parse(this.ipAddress.Substring(0, this.ipAddress.LastIndexOf('.')) + ".255");
            byte[] send = Encoding.ASCII.GetBytes("!pleaseTellMeYouAreServerFromDenys"); // 34 characters
            byte[] read = new byte[40];
            IPEndPoint ipEndPoint = new IPEndPoint(broadcastAddr, this.port);
            EndPoint endPoint = (EndPoint)new IPEndPoint(IPAddress.Any, this.portToRead);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.ReceiveTimeout = TransferProtocol.timeout;

            if (this.sockUdp.SendTo(send, ipEndPoint) != 0)
            {
                socket.Bind((EndPoint)new IPEndPoint(IPAddress.Parse(this.ipAddress), this.portToRead));
                for (int counter = 0; counter < 5;)
                {
                    try
                    {
                        //this.sockUdp.ReceiveFrom(read, 40, SocketFlags.None, ref endPoint);
                        socket.ReceiveFrom(read, 40, SocketFlags.None, ref endPoint);
                        if (Encoding.ASCII.GetString(read).Substring(0,20) == "!iAmServerFromNaween")
                        {
                            this.serverIpAddress = Encoding.ASCII.GetString(read).Substring(20).TrimEnd('\0');
                            gotMsg = true;
                            break;
                        }                 
                        else
                        {
                            counter++;
                        }       
                    }
                    catch (SocketException ex)
                    {
                        // timeout exception
                        if (ex.ErrorCode == 10060)
                        {
                            counter++;
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    this.sockUdp.SendTo(send, ipEndPoint);
                }

                if (gotMsg)
                {
                    result = 2;
                }
                else
                {
                    result = 3;
                }
            }
            else
            {
                result = -1;
            }

            socket.Close();

            return result;
        }


        public void StartAsClient()
        {
            this.state = 2;

            this.sockTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sockTcp.Connect(new IPEndPoint(IPAddress.Parse(this.serverIpAddress), this.port));
        }


        public void ClientSendTCP(string msg)
        {
            this.sockTcp.Send(Encoding.ASCII.GetBytes(msg));
        }



        public void ClientExit(string exitMsg)
        {
            this.sockTcp.Send(Encoding.ASCII.GetBytes(exitMsg));

            this.sockTcp.Close();
        }


        public string ClientReadTCP()
        {
            string result = "";

            byte[] bytes = new byte[TransferProtocol.defBuffer];
            try
            {
                this.sockTcp.Receive(bytes, TransferProtocol.defBuffer, SocketFlags.None);
                result = Encoding.ASCII.GetString(bytes).TrimEnd('\0');
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }            

            if(result == TransferProtocol.clientToServerUpgradeMessage)
            {
                this.ClientUpgrateToServer();                
            }
            else if (result.Length > 21)
            {
                if (result.Substring(0, 21) == TransferProtocol.newServerToConnectiMessage)
                {
                    this.ClientConnectToNewServer(result.Substring(21));
                    result = TransferProtocol.newServerToConnectiMessage;
                }
            }
            return result;
        }



        private void ClientUpgrateToServer()
        {
            this.sockTcp.Close();

            this.sockTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sockTcp.Bind(new IPEndPoint(IPAddress.Parse(this.ipAddress), this.port));
            this.sockTcp.Listen(10);
        }



        private void ClientConnectToNewServer(string newServerIpAddress)
        {
            this.sockTcp.Close();

            this.sockTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sockTcp.Connect(new IPEndPoint(IPAddress.Parse(newServerIpAddress), this.port));
        }



        public void StartAsServer()
        {
            this.state = 3;

            this.sockTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.sockTcp.Bind(new IPEndPoint(IPAddress.Parse(this.ipAddress), this.port));
            this.sockTcp.Listen(10);
        }
        
        
        
        public string ServerReadTCP(int clientID)
        {
            string result = "";

            if (this.dictionary.ContainsKey(clientID))
            {                
                byte[] bytes = new byte[TransferProtocol.defBuffer];
                this.dictionary[clientID].Receive(bytes, TransferProtocol.defBuffer, SocketFlags.None);
                result = Encoding.ASCII.GetString(bytes).TrimEnd('\0');
            }            

            return result;
        }  
        
        
        
        public void ServerSendTCP(string msg, int clientID)
        {
            if (this.dictionary.ContainsKey(clientID))
            {
                this.dictionary[clientID].Send(Encoding.ASCII.GetBytes(msg));
            }
        }


        public void ServerSendTCPAll(string msg)
        {
            for (int counter = 0; counter < this.dictionary.Keys.Count; counter++)
            {
                this.dictionary.Values.ElementAt<Socket>(counter).Send(Encoding.ASCII.GetBytes(msg));
            }
        }
              


        public int ServerAcceptOneClient()
        {
            int result = 0;

            Socket client = this.sockTcp.Accept();
            this.dictionary.Add(this.userCounter, client);
            result = this.userCounter;
            this.userCounter++;

            return result;
        }



        public void ServerDisconnectClient(int id)
        {
            if (this.dictionary.ContainsKey(id))
            {
                this.dictionary[id].Close();
                this.dictionary.Remove(id);
            }
        }



        public void ServerExit()
        {
            if(this.dictionary.Count != 0)
            {
                this.dictionary.Values.First<Socket>().Send(Encoding.ASCII.GetBytes(TransferProtocol.clientToServerUpgradeMessage));
                string newServerIpAddress = (this.dictionary.Values.First<Socket>().RemoteEndPoint as IPEndPoint).Address.ToString();

                if (this.dictionary.Count > 1)
                {
                    for (int counter = 0; counter < this.dictionary.Keys.Count; counter++)
                    {
                        this.dictionary.Values.ElementAt<Socket>(counter).Send(Encoding.ASCII.GetBytes(TransferProtocol.newServerToConnectiMessage+newServerIpAddress));
                        this.dictionary.Values.ElementAt<Socket>(counter).Close();
                    }
                }
            }

            for (int counter = 0; counter < this.dictionary.Keys.Count; counter++)
            {
                this.dictionary.Values.ElementAt<Socket>(counter).Close();
            }
            
            this.sockTcp.Close();
        }


        public void ServerListenToBroadcast()
        {
            IPAddress broadcastAddr = IPAddress.Parse(this.ipAddress.Substring(0, this.ipAddress.LastIndexOf('.')) + ".255");
            byte[] send = Encoding.ASCII.GetBytes("!iAmServerFromNaween" + this.ipAddress);
            byte[] read = new byte[40];
            IPEndPoint ipEndPoint = new IPEndPoint(broadcastAddr, this.portToRead);
            EndPoint endPoint = (EndPoint)new IPEndPoint(IPAddress.Any, this.port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind((EndPoint)new IPEndPoint(IPAddress.Parse(this.ipAddress), this.port));

            socket.ReceiveFrom(read, 40, SocketFlags.None, ref endPoint);

            if (Encoding.ASCII.GetString(read).TrimEnd('\0') == "!pleaseTellMeYouAreServerFromDenys")
            {
                Console.WriteLine("Send Broadcast message");
                for (int counter = 0; counter < 5; counter++)
                {
                    socket.SendTo(send, ipEndPoint);
                    Thread.Sleep(50);
                }
            }

            socket.Close();
        }
    }
}
