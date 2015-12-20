/*
    FILE            : CheckersServer.cs
    PROJECT         : WMP Final project
    PROGRAMMER      : Denys Politiuk
    FIRST VERSION   : 2015-12-03
    DESCRIPTION     :
        Class that contains the logic for the server for the final project
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using TCPIPconnector;

namespace CheckersLib
{
    /// <summary>
    /// Class Game that stores information about
    /// one game between two players
    /// </summary>
    class Game
    {
        public int id = 0;

        public Socket playerOne = null;
        public Socket playerTwo = null;
        public List<Socket> spectators = new List<Socket>();        
    }

    /// <summary>
    /// Class CheckersServer that contains main logic
    /// to connect clients and manage their communication between the game
    /// </summary>
    public class CheckersServer
    {
        private TCPIPconnectorServer connector;
        private int port;
        private bool run;
        private bool waitingForOneConnection = false;     
        private bool waitiForTwoConnection = false;
        private int toClose = 0;

        private List<Game> listAllGames;
        private Socket waiting;
        private Socket newClient;
        private int gameID;

        private string ip;



        /// <summary>
        /// Constructor for the server
        /// </summary>
        /// <param name="port">Port server will listen</param>
        public CheckersServer(int port)
        {
            IPAddress ipAddress = null;
            if (Dns.Resolve(Dns.GetHostName()).AddressList.Length > 1)
            {
                 ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[1];
            }
            else
            {
                ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            }
            
            this.port = port;

            this.ip = ipAddress.ToString();
            this.connector = new TCPIPconnectorServer(port, ipAddress);
            this.run = false;

            this.listAllGames = new List<Game>();
            this.gameID = 0;           
        }



        /// <summary>
        /// Start gathering clients and connecting them
        /// </summary>
        public void Start()
        {
            if (this.connector.Connect() == "")
            {
                this.run = true;
            }
            else
            {
                this.run = false;
            }

            //Console.WriteLine("Server Started\nIP: " + this.ip);            
            Logger.LogEvent("Start","Server Started\nIP: " + this.ip);

            while(this.run)
            {
                this.waitiForTwoConnection = true;
                this.waiting = this.connector.AcceptOneClient();
                //Console.WriteLine("Got First Client");
                Logger.LogEvent("Start","Got First Client");
                this.waitiForTwoConnection = false;
                if (this.run)
                {
                    this.connector.Send("!wait", this.waiting);
                }
                this.waitingForOneConnection = true;
                this.newClient = this.connector.AcceptOneClient();
                //Console.WriteLine("Got Second Client");
                Logger.LogEvent("Start", "Got Second Client");
                this.waitingForOneConnection = false;                              

                if (this.run)
                {
                    Game game = new Game();
                    this.gameID++;
                    game.id = gameID;
                    game.playerOne = this.waiting;
                    game.playerTwo = this.newClient;

                    this.listAllGames.Add(game);

                    Thread t = new Thread(new ParameterizedThreadStart(this.GameInteraction));
                    t.Start(game);
                }   
                else
                {
                    if (this.toClose == 2)
                    {
                        this.connector.Read(this.waiting);
                        this.connector.Read(this.newClient);
                    }

                    if (this.toClose == 1)
                    {
                        this.connector.Read(this.newClient);
                    }
                }             
            }
        }



        /// <summary>
        /// Game interaction between two connected players
        /// </summary>
        /// <param name="userGame">Game with clients</param>
        private void GameInteraction(object userGame)
        {
            Game game = (Game)userGame;
            string messageFirst = "";
            string messageSecond = "";

            this.connector.Send("!startOne", game.playerOne);
            this.connector.Send("!startTwo", game.playerTwo);

            bool firstExit = false;
            bool secondExit = false;

            while(true)
            {
                if (!firstExit)
                {
                    messageFirst = this.connector.Read(game.playerOne);

                    //Console.WriteLine("First " + messageFirst);
                    Logger.LogEvent("GameInteraction","First " + messageFirst);

                    if (messageFirst == TCPIPconnectorServer.kClientLeft)
                    {
                        firstExit = true;
                    }

                    if (messageFirst == "!send")
                    {
                        this.connector.Send("!ok", game.playerOne);
                        //byte[] tmp = this.connector.ReadObject(game.playerOne);
                        string tmp = this.connector.Read(game.playerOne);
                        this.connector.Send("!ok", game.playerOne);

                        this.connector.Send("!prepare", game.playerTwo);
                        this.connector.Read(game.playerTwo);
                        this.connector.Send(tmp, game.playerTwo);
                        this.connector.Read(game.playerTwo);
                    }
                }

                if (messageFirst == "!win")
                {
                    this.connector.Send("!lost", game.playerTwo);
                    firstExit = true;
                }

                if ((messageFirst == "!end") || (firstExit))
                {
                    if (!firstExit)
                    {
                        this.connector.Send("!turn", game.playerTwo);
                    }
                    else
                    {
                        if (!secondExit)
                        {
                            this.connector.Send("!left", game.playerTwo);
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true)
                    {
                        if (!secondExit)
                        {
                            messageSecond = this.connector.Read(game.playerTwo);

                            //Console.WriteLine("Second ", messageSecond);
                            Logger.LogEvent("GameInteraction", "Second " + messageFirst);

                            if (messageSecond == "!end")
                            {
                                this.connector.Send("!turn", game.playerOne);
                                break;
                            }

                            if (messageSecond == TCPIPconnectorServer.kClientLeft)
                            {
                                secondExit = true;
                                if (!firstExit)
                                {
                                    this.connector.Send("!left", game.playerOne);
                                }
                                break;
                            }

                            if (messageSecond == "!win")
                            {
                                this.connector.Send("!lost", game.playerOne);
                                secondExit = true;
                                break;
                            }

                            if (messageSecond == "!send")
                            {
                                this.connector.Send("!ok", game.playerTwo);
                                //byte[] tmp = this.connector.ReadObject(game.playerTwo);
                                string tmp = this.connector.Read(game.playerTwo);
                                this.connector.Send("!ok", game.playerTwo);

                                this.connector.Send("!prepare", game.playerOne);
                                this.connector.Read(game.playerOne);
                                this.connector.Send(tmp, game.playerOne);
                                this.connector.Read(game.playerOne);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (firstExit && secondExit)
                {
                    break;
                }
            }

            this.listAllGames.Remove(game);

            game.playerOne.Close();
            game.playerTwo.Close();
        }



        /// <summary>
        /// Method to stop run of the server connecting clients
        /// </summary>
        public void Stop()
        {
            this.run = false;

            IPAddress ipAddress = null;
            if (Dns.Resolve(Dns.GetHostName()).AddressList.Length > 1)
            {
                ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[1];
            }
            else
            {
                ipAddress = Dns.Resolve(Dns.GetHostName()).AddressList[0];
            }

            if (this.waitiForTwoConnection)
            {
                this.toClose = 2;
                TCPIPconnectorClient tmp = new TCPIPconnectorClient(this.port, ipAddress);
                TCPIPconnectorClient tmp2 = new TCPIPconnectorClient(this.port, ipAddress);
                tmp.Connect();
                tmp2.Connect();
                tmp.Dispose();
                tmp2.Dispose();
            }

            if (this.waitingForOneConnection)
            {
                this.toClose = 1;
                TCPIPconnectorClient tmp = new TCPIPconnectorClient(this.port, ipAddress);
                tmp.Connect();
                tmp.Dispose();
            }

            Thread.Sleep(10);
            this.Dispose();

            //Console.WriteLine("Server Stopped");
            Logger.LogEvent("Stop", "Server Stopped");
        }        


        /// <summary>
        /// Displase of the object on the server
        /// </summary>
        private void Dispose()
        {
            this.connector.Dispose();
        }
    }
}
