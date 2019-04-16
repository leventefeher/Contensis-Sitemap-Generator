using System;
using System.Collections.Generic;
using Zengenti.Contensis.Delivery;
using Zengenti.Search;
using System.Xml;



namespace ContensisSitemapGenerator
{
    class MainClass
    {
        public static string ApiName { get; set; }
        public static string FriendlyName { get; set; }
        public static string Url { get; set; }
        public static Boolean UsingSlug { get; set; }


        public static void Main()
        {


            Console.WriteLine("Start building Sitemaps");

            List<ContentType> sitemapList = new List<ContentType>();

            //Initiate an object of each content type you want to create a sitemap for
            ContentType person = new ContentType("person", "person", "/people/", true);
            ContentType news = new ContentType("news", "news", "/news/", true);
            ContentType events = new ContentType("events", "events", "/events/", true);

            //Add each object to the for list to run a foreach on
            sitemapList.Add(person);
            sitemapList.Add(news);
            sitemapList.Add(events);

            foreach (ContentType contentType in sitemapList)
            {
                ZengentiSearchFunction(contentType.ApiName, contentType.FriendlyName, contentType.Url, contentType.UsingSlug);

            }
        }



        public static void ZengentiSearchFunction(string apiName, string friendlyName, string url, Boolean usingSlug)
        {
            ApiName = apiName;
            FriendlyName = friendlyName;
            Url = url;
            UsingSlug = usingSlug;



            Console.WriteLine("Starting Content Type type: " + ApiName);
            var query = new Query
            (
                Op.EqualTo("sys.contentTypeId", ApiName)
            );

            //Override the default query page size from 25 to whatever you need
            query.PageSize = 5000;

            //Initiate your connection, see https://developer.zengenti.com/contensis/api/delivery/dotnet/key-concepts/api-instantiation.html
            var client = ContensisClient.Create("");

            var results = client.Entries.Search(query);

            string fileName = friendlyName + "Sitemap.xml";
            using (XmlWriter writer = XmlWriter.Create(fileName))

            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                foreach (var result in results.Items)
                {
                    string location = "";
                    if (usingSlug == true)
                    {
                        location = "https://www.kcl.ac.uk" + url + result.Slug;
                    }
                    else
                    {
                        location = "https://www.kcl.ac.uk" + url + result.Id;
                    }


                    DateTime modified = (System.DateTime)result.Version.Modified;

                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", location);
                    writer.WriteElementString("priority", "0.7");
                    writer.WriteElementString("lastmod", modified.ToString("yyyy-MM-dd"));
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            Console.WriteLine("File: " + fileName + " created");
        }
    }
}