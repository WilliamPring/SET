using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for TickerTape
/// </summary>
[WebService(Namespace = "http://localhost/webservice")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TickerTape : System.Web.Services.WebService
{

    ExceptionError eError = new ExceptionError();

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
            try
            {
                string errorString = "Invalid Ticker Format!";
                string moreDetailError = "Ticker Symbol cannot have Symbols or cannot be empty";
                eError.ThrowCustomSoapException(errorString, moreDetailError, Context.Request.Url.AbsoluteUri);
            }
            catch (SoapException se)
            {
                throw se;
            }
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
                try
                {
                    // Input string .
                    string errorString = "Input Ticker was not in a correct!";
                    string moreDetailError = "Ticker Symbol was invalid!";
                    eError.ThrowCustomSoapException(errorString, moreDetailError, Context.Request.Url.AbsoluteUri);
                }
                catch (SoapException sexp)
                {
                    throw sexp;
                }
            }

        }





        return qi;

    }
}
