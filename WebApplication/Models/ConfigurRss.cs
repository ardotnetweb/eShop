using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace WebApplication.RssApplication
{
    [XmlRoot("rss", Namespace = "http://www.w3.org/2005/Atom")]
    public class RSS
    {
        [XmlElement("channel")]
        public Chanel Chanel { get; set; }

        public string CalculateDate(DateTime dt)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');
            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }
            string PopulationDate = dt.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'));
            return PopulationDate;
        }

    }
    public class Chanel
    {

        private ArrayList map;

        public Chanel()
        {
            map = new ArrayList();
        }

        [XmlElement("title")]
        public string Title { get; set; }


        [XmlElement("link")]
        public string Link { get; set; }



        [XmlElement("description ")]
        public string Description { get; set; }


        [XmlElement("language")]
        public string Language { get; set; }


        [XmlElement("lastBuildDate")]
        public string LastBuildDate { get; set; }


        [XmlElement("item")]
        public ContentRss[] Item
        {
            get
            {
                ContentRss[] items = new ContentRss[map.Count];
                map.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null)
                    return;
                ContentRss[] items = (ContentRss[])value;
                map.Clear();
                foreach (ContentRss item in items)
                    map.Add(item);
            }
        }

        public int Add(ContentRss item)
        {
            return map.Add(item);
        }

    }
    public class ContentRss
    {
        public ContentRss() { }
        public ContentRss(DateTime dt)
        {
            int offset = TimeZone.CurrentTimeZone.GetUtcOffset(dt).Hours;
            string timeZone = "+" + offset.ToString().PadLeft(2, '0');
            if (offset < 0)
            {
                int i = offset * -1;
                timeZone = "-" + i.ToString().PadLeft(2, '0');
            }
            PopulationDate = dt.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'));
        }
        [XmlElement("link")]
        public string Link { get; set; }



        [XmlElement("title")]
        public string Title { get; set; }



        [XmlElement("description")]
        public string Description { get; set; }



        [XmlElement("pubDate")]
        public string PopulationDate { get; set; }

    }
}