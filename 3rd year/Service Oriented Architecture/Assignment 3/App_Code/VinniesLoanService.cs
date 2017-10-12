using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using NLog;

/// <summary>
/// Summary description for VinniesLoanService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VinniesLoanService : System.Web.Services.WebService
{
    private static Logger logger = LogManager.GetCurrentClassLogger();

    ExceptionError eError = new ExceptionError();
    public VinniesLoanService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public float LoanPayment(float principle, float rate, int payments)
    {
        string errorString = "";
        float monthlyPayment = 0;
        if ((principle <= 0) || (rate<=0) || (payments <=0))
        {
            try
            {
                errorString = "Values should not be less then 0";
                string moreDetailError = "Principle, Rate or Payment should not be less then 0";
                eError.ThrowCustomSoapException(errorString, moreDetailError, Context.Request.Url.AbsoluteUri);
            }
            catch (SoapException se)
            {
                logger.Error(se);
                throw se;
            }
        }
        else
        {
            try
            {
                monthlyPayment = (rate + rate / ((float)(Math.Pow((1 + rate), payments)) - 1)) * principle;

            }
            catch(DivideByZeroException ex)
            {
                logger.Error(ex);

                errorString = "Resulted ended in undefinded";
                string moreDetailError = "Cannot divide by zero";
                try
                {
                    eError.ThrowCustomSoapException(errorString, moreDetailError, Context.Request.Url.AbsoluteUri);
                }
                catch (SoapException se)
                {
                    logger.Error(se);
                    throw se;
                }
            }
   
        }
        return monthlyPayment;
    }

}
