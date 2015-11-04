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
        MessageQueue mq; //The message being sent 
        queueProtocol package;
        queueProtocol protocol;
        string retMessage;

        /** Property **/
        public MessageQueue MQ
        {
            get
            {
                return mq; 
            }
        }

        public IPCMQClient(string machine)
        {
            package = new queueProtocol();     

            try 
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
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }                   
        }



        /** Send/Recieve Packages to Queue **/

        public void SendWrite(string data)
        {
            package.message = data; 
            mq.Send(package); //Send the package object
            mq.Close();
        }      

        public string RequestWrite()
        {

            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(queueProtocol) });
            protocol = (queueProtocol)mq.Receive().Body; 

            if(package.message != protocol.message)
            {
                retMessage = protocol.message;
                mq.Close(); 
            }
            return retMessage; 
        }
    }
}
