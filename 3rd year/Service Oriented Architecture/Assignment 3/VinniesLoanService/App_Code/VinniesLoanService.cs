using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for VinniesLoanService
/// </summary>
[WebService(Namespace = "http://localhost/webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VinniesLoanService : System.Web.Services.WebService
{
    // private static Logger logger = LogManager.GetCurrentClassLogger();

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
        if ((principle <= 0) || (rate <= 0) || (payments <= 0))
        {

            string errorMessage = "Values should not be less then or equal to 0! Principle, Rate or Payment cannot not be less then 0";
            using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileVinniesLoanService.txt"), true))
            {
                file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic float LoanPayment(float principle, float rate, int payments) \n" + "Error Messages: " + errorMessage + "\n");
            }

            throw new SoapException(errorMessage, SoapException.ClientFaultCode);
        }
        else
        {
            try
            {
                monthlyPayment = (rate + rate / ((float)(Math.Pow((1 + rate), payments)) - 1)) * principle;
                monthlyPayment = (float)Math.Round(monthlyPayment, 2);
                if(float.IsInfinity(monthlyPayment))
                {
                    string errorMessage = "Number to large! The result was not a real number";
                    using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileVinniesLoanService.txt"), true))
                    {
                        file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic float LoanPayment(float principle, float rate, int payments) \n" + "Error Messages: " + errorMessage + "\n");
                    }
                    throw new SoapException(errorMessage, SoapException.ClientFaultCode);
                }

            }
            catch (OverflowException ofe)
            {
                string errorMessage = "Number to large! Principle. Overflow Exception: "  + ofe.Message;
                using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileVinniesLoanService.txt"), true))
                {
                    file2.WriteLine("Time: "+ DateTime.Now.ToString("h:mm:ss tt") + "\npublic float LoanPayment(float principle, float rate, int payments) \n" + "Error Messages: " + errorMessage + "\n");
                }
                throw new SoapException(errorMessage, SoapException.ClientFaultCode);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error server fault. Exception: " + ex.Message;
                using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileVinniesLoanService.txt"), true))
                {
                    file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic float LoanPayment(float principle, float rate, int payments) \n" + "Error Messages: " + errorMessage + "\n");
                }
                throw new SoapException(errorMessage, SoapException.ClientFaultCode);
            }

        }
        return monthlyPayment;
    }

}
