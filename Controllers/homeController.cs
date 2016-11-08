using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace hypster.Controllers
{
    public class homeController : ControllerBase
    {
        //
        // GET: /home/

        public ActionResult Index()
        {
            //hypster.ViewModels.homeViewModel model = new ViewModels.homeViewModel();

            //hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            //model.most_popular_songs = songManager.Get_MostPopularSong_Random();
            //hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            //model.visualSearch_list = visualSearchManager.getVisualSearchArtists_cached();
            //return View(model);

            return View();
        }



        [OutputCache(Duration = 240, VaryByParam = "none")]
        public ActionResult visualSearchBar()
        {
            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            List<hypster_tv_DAL.VisualSearch> model = visualSearchManager.getVisualSearchArtists_cached();


            return View(model);
        }



        [OutputCache(Duration = 240, VaryByParam = "none")]
        public ActionResult Get_MostPopularSong_Random()
        {
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> model = songManager.Get_MostPopularSong_Random();


            return View(model);
        }


    }
}
