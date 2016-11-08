using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.YouTube;

namespace hypster.Controllers
{
    public class songDetailsController : ControllerBase
    {



        //----------------------------------------------------------------------------------------------------------
        // song landing page dynamic content
        //
        public ActionResult GetSongDynamicDetails()
        {

            // 1.parse query string
            //-----------------------------------------------------------------------------------------------------
            string song_guid = "";
            if (Request.QueryString["song_guid"] != null)
            {
                song_guid = Request.QueryString["song_guid"];
            }
            //-----------------------------------------------------------------------------------------------------


            // 2.get video dynamic details
            //-----------------------------------------------------------------------------------------------------
            Video video = new Video();
            try
            {
                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);
                string feedUrl = "http://gdata.youtube.com/feeds/api/videos/" + song_guid;
                video = request.Retrieve<Video>(new Uri(feedUrl));
            }catch(Exception ex){}
            //-----------------------------------------------------------------------------------------------------




            return View(video);
        }
        //----------------------------------------------------------------------------------------------------------








        //----------------------------------------------------------------------------------------------------------
        [HttpPost]
        public ActionResult PostSongComment(string songComment, string songGuid)
        {
            if (User.Identity.IsAuthenticated == false)
                return RedirectPermanent("/account/SignIn?ReturnUrl=/song/" + songGuid);


            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Song curr_song = new hypster_tv_DAL.Song();
            curr_song = songManager.GetSongByID(songGuid);



            hypster_tv_DAL.songComment p_comment = new hypster_tv_DAL.songComment();
            p_comment.song_ID = curr_song.id;

            p_comment.ipAddress = IpAddress();
            p_comment.status = (int)hypster_tv_DAL.newsCommentStatus.NoActive;
            p_comment.user_ID = memberManager.getMemberByUserName(User.Identity.Name).id;
            p_comment.userName = User.Identity.Name;
            p_comment.postDate = DateTime.Now;

            
            p_comment.comment = songComment;
            if(p_comment.comment.Length > 1990)
                p_comment.comment = p_comment.comment.Substring(0,1990);


            hyDB.songComments.AddObject(p_comment);
            hyDB.SaveChanges();


            return RedirectPermanent("/song/" + songGuid);
        }
        //----------------------------------------------------------------------------------------------------------













        //----------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Service function - gives user ip address
        /// </summary>
        /// <returns></returns>
        private string IpAddress()
        {
            string strIpAddress;
            strIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (strIpAddress == null) strIpAddress = Request.ServerVariables["REMOTE_ADDR"];
            return strIpAddress;
        }
        //----------------------------------------------------------------------------------------------------------





    }
}
