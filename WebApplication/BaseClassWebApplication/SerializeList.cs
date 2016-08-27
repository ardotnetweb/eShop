using eShop.WebApplication.DomainClasses.ViewModelClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication.BaseClassWebApplication
{
    public static class SerializeList
    {
        public static string SeralizeList(List<BasketViewModel> lstBasket)
        {
            XmlSerializer xmlSer = new XmlSerializer(lstBasket.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSer.Serialize(textWriter, lstBasket);
            xmlSer = null;
            return textWriter.ToString();
        }
        public static List<BasketViewModel> DeseraLizeList(string lstBasket)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(List<BasketViewModel>));
            TextReader reader = new StringReader(lstBasket);
            object obj = xmlSer.Deserialize(reader);
            return (List<BasketViewModel>)obj;
        }
    }
}