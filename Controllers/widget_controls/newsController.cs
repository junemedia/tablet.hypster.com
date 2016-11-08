using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class newsController : ControllerBase
    {

        
        // cache applied to data select as well
        //----------------------------------------------------------------------------------------------------------
        [OutputCache(Duration = 60, VaryByParam = "none")]
        public ActionResult GetLatestNews()
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.newsPost> news_list = new List<hypster_tv_DAL.newsPost>();
            List<hypster_tv_DAL.newsPost> news_list_Display = new List<hypster_tv_DAL.newsPost>();
            news_list = newsManager.GetLatestNews_cache(30); //num of posts



            int maxPostsNum = 6; //num of posts
            if (news_list.Count > maxPostsNum)
            {
                Random randomGen = new Random();
                do
                {
                    int next_article = randomGen.Next(0, news_list.Count);

                    hypster_tv_DAL.newsPost item = new hypster_tv_DAL.newsPost();
                    item = news_list[next_article];

                    if (!news_list_Display.Contains(item))
                        news_list_Display.Add(item);


                } while (news_list_Display.Count < maxPostsNum);
            }
            //-----------------------------------------------------------------------------------------------------



            return View(news_list_Display);
        }
        //----------------------------------------------------------------------------------------------------------




    }
}
