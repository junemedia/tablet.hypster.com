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
    //
    // controller for music search
    public class searchController : ControllerBase
    {
        //----------------------------------------------------------------------------------------------------------
        private int MAX_RECENT_SEARCHES_NUM = 255;
        //----------------------------------------------------------------------------------------------------------




       

        //----------------------------------------------------------------------------------------------------------
        // perform youtube search
        public ActionResult Music()
        {
            if (Request.QueryString["ss"] != null)
            {
                //---------------------------------------------------------------------
                string search_string = Request.QueryString["ss"].ToString();
                ViewBag.search_string = search_string.Replace(" ", "+");
                //---------------------------------------------------------------------



                //---------------------------------------------------------------------
                int Curr_Page = 1;
                if (Request.QueryString["page"] != null)
                {
                    if (Int32.TryParse(Request.QueryString["page"], out Curr_Page) == false)
                        Curr_Page = 1;
                }
                ViewBag.CurrPage = Curr_Page;
                //---------------------------------------------------------------------



                // "orderBy"
                //---------------------------------------------------------------------
                string orderBy = "";
                if (Request.QueryString["orderBy"] != null)
                {
                    orderBy = Request.QueryString["orderBy"].ToString();
                }
                ViewBag.orderBy = orderBy;
                //---------------------------------------------------------------------

                



                //---------------------------------------------------------------------
                #region save_recent_searches_to_application-varibles
                //save recent searches to application varibles
                if (HttpContext.Application["RECENT_SEARCHES"] != null)
                {
                    List<string> recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];
                    recent_searches.Add(search_string);
                    if (recent_searches.Count > MAX_RECENT_SEARCHES_NUM)
                        recent_searches.RemoveAt(recent_searches.Count - 1);

                    HttpContext.Application["RECENT_SEARCHES"] = recent_searches;
                }
                else
                {
                    List<string> recent_searches = new List<string>();
                    recent_searches.Add(search_string);

                    HttpContext.Application["RECENT_SEARCHES"] = recent_searches;
                }

                if(HttpContext.Application["RECENT_SEARCHES"] != null)
                    ViewBag.recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];
                #endregion
                //---------------------------------------------------------------------




                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);

                //order by relevance
                //string feedUrl = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}&orderby=relevance", HttpUtility.UrlEncode(search_string.Replace("+"," ")), Request.ServerVariables["REMOTE_ADDR"], (Curr_Page * 25) + 1);  //(page.HasValue ? page * 25 : 0) + 1);
                //order by viewCount
                //string feedUrl = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}&orderby=viewCount", HttpUtility.UrlEncode(search_string.Replace("+"," ")), Request.ServerVariables["REMOTE_ADDR"], (Curr_Page * 25) + 1);  //(page.HasValue ? page * 25 : 0) + 1);
                //Feed<Video> videoFeed = null;


                //add orderBy if selected
                if (orderBy != "")
                    orderBy = "&orderby=" + orderBy;

                //original sorting order
                string feedUrl = String.Format("http://gdata.youtube.com/feeds/api/videos?q={0}&category=Music&format=5&restriction={1}&safeSearch=none&start-index={2}" + orderBy, HttpUtility.UrlEncode(search_string.Replace("+", " ")), Request.ServerVariables["REMOTE_ADDR"], (Curr_Page * 25) - 25 + 1);
                Feed<Video> videoFeed = null;


                try
                {
                    videoFeed = request.Get<Video>(new Uri(feedUrl));
                }
                catch (Exception ex)
                {
                }

                
                return View(videoFeed);
            } // NEED TO CHECK AND FIX THIS SECTION
            else
            {
                if (HttpContext.Application["RECENT_SEARCHES"] != null)
                    ViewBag.recent_searches = (List<string>)HttpContext.Application["RECENT_SEARCHES"];

                return View();
            }
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        // perform youtube search
        public ActionResult MusicYTID()
        {
            string search_string = "";
            if (Request.QueryString["ss"] != null)
            {
                //---------------------------------------------------------------------
                search_string = Request.QueryString["ss"].ToString();
                ViewBag.search_string = search_string.Replace(" ", "+");
                //---------------------------------------------------------------------
            }


            // 2.get video dynamic details
            //-----------------------------------------------------------------------------------------------------
            Video video = null;
            try
            {
                YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                YouTubeRequest request = new YouTubeRequest(settings);
                string feedUrl = "http://gdata.youtube.com/feeds/api/videos/" + HttpUtility.UrlEncode(search_string.Replace("+"," "));
                video = request.Retrieve<Video>(new Uri(feedUrl));

            }
            catch (Exception ex) { }
            //-----------------------------------------------------------------------------------------------------


            return View(video);
        }
        //----------------------------------------------------------------------------------------------------------






    }
}
