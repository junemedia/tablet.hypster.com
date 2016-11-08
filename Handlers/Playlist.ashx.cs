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
    public class Playlist : IHttpHandler
    {




        public void ProcessRequest(HttpContext context)
        {
            int userId = -1;
            int playlistId = -1;
            bool isRandom = false;
            //--------------------------------------------------------------------------------------------------------------------



            // old logic has this (do not remove)
            //--------------------------------------------------------------------------------------------------------------------
            if (!int.TryParse(context.Request.QueryString["id"], out playlistId))
            {
                // try parse in [user_id]:[playlist_id]:[is_random] format
                if (context.Request.QueryString["id"].Contains(":") == false)
                    return; // exit if : char isnt present

                var data = context.Request.QueryString["id"].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (data.Count() != 3)
                    return; // exit if param count doesn't equal 3

                userId = int.Parse(data[0]);
                playlistId = int.Parse(data[1]);
                isRandom = data[2] == "0" ? false : true;
            }

            if (!int.TryParse(context.Request.QueryString["us_id"], out userId))
            {
                userId = 0;
            }
            //--------------------------------------------------------------------------------------------------------------------





            //--------------------------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManagement = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.PlaylistData_Song> playlist_songs = new List<hypster_tv_DAL.PlaylistData_Song>();
            //--------------------------------------------------------------------------------------------------------------------






            //--------------------------------------------------------------------------------------------------------------------
            XElement xml;
            XNamespace jwNS = "http://developer.longtailvideo.com/trac/wiki/FlashFormats";
            int i = 1;

            //need to shuffle if set
            if ((context.Request.QueryString["shuffle"] != null && context.Request.QueryString["shuffle"].Equals("true", StringComparison.CurrentCultureIgnoreCase)) || isRandom)
            {
                playlist_songs = playlistManagement.GetPlayListDataByPlaylistID_Random(playlistId);
            }
            else
            {
                playlist_songs = playlistManagement.GetPlayListDataByPlaylistID(playlistId);
            }
            //--------------------------------------------------------------------------------------------------------------------






            //prepare playlist songs
            //--------------------------------------------------------------------------------------------------------------------
            ArrayList tracks_list_xml = new ArrayList();
            foreach (hypster_tv_DAL.PlaylistData_Song item in playlist_songs)
            {
                XElement songs_xml = new XElement("track",
                    new XAttribute("id", item.playlist_track_id),
                    new XElement("youtubeId", item.YoutubeId ?? "null"),
                    new XElement("type", (item.YoutubeId == "") ? "mp3" : "youtube"),
                    new XElement("title", item.FullTitle),
                    new XElement("link", "http://www.hypster.com/song/" + item.playlist_track_id.ToString()),
                    new XElement("location", (item.YoutubeId == "") ?
                        "http://www.hypster.com/Handlers/Stream.ashx?id=" + item.playlist_track_id.ToString() + "&pid=" + playlistId + "&type=.mp3"
                        :
                        "http://www.youtube.com/watch?v=" + item.YoutubeId + "&type=youtube")
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




        #region TEMP_PENDING_DELETE
        //public void ProcessRequest(HttpContext context)
        //{
        //int playlistId = -1;
        //bool isRandom = false;

        //if (!int.TryParse(context.Request.QueryString["id"], out playlistId))
        //{
        //    // try parse in [user_id]:[playlist_id]:[is_random] format

        //    if (context.Request.QueryString["id"].Contains(":") == false)
        //        return; // exit if : char isnt present

        //    var data = context.Request.QueryString["id"].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

        //    if (data.Count() != 3)
        //        return; // exit if param count doesn't equal 3

        //    playlistId = int.Parse(data[1]);
        //    isRandom = data[2] == "0" ? false : true;
        //}

        //IHypsterRepository hypsterRepository = new HypsterRepository();
        //PlaylistObject playlist = hypsterRepository.GetPlaylistById(playlistId);

        //if (playlist == null)
        //    return;


        //// retrieve member data
        //var user = hypsterRepository.GetMemberById(playlist.UserId);

        //XElement xml;
        //XNamespace jwNS = "http://developer.longtailvideo.com/trac/wiki/FlashFormats";
        //int i = 1;

        //if ((context.Request.QueryString["shuffle"] != null && context.Request.QueryString["shuffle"].Equals("true", StringComparison.CurrentCultureIgnoreCase)) || isRandom)
        //{
        //    playlist.Songs = playlist.Songs.OrderBy( d=> Guid.NewGuid()).ToList();
        //}

        //xml = new XElement("playlist", new XAttribute(XNamespace.Xmlns + "jwplayer", jwNS.NamespaceName),

        //    new XElement("tracklist", from p in playlist.Songs
        //        where (context.Request.QueryString["nv"] == null || (p.YoutubeId == null || p.YoutubeProcessed == true))
        //        select new XElement("track",
        //            new XAttribute("id", i++),
        //            new XElement("youtubeId", p.YoutubeId ?? "null"),
        //            new XElement("type", p.YoutubeId.IsNullOrEmpty() ? "mp3" : "youtube"),
        //            new XElement("title", p.FullTitle),
        //            new XElement("link", "http://www.hypster.com/song/" + p.SongId.ToString()),
        //            new XElement("location", p.YoutubeId.IsNullOrEmpty() ? "http://www.hypster.com/Handlers/Stream.ashx?id=" + 
        //                p.SongId.ToString() + "&pid=" + p.Id.ToString() + "&type=.mp3" : "http://www.youtube.com/watch?v=" + 
        //                p.YoutubeId + "&id=" + p.SongId.ToString() + "&pid=" + p.Id.ToString() + "&type=youtube"))),
        //                new XElement("data",
        //        new XElement("title", user.username + " - Hypster Radio"),
        //        new XElement("username", user.username),
        //        new XElement("photo", user.profile_pic_id != 0 ? "http://pics.hypster.com/userpics/" + user.profile_pic_id.ToString() + "_s.jpg" : "http://pics.hypster.com/images/nophoto.jpg"),
        //        new XElement("truename", user.name),
        //        new XElement("gender", user.sex == 0 ? "Male" : "Female"),
        //        new XElement("country", user.country),
        //        new XElement("city", user.city),
        //        new XElement("introduce", user.introduce)));


        //context.Response.ContentType = "text/xml";
        //context.Response.Write(xml.ToString(SaveOptions.DisableFormatting));
        //}
        #endregion




        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
