using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using IADtransfer;
using System.Threading;

namespace IAD
{
    public partial class GameUI : Form
    {
        TransferProtocol transferProtocol;
        bool? isClient;
        List<int> listOfClients;
        bool statusForClient = true;

        System.Drawing.Graphics graphicsObj;
        Pen myPen;
        Rectangle rectangle;
        SolidBrush solidBrush;
        bool loopStatusForBroadcast;
        int yPos;
        int xPos;
        int widthBorder;
        Random rand;
        public GameUI()
        {
            InitializeComponent();

            this.listOfClients = new List<int>();

            this.transferProtocol = new TransferProtocol();

            int broadcastResult = this.transferProtocol.SendBroadcast();
            if (broadcastResult == TransferProtocol.ClientState)
            {
                this.isClient = true;
                this.transferProtocol.StartAsClient();

                Thread client = new Thread(new ParameterizedThreadStart(startClient));
                client.Start(isClient);
            }
            else if (broadcastResult == TransferProtocol.ServerState)
            {

                this.isClient = false;
                this.transferProtocol.StartAsServer();

                Thread trd = new Thread(new ParameterizedThreadStart(broadcast));
                //make the loopStatusForBroadcast True so it will loop infinite
                loopStatusForBroadcast = true;
                trd.Start((object)loopStatusForBroadcast);

                //thread for accepting threads
                Thread acceptClientThread = new Thread(acceptClient);
                acceptClientThread.Start();
            }
            else
            {
                this.isClient = null;
            }


            xPos = 2;
            yPos = 2;
            widthBorder = 1;
            graphicsObj = displayPnl.CreateGraphics();
            myPen = new Pen(Color.Black, widthBorder);
            rectangle = new Rectangle(xPos, yPos, 50, 50);
            solidBrush = new SolidBrush(Color.PaleVioletRed);
            rand = new Random();
        }
        private void startClient(object status)
        {
            //bool statusForClient = (bool)status;
            string msg = "";
            while (statusForClient)
            {
                msg = this.transferProtocol.ClientReadTCP();

                if (msg == TransferProtocol.clientToServerUpgradeMessage)
                {
                    MessageBox.Show(msg);
                }
                else if (msg == TransferProtocol.newServerToConnectiMessage)
                {
                    MessageBox.Show(msg);
                }
                else
                {
                    //MessageBox.Show(msg);
                    if (msg == "UP")
                    {
                        bttnUp_Click(status, EventArgs.Empty);
                    }
                    else if (msg == "DOWN")
                    {
                        bttnDown_Click(status, EventArgs.Empty);
                    }
                    else if (msg == "LEFT")
                    {
                        bttnLeft_Click(status, EventArgs.Empty);
                    }
                    else if (msg == "RIGHT")
                    {
                        bttnRight_Click(status, EventArgs.Empty);
                    }
                }
            }

            MessageBox.Show("Killed Client Reading Thread");
        }

        private void acceptClient()
        {
            bool isRunning = true;
            Thread readThread; 

            while (isRunning)
            {
                // start client accepting thread
                int client = this.transferProtocol.ServerAcceptOneClient();
                listOfClients.Add(client);

                //Once client has been accepted and added to the list
                //A new thread will be spawned to listen in on incoming messages
                readThread = new Thread(new ParameterizedThreadStart(listenOnCommunication));
                readThread.Start(client);
            }
        }



        private void listenOnCommunication(object sender)
        {
            int threadID = (int)sender; //Thread ID
            bool isRead  = true;
            string readMsg = "";

            //Listen in on incoming messages
            while(isRead)
            {
                readMsg = transferProtocol.ServerReadTCP(threadID);
                //MessageBox.Show(msg);
                if (readMsg == "UP")
                {
                    bttnUp_Click(sender, EventArgs.Empty);
                }
                else if (readMsg == "DOWN")
                {
                    bttnDown_Click(sender, EventArgs.Empty);
                }
                else if (readMsg == "LEFT")
                {
                    bttnLeft_Click(sender, EventArgs.Empty);
                }
                else if (readMsg == "RIGHT")
                {
                    bttnRight_Click(sender, EventArgs.Empty);
                }

                if (readMsg == "!exit")
                {
                    this.transferProtocol.ServerDisconnectClient(threadID);
                    break;
                }
            }

            MessageBox.Show("Killed Reading for client #" + threadID.ToString());
        }




        /// <summary>
        /// The thread that will be listening to the ServerListenToBroadCast function
        /// </summary>
        /// <param name="broadcastListenStatus"></param>
        private void broadcast(object broadcastListenStatus)
        {
            bool status = (bool)broadcastListenStatus;
            while (status)
            {
                this.transferProtocol.ServerListenToBroadcast();
            }
        }




        private void bttnRight_Click(object sender, EventArgs e)
        {
            if ((xPos + 54 + widthBorder) <= displayPnl.Width)
            {
                xPos += 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
            else
            {
                int t = 0;
                t++;
            }
        }
        private void bttnUp_Click(object sender, EventArgs e)
        {
            if (yPos >=0)
            {
                yPos -= 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }
        private void bttnDown_Click(object sender, EventArgs e)
        {
            if ((yPos + 54 + widthBorder) <= displayPnl.Height)
            {
                yPos += 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }

        private void bttnLeft_Click(object sender, EventArgs e)
        {
            if (xPos >= 0)
            {
                xPos -= 4;
                rectangle = new Rectangle(xPos, yPos, 50, 50);
                graphicsObj.DrawRectangle(myPen, rectangle);
                displayPnl.Invalidate();
            }
        }

        private void displayPnl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(solidBrush, rectangle);
            graphicsObj.DrawRectangle(myPen, rectangle);
        }

        private void bttnFill_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogFilling.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                solidBrush = new SolidBrush(colorDialogFilling.Color);
                displayPnl.Invalidate();
            }
        }

        private void bttnBorder_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialogFilling.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                myPen = new Pen(colorDialogFilling.Color);
                displayPnl.Invalidate();
            }
        }

        private void bttnSendRandMove_Click(object sender, EventArgs e)
        {
            string tcpMessage = "";

            int randomMove = rand.Next(1, 4);
            //up
            if (randomMove==1)
            {
                bttnUp_Click(sender, e);
                tcpMessage = "UP";
            }
            //down
            else if (randomMove ==2)
            {
                bttnDown_Click(sender, e);
                tcpMessage = "DOWN";
            }
            //left
            else if(randomMove ==3)
            {
                bttnLeft_Click(sender, e);
                tcpMessage = "LEFT";
            }
            //right
            else if (randomMove==4)
            {
                bttnRight_Click(sender, e);
                tcpMessage = "RIGHT";
            }

            if ((bool)isClient)
            {
                this.transferProtocol.ClientSendTCP(tcpMessage);
            }
            else if (!(bool)isClient)
            {
                this.transferProtocol.ServerSendTCPAll(tcpMessage);                
            }
        }

        private void GameUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((bool)this.isClient)
            {
                this.transferProtocol.ClientExit("!exit");
                this.statusForClient = false;
            }
        }
    }
}
