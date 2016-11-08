using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class videosController : ControllerBase
    {


        //----------------------------------------------------------------------------------------------------------
        //Video Widget
        //
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult GetSideVideos()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.videoFeaturedManager videoManager = new hypster_tv_DAL.videoFeaturedManager();
            List<hypster_tv_DAL.videoFeaturedSlideshow> videos_list = videoManager.getFeaturedVideos();
            //-----------------------------------------------------------------------------------------------------

            return View(videos_list);
        }
        //----------------------------------------------------------------------------------------------------------




    }
}
