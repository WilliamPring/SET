using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

/// <summary>
/// Summary description for ExceptionError
/// </summary>
public class ExceptionError
{

    public ExceptionError()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Taken from: https://msdn.microsoft.com/en-us/library/6d0x301k(v=vs.100).aspx
    /// </summary>
    public void ThrowCustomSoapException(string errorString, string moreDetailError, string url)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element,
             SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

        // Build specific details for the SoapException.
        // Add first child of detail XML element.
        System.Xml.XmlNode details =
          doc.CreateNode(XmlNodeType.Element, "DetailError",
                         "http://tempuri.org/");
        details.InnerText = moreDetailError;
        node.AppendChild(details);

        //Throw the exception    
        SoapException se = new SoapException(errorString,
          SoapException.ClientFaultCode,
          url,
          node);

        throw se;
    }
}