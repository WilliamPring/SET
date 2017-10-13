using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;


[WebService(Namespace = "http://localhost/webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class TextService : System.Web.Services.WebService
{
    //private static Logger logger = LogManager.GetCurrentClassLogger();

    public TextService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string CaseConvert(string incoming, uint flag)
    {
        string returnStringValue = "";
        if (flag == 1)
        {
            returnStringValue = incoming.ToUpper();
        }
        else if (flag == 2)
        {
            returnStringValue = incoming.ToLower();
        }
        else
        {
            string errorMessage = "Flag out of bound! Invalid Case Flag! Flag = 1 to Capalizes all string, Flag = 2 to lower case all the string";


            using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileTextService.txt"), true))
            {
                file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic string CaseConvert(string incoming, uint flag) \n" + "Error Messages: " + errorMessage + "\n");
            }
            throw new SoapException(errorMessage, SoapException.ClientFaultCode);
        }
        return returnStringValue;
    }
}