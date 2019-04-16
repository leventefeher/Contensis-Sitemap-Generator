using System;
namespace ContensisSitemapGenerator
{
    public class ContentType
    {
        public string ApiName { get; set; } //API name, The delivery APi search will search for entries of this content type
        public string FriendlyName { get; set; } //Friendly name for you to refer  to it
        public string Url { get; set; } //The path of the entry, for example if using GUIDs /events/event-story?id= it will append the ID at the end
        public Boolean UsingSlug { get; set; } // Is it using slug, not implemented yet, it the future it will build the URL depending whether it is using slug or no


        public ContentType(string apiName, string friendlyName, string url, Boolean usingSlug)
        {
            ApiName = apiName;
            FriendlyName = friendlyName;
            Url = url;
            UsingSlug = usingSlug;

        }


    }



}

