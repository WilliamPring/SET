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
        string printToScreen;

        StreamWriter output;
        StreamReader input;

        bool isItServer;

        public static bool ableToPost = true;
        public static bool finished = false;
        public static bool connectClient = false; 
        
        delegate void updateTextBox(string text); //To update the textbox from a NON-UI thread 

        Thread newthread;

        NamedPipeServerStream serverWriting;
        NamedPipeServerStream serverReading;

        NamedPipeClientStream clientWriting;
        NamedPipeClientStream clientReading;
    
        public Form1()
        {
            InitializeComponent();
            //Read only textboxes, some are temporary 
            ChatScreen.ReadOnly    = true;
            TextScreen.ReadOnly    = true;
            UserName.ReadOnly      = true;
            LocalComputer.ReadOnly = true;

            isItServer = false;

            Connect.Enabled     = false;

            //For the user name and path of the queue 
            userName         = "BIRRIAM";
            printToScreen    = "";
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
            //string[] user = UserName.Lines;

            ////Enable more form options
            //TextScreen.ReadOnly = false;
            
            //for (int i = 0; i < user.Length; i++)
            //{
            //    userName += user[i]; 
            //}

            //if(userName == "")
            //{
            //    userName = "Anonymous"; 
            //}    

            //pipeNameOne = LocalComputer.Text;          

            //client = new NamedPipeClientStream(".", pipeNameOne); //The name of the server pipe
            //client.Connect();
            //output = new StreamWriter(client); //Write to the client stream

            ////Turn on all of the buttons for use 
            //Connect.Enabled = false;
            //UserName.ReadOnly = true;
            //LocalComputer.ReadOnly = true;                        
                    
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
            if(ableToPost)
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

                    if(isItServer)
                    {
                        //output = new StreamWriter(serverWriting);
                        output = new StreamWriter(serverReading);
                        output.WriteLine(userName + " >> " + printToScreen);
                        output.Flush();
                    }
                    else
                    {
                        output = new StreamWriter(clientWriting);
                        //output = new StreamWriter(clientReading);
                        output.WriteLine(userName + " >> " + printToScreen);
                        output.Flush();

                    }              

                    //Write to the screen 
                    ChatScreen.Text += userName + " >> " + printToScreen + "\n";
                    ChatScreen.SelectionStart = ChatScreen.Text.Length;
                    ChatScreen.ScrollToCaret();
                }
            }                   
        }

        /******************************************************************/                

        /*************** Non-UI Thread Reading From Queue ***************/
                

        /***** Implementing Client and Server With Invoke of New Thread ******/

        private void Server_Click(object sender, EventArgs e)
        {
            string pipeServerNameOne = PipeName_One.Text; //Name of the first server pipe 
            
            if (pipeServerNameOne != "")
            {
                serverWriting = new NamedPipeServerStream(pipeServerNameOne + "write"); //Start the writing thread               
                serverReading = new NamedPipeServerStream(pipeServerNameOne + "read");

                serverReading.WaitForConnection(); //Wait for both the server pipes to be connected 
                serverWriting.WaitForConnection();

                TextScreen.ReadOnly = false; //Let user type in textbox

                //Start the new threads 
                newthread = new Thread(new ParameterizedThreadStart(RequestChatLog)); //Method invoked when thread is created 
                newthread.Start("server"); //Name from the serverName textbox 
                isItServer = true; 
            }
        }

        private void Host_Click(object sender, EventArgs e)
        {
            string pipeClientNameOne = PipeName_One.Text;

            if (pipeClientNameOne != "")
            {                
                clientWriting = new NamedPipeClientStream(".", pipeClientNameOne + "write");
                clientReading = new NamedPipeClientStream(".", pipeClientNameOne + "read");

                clientReading.Connect(); //Connect to both pipes from the server 
                clientWriting.Connect();

                TextScreen.ReadOnly = false; //Let user type in textbox
                
                //Start the threads 
                newthread = new Thread(new ParameterizedThreadStart(RequestChatLog)); //Method invoked when thread is created 
                newthread.Start("client"); //Name from the serverName textbox 
            }

        }

        /************** Method invoked when the thread is started *************/

        private void RequestChatLog(object pName)
        {    
            //Once the connection has been established run a loop to continuously get input

            string typeReader = (string)pName;

            if (typeReader == "server")
            {
                input = new StreamReader(serverWriting);
            }

            if (typeReader == "client")
            {
                input = new StreamReader(clientReading); // Crashes here 
            }

            while (true)
            {          

                string screenMsg = input.ReadLine();

                //if (screenMsg == "|Client Has Disconnected|")
                //{
                //    continue; 
                //}

                if (screenMsg == "|Client Has Disconnected|")
                {
                    if (ChatScreen.InvokeRequired)
                    {
                        ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { screenMsg }); //Print the message with safe thread call 
                    }
                    ableToPost = false; 
                    break; 
                }

                
                if(screenMsg == "|exit|")
                {
                    if (isItServer)
                    {
                        output = new StreamWriter(serverReading);
                        output.WriteLine("|Client Has Disconnected|");
                        output.Flush();
                    }
                    else
                    {
                        output = new StreamWriter(clientWriting);
                        output.WriteLine("|Client Has Disconnected|");
                        output.Flush();
                    }
                    ableToPost = false; 
                    break;
                }

                //Call the Safe Thread Call, Delegate 

                if (ChatScreen.InvokeRequired)
                {
                    ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { screenMsg }); //Print the message with safe thread call 
                }
            }
        }

        private void writeToScreen(object text) //Used from the NON-UI Threads 
        {
            string print = (string)text;
            ChatScreen.Text += print + "\n";
            ChatScreen.SelectionStart = ChatScreen.Text.Length;
            ChatScreen.ScrollToCaret();
        }

        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            if(ableToPost)
            {
                if (isItServer)
                {
                    output = new StreamWriter(serverReading);
                    output.WriteLine("|exit|");
                    output.Flush();
                }
                else
                {
                    output = new StreamWriter(clientWriting);
                    output.WriteLine("|exit|");
                    output.Flush();
                }

            }            
        
            //Exiting the thread 
        }
    }
}


