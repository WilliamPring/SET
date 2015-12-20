/*    
 * Filename: Message.cs
 * Assignment: WMP Final Project 
 * By: Naween Mehanmal and William Pring, Denys Politiuk
 * Date: December 16, 2015
 * Description: The file contains class that is used to pass messages between two clients
 *
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace CheckersClient
{
    /// <summary>
    /// Message class that contains data
    /// to make a move from one client on another
    /// </summary>
    [DataContract]
    public class Message
    {
        private const int size = 1024;

        [DataMember]
        public bool color;
        [DataMember]
        public bool king;
        [DataMember]
        public int currentX;
        [DataMember]
        public int currentY;
        [DataMember]
        public int newX;
        [DataMember]
        public int newY;


        /// <summary>
        /// Serializing method for the class
        /// </summary>
        /// <param name="msg">object of the class</param>
        /// <returns></returns>
        static public string Serialize(Message msg)
        {
            string array;

            using (MemoryStream stream = new MemoryStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Message));
                    serializer.WriteObject(stream, msg);
                    stream.Position = 0;
                    //array = stream.ToArray();
                    array = reader.ReadToEnd();
                }
            }

            return array;
        }

        /// <summary>
        /// Deserialize method for the class
        /// </summary>
        /// <param name="array">Serialized object</param>
        /// <returns></returns>
        static public Message Deserialize(string array)
        {
            Message msg = new Message();

            using (Stream stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(array);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                DataContractSerializer deserializer = new DataContractSerializer(typeof(Message));
                msg = (Message)deserializer.ReadObject(stream);
            }

            return msg;
        }
    }
}
