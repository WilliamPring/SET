using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NLog;
using System.Web.Services.Protocols;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for TickerTape
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class TickerTape : System.Web.Services.WebService
{

    private static Logger logger = LogManager.GetCurrentClassLogger();
    ExceptionError eError = new ExceptionError();

    delayedstockquote.DelayedStockQuote dsq = new delayedstockquote.DelayedStockQuote();
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

        if ((Regex.IsMatch(tickerSymbol, @"^[a-zA-Z]+$") == false))
        {
            try
            {
                string errorString = "Invalid Ticker Format!";
                string moreDetailError = "Ticker Symbol cannot contain numbers and Symbols";
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
                delayedstockquote.QuoteData qd = dsq.GetQuote(tickerSymbol, "0");
                qi.Symbol = qd.StockSymbol;
                qi.LastPrice = System.Convert.ToDouble(qd.LastTradeAmount);
                qi.LastPriceDate = qd.LastTradeDateTime.Date.ToString();
                qi.LastPriceTime = qd.LastTradeDateTime.TimeOfDay.ToString();
            }
            catch (SoapException se)
            {
                logger.Error(se);
                throw se;
            }

        }





        return qi;

    }

}
