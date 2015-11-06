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
using System.Net; 

namespace ChatProgram
{
    public partial class Form1 : Form
    {
        /**** DATA MEMBERS ****/
        
        public static bool ableToPost = false;

        delegate void updateTextBox(string text); //To update the textbox from a NON-UI thread 

        string userName;
        string compName;
        string machineName; 
        string printToScreen;

        StreamWriter output;
        StreamReader input;

        bool isItServer;
    
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
            Send.Enabled = false; 
            isItServer   = false;
            Disconnect.Enabled = false; 

            //For the user name and path of the queue 
            userName = "";
            compName = "";
            machineName = ""; 
            printToScreen = "";
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
            if (ableToPost)
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

                    if (isItServer)
                    {
                        output = new StreamWriter(serverReading);
                        output.WriteLine(userName + " >> " + printToScreen);
                        output.Flush();
                    }
                    else
                    {
                        output = new StreamWriter(clientWriting);
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
        
        private void configureButtons()
        {
            ServerButton.Enabled = false;
            HostButton.Enabled   = false;
            PipeName_One.ReadOnly = true;
            UserName.ReadOnly     = true;
            TextScreen.ReadOnly  = false; //Let user type in textbox
            Send.Enabled = true;
            Disconnect.Enabled = true;
            
            userName = UserName.Text; //Get the username the user typed in
            
            if(userName == "")
            {
                userName = "Anonymous"; //Default username 
            }
        } 

        /***** Implementing Client and Server With Invoke of New Thread ******/
        
        private void Server_Click(object sender, EventArgs e)
        {
            try
            {
                string pipeServerNameOne = PipeName_One.Text; //Name of the first server pipe      

                if (pipeServerNameOne != "")
                {
                    ChatScreen.Text = ""; 

                    serverWriting = new NamedPipeServerStream(pipeServerNameOne + "write"); //Start the writing thread               
                    serverReading = new NamedPipeServerStream(pipeServerNameOne + "read");

                    serverReading.WaitForConnection(); //Wait for both the server pipes to be connected 
                    serverWriting.WaitForConnection();

                    configureButtons(); //Type out what's currently known about the server and computer hosting it

                    //Start the new threads 
                    newthread = new Thread(new ParameterizedThreadStart(RequestChatLog)); //Method invoked when thread is created 
                    newthread.Start("server"); //Name from the serverName textbox 
                    isItServer = true;
                }
                else
                {
                    MessageBox.Show("You must enter a pipe name");
                }
            }
            catch(IOException)
            {
                MessageBox.Show("Pipe name already taken");
                UserName.Text = ""; //Clear out the textbox 
                PipeName_One.Text = ""; 
            }           
        }

        private void Host_Click(object sender, EventArgs e)
        {
            try
            {
                string pipeClientNameOne = PipeName_One.Text;
                machineName = MachineServer.Text; //Name of the computer machine

                if(machineName == "")
                {
                    machineName = "."; //Default will connect to local machine 
                }

                if (pipeClientNameOne != "")
                {
                    ChatScreen.Text = ""; 
                    
                    clientWriting = new NamedPipeClientStream(machineName, pipeClientNameOne + "write");
                    clientReading = new NamedPipeClientStream(machineName, pipeClientNameOne + "read");

                    clientReading.Connect(250); //Connect to both pipes from the server 
                    clientWriting.Connect(250);

                    configureButtons(); 
                
                    //Start the threads 
                    newthread = new Thread(new ParameterizedThreadStart(RequestChatLog)); //Method invoked when thread is created 
                    newthread.Start("client"); //Name from the serverName textbox
                    isItServer = false; 
                }
                else
                {
                    MessageBox.Show("You must enter a pipe name");
                }
            }
            catch(TimeoutException)
            {
                MessageBox.Show("Unable to connect to pipe");
                UserName.Text = ""; //Clear out the textbox 
                PipeName_One.Text = ""; 
            } 
            catch(IOException ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        /*************** Non-UI Thread Reading From Queue ***************/

        private void RequestChatLog(object pName)
        {           

            ableToPost = true; 
            
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

                if (screenMsg == "|Client Has Disconnected|")
                {
                    if (ChatScreen.InvokeRequired)
                    {
                        ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { "Disconnected Successfully" }); //Print the message with safe thread call 
                    }

                    break; //Close the loop and thread altogether succesfully 
                }
                
                if(screenMsg == "|exit|")
                {
                    if (isItServer)
                    {
                        output = new StreamWriter(serverReading);
                        output.WriteLine("|Client Has Disconnected|");
                        output.Flush();

                        //CLOSE PIPES 

                        serverWriting.Dispose();
                        serverReading.Dispose();

                        serverWriting.Close();
                        serverReading.Close(); 

                        if (ChatScreen.InvokeRequired)
                        {
                            ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { "Client Disconnected\nAll Pipes Have Closed" }); //Print the message with safe thread call 
                        }
                    }
                    else
                    {                                             
                        output = new StreamWriter(clientWriting);
                        output.WriteLine("|Client Has Disconnected|");
                        output.Flush();

                        //CLOSE PIPES 

                        clientWriting.Dispose();
                        clientReading.Dispose();

                        clientWriting.Close();
                        clientReading.Close(); 

                        if (ChatScreen.InvokeRequired)
                        {
                            ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { "Client Disconnected\nAll Pipes Have Closed" }); //Print the message with safe thread call 
                        }
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

            if (print == "Client Disconnected\nAll Pipes Have Closed" || print == "Disconnected Successfully")
            {
                ServerButton.Enabled = true;
                HostButton.Enabled   = true;
                isItServer   = false;
                ableToPost   = false;

                Send.Enabled = false;
                Disconnect.Enabled = false;

                PipeName_One.Text  = "";
                UserName.Text      = "";
                MachineServer.Text = "";

                PipeName_One.ReadOnly = false;
                UserName.ReadOnly = false; 

                ChatScreen.Text = ""; //Clear the chat screen

            }

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
        
            //Exiting the threads and program entirely
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            if (ableToPost)
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
        } 
    }
}


