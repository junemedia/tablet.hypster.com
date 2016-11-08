using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;


namespace hypster.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class RadioPlaylist : IHttpHandler
    {




        public void ProcessRequest(HttpContext context)
        {
            int userId = -1;
            string genre = "";
            string page = "";
            string search = "";


            if (context.Request.QueryString["genre"] != null)
            {
                genre = context.Request.QueryString["genre"];
            }

            if (context.Request.QueryString["page"] != null)
            {
                page = context.Request.QueryString["page"];
            }

            if (context.Request.QueryString["search"] != null)
            {
                search = context.Request.QueryString["search"];
            }
            //--------------------------------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------------------------------
            //current radio logic
            //if search is (1) then straight go to youtube api if not 
            //then try to find radio playlist accorging to genre
            //--------------------------------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------------------------------
            YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
            YouTubeRequest request = new YouTubeRequest(settings);


            //string feedUrl = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}&orderby=relevance", HttpUtility.UrlEncode(genre), context.Request.ServerVariables["REMOTE_ADDR"], 1);
            //string feedUrl = "http://gdata.youtube.com/feeds/api/videos/-/music/pop?v=2";
            //YouTubeQuery query = new YouTubeQuery();
            //request.GetVideoForActivity(
            //string feedUrl = "https://gdata.youtube.com/feeds/api/standardfeeds/most_popular";
            //string feedUrl = "http://gdata.youtube.com/feeds/api/videos/-/music/" + genre + "?v=2&start-index=1&orderby=viewCount";





            int this_page = 1;


            string feedUrl = "http://gdata.youtube.com/feeds/api/videos?q=" + genre + "&category=Music&format=5&restriction=" + context.Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + this_page + "&orderby=viewCount";
            Feed<Video> videoFeed = request.Get<Video>(new Uri(feedUrl));
            //--------------------------------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------------------------------
            XElement xml;
            XNamespace jwNS = "http://developer.longtailvideo.com/trac/wiki/FlashFormats";
            //--------------------------------------------------------------------------------------------------------------------



            //prepare playlist songs
            //--------------------------------------------------------------------------------------------------------------------
            ArrayList tracks_list_xml = new ArrayList();
            foreach (Video item in videoFeed.Entries)
            {
                XElement songs_xml = new XElement("track",
                    new XAttribute("id", "0"),
                    new XElement("youtubeId", item.VideoId ?? "null"),
                    new XElement("type", (item.VideoId == "") ? "mp3" : "youtube"),
                    new XElement("title", item.Title),
                    new XElement("link", ""),
                    new XElement("location", "")
                    );
                tracks_list_xml.Add(songs_xml);
            }
            //--------------------------------------------------------------------------------------------------------------------
            

            
            
            this_page += 1;
            string feedUrl_2 = "http://gdata.youtube.com/feeds/api/videos?q=" + genre + "&category=Music&format=5&restriction=" + context.Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + (this_page * 25) + "&orderby=viewCount";
            Feed<Video> videoFeed_2 = request.Get<Video>(new Uri(feedUrl_2));

            //--------------------------------------------------------------------------------------------------------------------
            //prepare playlist songs
            //--------------------------------------------------------------------------------------------------------------------
            foreach (Video item in videoFeed_2.Entries)
            {
                XElement songs_xml = new XElement("track",
                    new XAttribute("id", "0"),
                    new XElement("youtubeId", item.VideoId ?? "null"),
                    new XElement("type", (item.VideoId == "") ? "mp3" : "youtube"),
                    new XElement("title", item.Title),
                    new XElement("link", ""),
                    new XElement("location", "")
                    );
                tracks_list_xml.Add(songs_xml);
            }
            //--------------------------------------------------------------------------------------------------------------------





            this_page += 3;
            string feedUrl_3 = "http://gdata.youtube.com/feeds/api/videos?q=" + genre + "&category=Music&format=5&restriction=" + context.Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + (this_page * 25) + "&orderby=viewCount";
            Feed<Video> videoFeed_3 = request.Get<Video>(new Uri(feedUrl_3));

            //--------------------------------------------------------------------------------------------------------------------
            //prepare playlist songs
            //--------------------------------------------------------------------------------------------------------------------
            foreach (Video item in videoFeed_3.Entries)
            {
                XElement songs_xml = new XElement("track",
                    new XAttribute("id", "0"),
                    new XElement("youtubeId", item.VideoId ?? "null"),
                    new XElement("type", (item.VideoId == "") ? "mp3" : "youtube"),
                    new XElement("title", item.Title),
                    new XElement("link", ""),
                    new XElement("location", "")
                    );
                tracks_list_xml.Add(songs_xml);
            }
            //--------------------------------------------------------------------------------------------------------------------








            //populate playlist
            //--------------------------------------------------------------------------------------------------------------------
            xml = new XElement("playlist", new XAttribute(XNamespace.Xmlns + "jwplayer", jwNS.NamespaceName),
                new XElement("tracklist", tracks_list_xml),
                    new XElement("data",
                    new XElement("title", " - Hypster Radio"),
                    new XElement("username", "N/A"),
                    new XElement("photo", "N/A"),
                    new XElement("truename", "N/A"),
                    new XElement("gender", "N/A"),
                    new XElement("country", "N/A"),
                    new XElement("city", "N/A"),
                    new XElement("introduce", "N/A")));
            //--------------------------------------------------------------------------------------------------------------------



            //--------------------------------------------------------------------------------------------------------------------
            context.Response.ContentType = "text/xml";
            context.Response.Write(xml.ToString(SaveOptions.DisableFormatting));
            //--------------------------------------------------------------------------------------------------------------------
        }






        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }



}