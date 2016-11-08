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
//using Hypster.Models;

namespace Hypster.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class SinglePlaylist : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            //--------------------------------------------------------------------------------------------------------------------
            string id = context.Request.QueryString["id"].ToString();
            //--------------------------------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.Song item = new hypster_tv_DAL.Song();
            item = songManager.GetSongByGUID(id);
            //--------------------------------------------------------------------------------------------------------------------





            //--------------------------------------------------------------------------------------------------------------------
            XElement xml;
            XNamespace jwNS = "http://developer.longtailvideo.com/trac/wiki/FlashFormats";

            xml = new XElement("playlist", new XAttribute(XNamespace.Xmlns + "jwplayer", jwNS.NamespaceName), new XElement("title", "Hypster Radio"),
                  new XElement("tracklist", new XElement("track",
                                              new XAttribute("id", item.id),
                                              new XElement("title", HttpUtility.HtmlEncode(item.FullTitle)),
                                              new XElement("location", (item.YoutubeId == "") ?
                                                                                    "http://www.hypster.com/Handlers/Stream.ashx?id=" + item.id + "&pid=0&type=.mp3" 
                                                                                    :
                                                                                    "http://www.youtube.com/watch?v=" + item.YoutubeId + "&type=youtube")
                                              )));
            //--------------------------------------------------------------------------------------------------------------------





            //--------------------------------------------------------------------------------------------------------------------
            context.Response.ContentType = "text/xml";
            context.Response.Write(xml.ToString());
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
