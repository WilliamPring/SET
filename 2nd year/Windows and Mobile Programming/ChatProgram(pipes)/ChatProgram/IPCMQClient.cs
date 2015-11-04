using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Windows.Forms;
using System.Net;

namespace ChatProgram
{
    class IPCMQClient
    {                
        string mQueueName; //The name of the queue
        string origMessage; 
        MessageQueue mq; //The message being sent 
        System.Messaging.Message myMessage;
                
        /** Property **/
        public MessageQueue MQ
        {
            get
            {
                return mq; 
            }
        }

        /** Constructor **/
        public IPCMQClient(string machine)
        {
            mQueueName = machine + "\\private$\\ipcTest";

            if (!MessageQueue.Exists(mQueueName))
            {
                mq = MessageQueue.Create(mQueueName);
            }
            else
            {
                mq = new MessageQueue(mQueueName);
            }                         
        }

        /** Send/Recieve Packages to Queue **/
        public void SendWrite(string data)
        {
            myMessage = new System.Messaging.Message(data);


            mq.Send(myMessage); //Send the package object
            //MessageBox.Show("Send Message ID: " + myMessage.Id.ToString());
            origMessage = myMessage.Id.ToString(); 
            mq.Close();
        }      

        public string RequestWrite()
        {
            string retMessage = "";
            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            System.Messaging.Message recMessage = mq.Receive();
            //MessageBox.Show("Return Mes ID: " + recMessage.Id.ToString());

            if(origMessage == recMessage.Id.ToString())
            {
                mq.Send(recMessage.Body.ToString()); //Send the package object
                MessageBox.Show("So print nothing");
                retMessage = "";
                mq.Close(); 
            }
            else
            {
                retMessage = recMessage.Body.ToString();
                mq.Close(); 
            }
            return retMessage; 
        }
    }
}
