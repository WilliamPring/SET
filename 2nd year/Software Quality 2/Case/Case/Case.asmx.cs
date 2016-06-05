using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Case
{
    /// <summary>
    /// Summary description for Case
    /// </summary>
    [WebService(Namespace = "http://localhost/webservice/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Case : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string CaseConvert(string incoming, uint flag)
        {
            if (incoming == "")
            {
                throw new SoapException("Give me something to convert", new System.Xml.XmlQualifiedName("Empty String"));
            }
            else if (flag == 1)
            {
                incoming = incoming.ToUpper();
            }
            else if (flag == 2)
            {
                incoming = incoming.ToUpper();
            }
            else if (flag == 3)
            {
                incoming = Regex.Replace(incoming, @"\s+", "");
            }
            else
            {
                throw new SoapException("Invalid CaseConvert operation : operation = [1 for upper case, 2 for lower case, 3 for collapse space]", new System.Xml.XmlQualifiedName("Empty String"));
            }
            return incoming;
        }
    }
}
