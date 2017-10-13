using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;
using System.IO;

/// <summary>
/// Summary description for TickerTape
/// </summary>
[WebService(Namespace = "http://localhost/webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TickerTape : System.Web.Services.WebService
{
    stockquote.DelayedStockQuote dsq = new stockquote.DelayedStockQuote();
    public struct QuoteInfo
    {
        public string Symbol;
        public double LastPrice;
        public string LastPriceDate;
        public string LastPriceTime;
    }




    public TickerTape()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public QuoteInfo GetQuote(string tickerSymbol)
    {
        QuoteInfo qi = new QuoteInfo();

        if ((Regex.IsMatch(tickerSymbol, @"[^A-Za-z0-9]+")) || (tickerSymbol == ""))
        {

            string errorMessage = "Invalid Ticker Format! Ticker Symbol cannot have Symbols or cannot be empty";
            using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileTickerTape.txt"), true))
            {
                file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic QuoteInfo GetQuote(string tickerSymbol) \n" + "Error Messages: " + errorMessage + "\n");
            }
            throw new SoapException(errorMessage, SoapException.ClientFaultCode);
        }

        else
        {

            try
            {
                stockquote.QuoteData qd = dsq.GetQuote(tickerSymbol, "0");
                qi.Symbol = qd.StockSymbol;
                qi.LastPrice = System.Convert.ToDouble(qd.LastTradeAmount);
                qi.LastPriceDate = qd.LastTradeDateTime.Date.ToString();
                qi.LastPriceTime = qd.LastTradeDateTime.TimeOfDay.ToString();
            }
            catch (SoapException se)
            {
                string errorMessage = "Input Ticker was not in a correct! Ticker Symbol was invalid!";
                using (StreamWriter file2 = new StreamWriter(Server.MapPath("LogFileTickerTape.txt"), true))
                {
                    file2.WriteLine("Time: " + DateTime.Now.ToString("h:mm:ss tt") + "\npublic QuoteInfo GetQuote(string tickerSymbol) \n" + "Error Messages: " + errorMessage + "\n");
                }
                throw new SoapException(errorMessage, SoapException.ClientFaultCode);
            }

        }
        return qi;

    }
}
