/*    
 * Filename: TCPIPconnectorClient.cs
 * Assignment: WMP Final Project 
 * By: Naween Mehanmal and William Pring, Denys Politiuk
 * Date: December 16, 2015
 * Description: The file containts client side of the TCPIP communication
 *
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace CheckersClient
{
    /// <summary>
    /// TCPIp client for the Final project
    /// </summary>
    class TCPIPconnectorClient
    {
        private Socket sock = null;
        private ManualResetEvent clientDone = new ManualResetEvent(false);
        private const int timeout = 1000;
        private const int maxBuffer = 1024;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public TCPIPconnectorClient()
        {

        }


        /// <summary>
        /// Connect to the server
        /// </summary>
        /// <param name="ipAddress">IP address of the server</param>
        /// <param name="port">port to connect to</param>
        /// <returns></returns>
        public string Connect(string ipAddress, int port)
        {
            string result = string.Empty;
            bool finished = false;

            DnsEndPoint host = new DnsEndPoint(ipAddress, port);

            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs socketEventArgs = new SocketAsyncEventArgs();
            socketEventArgs.RemoteEndPoint = host;

            socketEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(
                delegate (object s, SocketAsyncEventArgs e)
                {
                    result = e.SocketError.ToString();

                    this.clientDone.Set();
                    finished = true;
                });
            this.clientDone.Reset();

            this.sock.ConnectAsync(socketEventArgs);

            this.clientDone.WaitOne(TCPIPconnectorClient.timeout);

            if (!finished)
            {
                result = "!error";
            }

            return result;
        }



        /// <summary>
        /// Send message to the server
        /// </summary>
        /// <param name="data">Data to send</param>
        /// <returns></returns>
        public string Send(string data)
        {
            string response = "";

            if (this.sock != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();

                socketEventArg.RemoteEndPoint = this.sock.RemoteEndPoint;
                socketEventArg.UserToken = null;

                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(
                    delegate (object s, SocketAsyncEventArgs e)
                    {
                        response = e.SocketError.ToString();
                        this.clientDone.Set();
                    });

                byte[] message = Encoding.UTF8.GetBytes(data);
                socketEventArg.SetBuffer(message, 0, message.Length);

                this.clientDone.Reset();

                this.sock.SendAsync(socketEventArg);

                this.clientDone.WaitOne(TCPIPconnectorClient.timeout);
            }
            else
            {
                response = "Socket is not Initialized";
            }

            return response;
        }


        /// <summary>
        /// Read the data from the server
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            string response = "Operation Timeout";

            if (this.sock != null)
            {
                SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                socketEventArg.RemoteEndPoint = this.sock.RemoteEndPoint;

                socketEventArg.SetBuffer(new Byte[TCPIPconnectorClient.maxBuffer], 0, TCPIPconnectorClient.maxBuffer);

                socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(
                    delegate (object s, SocketAsyncEventArgs e)
                    {
                        if (e.SocketError == SocketError.Success)
                        {
                            response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                            response = response.Trim('\0');
                        }
                        else
                        {
                            response = e.SocketError.ToString();
                        }

                        this.clientDone.Set();
                    });

                this.clientDone.Reset();

                this.sock.ReceiveAsync(socketEventArg);

                this.clientDone.WaitOne();
            }
            else
            {
                response = "Socket is not initialized";
            }

            return response;
        }


        /// <summary>
        /// Close the client
        /// </summary>
        public void Close()
        {
            if (this.sock != null)
            {
                this.Send("!closeMeNoRead");
                if (this.sock.Connected)
                {
                    this.sock.Shutdown(SocketShutdown.Both);
                }
            }
        }


        /// <summary>
        /// Dispose of the objects on the client
        /// </summary>
        public void Dispose()
        {
            this.sock.Dispose();
        }
    }
}
