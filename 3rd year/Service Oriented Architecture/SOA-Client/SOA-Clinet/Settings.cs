using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_Clinet
{
    public class ClientSetting
    {
        public string IPAddress { get; set; }
        public string RegisterPort { get; set; }
        public string TeamName { get; set; }
        public string TeamId { get; set; }
        public string TagName { get; set; }
    }

    public class ServiceToConsume
    {
        public string ServiceName { get; set; }
        public string argumentNumbers { get; set; }
        public string ipaddress { get; set; }
        public string port { get; set; }
    }

    public class RootObject
    {
        public ClientSetting clientSetting { get; set; }
        public ServiceToConsume ServiceToConsume { get; set; }
    }
}
