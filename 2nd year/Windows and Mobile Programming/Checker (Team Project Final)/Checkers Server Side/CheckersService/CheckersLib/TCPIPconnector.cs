/*
 * FILE             : TCPIPconnector.cs
 * PROJECT          : RDB assignment #4 and WMP assignment #6
 * PROGRAMMER       : Denys Politiuk
 * FIRST VERSION    : 23.11.2015
 * DESCRIPTION      :
 *  The file contains TCPIPconnector parent class to communicate between client and a server,
 * TCPIPconnectorClient and TCPIPconnectorServer child classes that inherit from the TCPIPconnector
 */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TCPIPconnector
{
    class DataBox
    {
        public Socket socket = null;
        public const int bufferSize = 1024;
        public byte[] buffer = new byte[bufferSize];
        public StringBuilder builder = new StringBuilder();
        public ManualResetEvent mutex = new ManualResetEvent(false);
        public String response;
    }



    public class TCPIPconnector
    {
        private const int backlog = 20;
        public const string Server = "server";
        public const string kClientLeft = "!clientExit";
        public const string kServerLeft = "!serverExit";
        public const string Client = "client";

        private string type;
        private Socket sock;
        private IPEndPoint endPoint;

        private List<Socket> serverAllClients;
        private Socket serverLastClient;

        private volatile ManualResetEvent clientConnect;
        private volatile ManualResetEvent clientSend;
        private volatile ManualResetEvent clientReceive;
        private String clientResponse;
        private bool clientConnected = true;
        private bool clientReading = false;

        // string type -> client for creating a client connection
        //                server for creating a server connection
        public TCPIPconnector(string type, int port, IPAddress address)
        {
            try
            {
                this.endPoint = new IPEndPoint(address, port);
                this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (Exception e)
            {
                throw e;
            }

            this.type = type;

            if (type == "client")
            {
                this.clientConnect = new ManualResetEvent(false);
                this.clientSend = new ManualResetEvent(false);
                this.clientReceive = new ManualResetEvent(false);

                this.clientResponse = String.Empty;
            }
            else if (type == "server")
            {
                // empty
            }
        }



        // closes all connections with the server/clients
        protected void Dispose()
        {
            if (this.type == TCPIPconnector.Client)
            {
                if (this.clientConnected)
                {
                    if (this.clientReading == true)
                    {
                        byte[] tmp = Encoding.ASCII.GetBytes("!closeMe");
                        this.sock.Send(tmp);
                        // waiting to get confirmation that reading has ended
                        this.clientReceive.WaitOne();
                    }
                    else
                    {
                        byte[] tmp = Encoding.ASCII.GetBytes("!closeMeNoRead");
                        this.sock.Send(tmp);
                        //this.clientSend.WaitOne();
                    }
                }

                if (this.sock.Connected)
                {
                    this.sock.Shutdown(SocketShutdown.Both);
                }
                this.sock.Close();
            }
            else if (this.type == TCPIPconnector.Server)
            {
                if (this.serverAllClients.Count != 0)
                {
                    // tell client that server is closed
                    byte[] tmp = Encoding.ASCII.GetBytes("!closeYou");
                    foreach (Socket s in this.serverAllClients)
                    {
                        s.Send(tmp);
                    }
                }
                else
                {
                    foreach (Socket s in this.serverAllClients)
                    {
                        s.Shutdown(SocketShutdown.Both);
                        s.Close();
                    }

                    if (this.sock.Connected)
                    {
                        this.sock.Shutdown(SocketShutdown.Both);
                    }
                    this.sock.Close();
                }
            }
        }



        protected string Connect()
        {
            string result = "";
            if (this.type == TCPIPconnector.Client)
            {
                try
                {
                    IAsyncResult connectResult = this.sock.BeginConnect(this.endPoint.Address, this.endPoint.Port, null, null);
                    bool connectSuccess = connectResult.AsyncWaitHandle.WaitOne(2000, true);
                    if (!connectSuccess)
                    {
                        this.sock.Close();
                    }
                }
                catch (Exception e)
                {
                    result = e.Message;
                }
                if (this.sock.Connected == false)
                {
                    result = "Error, could not connect to the specified server";
                }
            }
            else if (this.type == TCPIPconnector.Server)
            {
                try
                {
                    this.sock.Bind(this.endPoint);
                    this.sock.Listen(backlog);
                    this.serverAllClients = new List<Socket>();
                }
                catch (Exception e)
                {
                    result = e.Message;
                }
            }

            return result;
        }




        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /*                     CLIENT SIDE                                             */
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////




        protected string Send(string message)
        {
            string result = "";

            if (this.type == TCPIPconnector.Client)
            {
                byte[] data = Encoding.ASCII.GetBytes(message);

                if (this.clientConnected)
                {
                    try
                    {
                        this.clientSend.Reset();
                        this.sock.BeginSend(data, 0, data.Length, 0, new AsyncCallback(ClientSendCallback), this.sock);
                        this.clientSend.WaitOne();
                    }
                    catch (Exception e)
                    {
                        this.clientSend.Reset();
                        result = e.Message;
                    }
                }
            }

            return result;
        }



        protected string Send(byte[] message)
        {
            string result = "";

            if (this.type == TCPIPconnector.Client)
            {
                if (this.clientConnected)
                {
                    try
                    {
                        this.clientSend.Reset();
                        this.sock.BeginSend(message, 0, message.Length, 0, new AsyncCallback(ClientSendCallback), this.sock);
                        this.clientSend.WaitOne();
                    }
                    catch (Exception e)
                    {
                        this.clientSend.Reset();
                        result = e.Message;
                    }
                }
            }

            return result;
        }



        private void ClientSendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;

            client.EndSend(ar);

            this.clientSend.Set();
        }



        // returns TCPIPconnector.kServerLeft when server has been closed
        protected string Read()
        {
            string result = "";

            if (this.type == TCPIPconnector.Client)
            {
                DataBox box = new DataBox();
                box.socket = this.sock;

                this.clientReceive.Reset();
                this.clientReading = true;
                this.sock.Receive(box.buffer, 0, DataBox.bufferSize, 0);
                this.clientReading = false;
                this.clientReceive.Set();

                result = Encoding.ASCII.GetString(box.buffer, 0, box.buffer.Length).TrimEnd('\0');
                if (result == "!closeMe")
                {
                    result = "";
                }
                if (result == "!closeYou")
                {
                    this.clientConnected = false;
                    if (this.sock.Connected)
                    {
                        this.sock.Disconnect(false);
                    }
                    result = TCPIPconnector.kServerLeft;
                }
                this.clientResponse = "";
            }

            return result;
        }


        // returns null if the server has left
        protected byte[] ReadObject()
        {
            byte[] rslt = new byte[DataBox.bufferSize];

            if (this.type == TCPIPconnector.Client)
            {
                DataBox box = new DataBox();
                box.socket = this.sock;

                this.clientReceive.Reset();
                this.clientReading = true;
                this.sock.Receive(box.buffer, 0, DataBox.bufferSize, 0);
                this.clientReading = false;
                this.clientReceive.Set();

                rslt = box.buffer;

                string result = Encoding.ASCII.GetString(box.buffer, 0, box.buffer.Length).TrimEnd('\0');
                if (result == "!closeMe")
                {
                    result = "";
                }
                if (result == "!closeYou")
                {
                    this.clientConnected = false;
                    if (this.sock.Connected)
                    {
                        this.sock.Disconnect(false);
                    }
                    result = TCPIPconnector.kServerLeft;
                    rslt = null;
                }
                this.clientResponse = "";
            }

            return rslt;
        }



        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////
        /*                     SERVER SIDE                                             */
        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////



        protected Socket ServerAcceptOneClient()
        {
            if (this.type == TCPIPconnector.Server)
            {
                this.serverLastClient = this.sock.Accept();
                this.serverAllClients.Add(this.serverLastClient);
            }
            else
            {
                this.serverLastClient = null;
            }

            return this.serverLastClient;
        }



        // returns TCPIPconnector.kClientLeft when client has left
        protected string ServerRead(Socket sender)
        {
            string result = "";

            if (this.type == TCPIPconnector.Server)
            {
                DataBox box = new DataBox();
                box.socket = sender;
                box.mutex.Reset();
                if ((box.socket != null) && (box.socket.Connected))
                {
                    box.socket.BeginReceive(box.buffer, 0, DataBox.bufferSize, 0, new AsyncCallback(ServerReadCallback), box);
                    box.mutex.WaitOne();
                }
                else
                {
                    box.response = TCPIPconnector.kClientLeft;
                }
                result = box.response;
            }

            return result;
        }



        protected byte[] ServerReadObject(Socket sender)
        {
            byte[] rslt = new byte[DataBox.bufferSize];

            if (this.type == TCPIPconnector.Server)
            {
                DataBox box = new DataBox();
                box.socket = sender;
                box.mutex.Reset();
                if ((box.socket != null) && (box.socket.Connected))
                {
                    box.socket.BeginReceive(box.buffer, 0, DataBox.bufferSize, 0, new AsyncCallback(ServerReadCallback), box);
                    box.mutex.WaitOne();
                    rslt = box.buffer;
                }
                else
                {
                    box.response = TCPIPconnector.kClientLeft;
                    rslt = null;
                }
            }

            return rslt;
        }



        private void ServerReadCallback(IAsyncResult ar)
        {
            DataBox box = (DataBox)ar.AsyncState;
            box.response = String.Empty;
            Socket handler = box.socket;

            int byteRead = handler.EndReceive(ar);

            if (byteRead > 0)
            {
                box.builder.Append(Encoding.ASCII.GetString(box.buffer, 0, byteRead));

                box.response = box.builder.ToString();

                if (box.builder.ToString() == "!closeMe")
                {
                    handler.Send(box.buffer);
                    this.serverAllClients.Remove(handler);
                    if (this.serverLastClient == handler)
                    {
                        this.serverLastClient = null;
                    }
                    handler.Close();
                }
                if (box.builder.ToString() == "!closeMeNoRead")
                {
                    this.serverAllClients.Remove(handler);
                    if (this.serverLastClient == handler)
                    {
                        this.serverLastClient = null;
                    }
                    handler.Close();
                }
            }
            box.mutex.Set();
        }



        protected void ServerSend(String message, Socket receiver)
        {
            if (this.type == TCPIPconnector.Server)
            {
                Socket clientSocket = receiver;
                // Convert the string data to byte data using ASCII encoding.
                byte[] byteData = Encoding.ASCII.GetBytes(message);

                // Begin sending the data to the remote device.
                if (receiver != null)
                {
                    clientSocket.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(ServerSendCallback), clientSocket);
                }
            }
        }



        protected void ServerSend(byte[] message, Socket receiver)
        {
            if (this.type == TCPIPconnector.Server)
            {
                Socket clientSocket = receiver;

                // Begin sending the data to the remote device.
                if (receiver != null)
                {
                    clientSocket.BeginSend(message, 0, message.Length, 0, new AsyncCallback(ServerSendCallback), clientSocket);
                }
            }
        }



        private void ServerSendCallback(IAsyncResult ar)
        {
            Socket handler = (Socket)ar.AsyncState;

            handler.EndSend(ar);
        }
    }



    public class TCPIPconnectorClient : TCPIPconnector
    {
        public TCPIPconnectorClient(int port, IPAddress address)
            : base(TCPIPconnector.Client, port, address)
        {
            // empty
        }



        new public void Dispose()
        {
            base.Dispose();
        }



        new public string Connect()
        {
            return base.Connect();
        }



        new public void Send(String message)
        {
            base.Send(message);
        }



        new public void Send(byte[] message)
        {
            base.Send(message);
        }



        new public string Read()
        {
            return base.Read();
        }



        new public byte[] ReadObject()
        {
            return base.ReadObject();
        }
    }



    public class TCPIPconnectorServer : TCPIPconnector
    {
        public TCPIPconnectorServer(int port, IPAddress address)
            : base(TCPIPconnector.Server, port, address)
        {
            // empty
        }



        new public void Dispose()
        {
            base.Dispose();
        }



        new public string Connect()
        {
            return base.Connect();
        }



        public Socket AcceptOneClient()
        {
            return base.ServerAcceptOneClient();
        }



        public string Read(Socket sender)
        {
            return base.ServerRead(sender);
        }



        public byte[] ReadObject(Socket sender)
        {
            return base.ServerReadObject(sender);
        }



        public void Send(String message, Socket receiver)
        {
            base.ServerSend(message, receiver);
        }



        public void Send(byte[] message, Socket receiver)
        {
            base.ServerSend(message, receiver);
        }
    }
}