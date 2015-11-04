using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Messaging;
using System.IO; 
using System.IO.Pipes;

namespace ChatProgram
{
    public partial class Form1 : Form
    {
        string userName;
        string pipeNameOne;
        string pipeNameTwo; 

        string printToScreen;
        string retPrintToScreen;

        StreamWriter output; 

        public static bool finished = false;
        public static bool connectClient = false; 
        
        delegate void updateTextBox(string text); //To update the textbox from a NON-UI thread 

        Thread newthread;

        NamedPipeServerStream server;
        NamedPipeClientStream client; 
    
        public Form1()
        {
            InitializeComponent();
            //Read only textboxes, some are temporary 
            ChatScreen.ReadOnly    = true;
            TextScreen.ReadOnly    = true;
            UserName.ReadOnly      = true;
            LocalComputer.ReadOnly = true; 

            Connect.Enabled     = false;
            Disconnect.Enabled  = false; 

            //For the user name and path of the queue 
            userName         = "";
            printToScreen    = "";
            retPrintToScreen = "";
            pipeNameOne = "";
            pipeNameTwo = "";

        }
 
        private void LocalComputer_TextChanged(object sender, EventArgs e)
        {
            //Only submit when the user has entered something in the Pipe Name textbox 
            if(LocalComputer.Lines.Length != 0)
            {
                Connect.Enabled = true; 
            }
            else
            {
                Connect.Enabled = false;
            }
        }

        /************************************/


        private void Connect_Click_1(object sender, EventArgs e)
        {
            string[] user = UserName.Lines;

            //Enable more form options
            TextScreen.ReadOnly = false;
            
            for (int i = 0; i < user.Length; i++)
            {
                userName += user[i]; 
            }

            if(userName == "")
            {
                userName = "Anonymous"; 
            }    

            pipeNameOne = LocalComputer.Text;          

            client = new NamedPipeClientStream(pipeNameOne); //The name of the server pipe
            client.Connect();
            output = new StreamWriter(client); //Write to the client stream

            //Turn on all of the buttons for use 
            Connect.Enabled = false;
            Disconnect.Enabled = true;
            UserName.ReadOnly = true;
            LocalComputer.ReadOnly = true;                        
                    
        }


        /*************** UI MAINT THREAD Send to Queue ***************/
        
        private void Send_Click(object sender, EventArgs e)
        {
            ChatLog(); 
        }     

        private void TextScreen_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode == Keys.Enter)
           {
               ChatLog();
           }
        }

        private void ChatLog()
        {
            string[] sendMessage = TextScreen.Lines;
            printToScreen = "";

            for (int i = 0; i < sendMessage.Length; i++)
            {
                printToScreen += sendMessage[i];
            }

            if (printToScreen != "")
            {
                TextScreen.Text = "";
                //Send to the other chat
                output = new StreamWriter(client);
                output.WriteLine(userName + " >> " + printToScreen + "\n");
                output.Flush();
                //Write to the screen 
                ChatScreen.SelectionColor = Color.Black; 
                ChatScreen.Text += userName + " >> " + printToScreen + "\n";
                ChatScreen.SelectionStart = ChatScreen.Text.Length; 
                ChatScreen.ScrollToCaret(); 
            }
        }

        /******************************************************************/             

        //Disconnect the client from the conversation

        private void Disconnect_Click(object sender, EventArgs e)
        {
            newthread.Join();
            Form1.finished = true; //End the writing loop

            //Disable most buttons 
            ChatScreen.ReadOnly = true;
            TextScreen.ReadOnly = true;
            Connect.Enabled     = true; 
            Disconnect.Enabled  = false;

            //Enable certain buttons
            UserName.ReadOnly = false;
            LocalComputer.ReadOnly = false;

            //Clear everything (Textboxes)
            TextScreen.Text = "";
            ChatScreen.Text = "";
            UserName.Text = "";
            LocalComputer.Text = ""; 

            //Clear variables
            userName = "";
        }

        /*************** Non-UI Thread Reading From Queue ***************/
          
        private void Host(object sender, EventArgs e)
        {
            string pipeServerName = ServerName.Text;

            if (pipeServerName != "")
            {
                ServerName.ReadOnly = true;
                HostButton.Enabled  = false; 
                
                //Allow for user to type in textbox
                UserName.ReadOnly      = false;
                LocalComputer.ReadOnly = false; 

                newthread = new Thread(new ParameterizedThreadStart(RequestChatLog)); //Method invoked when thread is created 
                newthread.Start(pipeServerName); //Name from the serverName textbox 
            }
        }

        private void RequestChatLog(object pipeArgName)
        {
            string pipeName = (string)pipeArgName; 
            
            server = new NamedPipeServerStream(pipeName);
            server.WaitForConnection();
            StreamReader input = new StreamReader(server);

            while (!finished)
            {
                string screenMsg = input.ReadLine();

                if (ChatScreen.InvokeRequired)
                {
                    ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { screenMsg }); //Print the message with safe thread call 
                }
            }
        }

        private void writeToScreen(object text) //Used from the NON-UI Threads 
        {
            string print = (string)text;
            ChatScreen.SelectionColor = Color.Red;
            ChatScreen.Text += print + "\n";
            ChatScreen.SelectionStart = ChatScreen.Text.Length;
            ChatScreen.ScrollToCaret();
        }

        private void closingForm(object sender, FormClosingEventArgs e)
        {
            Form1.finished = true;
            server.Dispose();
            server = null; 
        }
    }
}


