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

namespace ChatProgram
{
    public partial class Form1 : Form
    {
        string userName;
        string computerName;
        string printToScreen;
        string tempToTest;
        delegate void updateTextBox(string text); //To update the textbox from a NON-UI thread 

        Thread newthread; 

        IPCMQClient queueName;
        System.Messaging.Message msg;
    
        public Form1()
        {
            InitializeComponent();
            ChatScreen.ReadOnly = true;
            TextScreen.ReadOnly = true;
            Connect.Enabled     = false;

            //Instantiating message class to allow to send messages to the message queue 
            msg = new System.Messaging.Message();

            //For the user name and path of the queue 
            userName      = "";
            printToScreen = ""; 
            computerName  = "";

            newthread = new Thread(new ThreadStart(RequestChatLog)); //Method invoked when thread is created 
        }
 
        private void LocalComputer_TextChanged(object sender, EventArgs e)
        {
            //Only submit when the user has entered something in the Local Computer textbox 
            if(LocalComputer.Lines.Length != 0)
            {
                Connect.Enabled = true; 
            }
            else
            {
                Connect.Enabled = false;
            }
        }

        private void Connect_Click_1(object sender, EventArgs e)
        {
            string[] user = UserName.Lines;
            TextScreen.ReadOnly = false;


            for (int i = 0; i < user.Length; i++)
            {
                userName += user[i]; 
            }

            if(userName == "")
            {
                userName = "Anonymous"; 
            }

            Connect.Enabled        = false;
            UserName.ReadOnly      = true;
            LocalComputer.ReadOnly = true;

            computerName = LocalComputer.Text;
            queueName = new IPCMQClient(computerName);
            newthread.Start(); 
        }


        /*************** UI MAINT THREAD Send to Queue ***************/
        
        private void Send_Click(object sender, EventArgs e)
        {
            //Add the path right now for testing purposes
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
                ChatScreen.Text += userName + " >> " + printToScreen + "\n";
                msg.Body = userName + " >> " + printToScreen + "\n"; //Append the string to the Msg body to be sent off to the other machine 
                tempToTest = userName + " >> " + printToScreen + "\n";
                //First send
                queueName.SendWrite((string)msg.Body); //Send the message off to the other machine, cast object to string  
            }
        }


        
        /*************** Non-UI Thread Reading From Queue ***************/
        
        private void RequestChatLog()
        {
            bool finished = false; 

            while(!finished)
            {
                tempToTest+= "";

                
                printToScreen = queueName.RequestWrite();
                if (printToScreen != null)
                {
                    if (printToScreen == "shutdown")
                    {
                        break; //Break out of the loop, stop reading
                    }

                    if (printToScreen == "")
                    {
                        continue;
                    }

                    if (ChatScreen.InvokeRequired)
                    {
                        ChatScreen.BeginInvoke(new updateTextBox(writeToScreen), new object[] { printToScreen }); //Print the message with safe thread call 
                    }
                }
                
                
            }           

        }

        private void writeToScreen(object text) //Used from the NON-UI Threads 
        {
            string print = (string)text;
            ChatScreen.Text += print + "\n";
        }
    }
}
