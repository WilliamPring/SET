using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOA_Clinet
{
    class QueryServiceResponse
    {
        public string type;
        public string pos;
        public string name;
        public string datatype;
        public string required;
        public string value;
        public QueryServiceResponse(string type, string pos, string name, string datatype, string required)
        {
            this.type = type;
            this.pos = pos;
            this.name = name;
            this.datatype = datatype;
            this.required = required;
        }
        public QueryServiceResponse(string type, string pos, string name, string datatype)
        {
            this.type = type;
            this.pos = pos;
            this.name = name;
            this.datatype = datatype;
        }
    }
}
