using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using NLog;


[WebService(Namespace = "http://localhost/webservice/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class TextService : System.Web.Services.WebService
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    ExceptionError eError = new ExceptionError();
    public TextService() {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string CaseConvert(string incoming, uint flag)
    {
        string returnStringValue = "";
        string moreDetailError = "";
        string errorString = "";
        if (flag ==1)
        {
            returnStringValue = incoming.ToUpper();
        }
        else if (flag == 2)
        {
            returnStringValue = incoming.ToLower();
        }
        else
        {
            try
            {
                errorString = "Flag out of bound";
                moreDetailError = "Invalid Case Flag! Flag = 1 to Capalizes all string, Flag = 2 to lower case all the string";
                eError.ThrowCustomSoapException(errorString, moreDetailError, Context.Request.Url.AbsoluteUri);
            }
            catch(SoapException se)
            {
                logger.Error(se);

                throw se;
            }


        }
        return returnStringValue;
    }



}