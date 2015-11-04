using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net; 

namespace ChatProgram
{
    public class queueProtocol
    {
        public string message;
        public string uniqueName;

        public queueProtocol()
        {
            this.message = "";
            this.uniqueName = Dns.GetHostName(); //Get the host name of the computer   
        }
    }
}


