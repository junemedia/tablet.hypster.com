using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers.popular
{
    public class popularController : Controller
    {


        //
        // GET: /popular/

        public ActionResult Index()
        {
            return View();
        }




        [OutputCache(Duration = 240, VaryByParam = "none")]
        public ActionResult PopularPlaylists()
        {
            hypster_tv_DAL.playlistManagement playlistsManager = new hypster_tv_DAL.playlistManagement();
            List<hypster_tv_DAL.Playlist> most_viewed_playlists = new List<hypster_tv_DAL.Playlist>();
            most_viewed_playlists = playlistsManager.GetMostViewedPlaylists();


            return View(most_viewed_playlists);
        }




        [OutputCache(Duration = 240, VaryByParam = "none")]
        public ActionResult PopularMusicOnHypster()
        {
            hypster_tv_DAL.songsManagement songManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> most_popular_songs = new List<hypster_tv_DAL.Song>();
            most_popular_songs = songManager.Get_MostPopularSong_Random();


            return View(most_popular_songs);
        }






        [OutputCache(Duration = 240, VaryByParam = "none")]
        public ActionResult PopularVideos()
        {
            List<hypster_tv_DAL.videoClip> TopVideos = new List<hypster_tv_DAL.videoClip>();

            hypster_tv_DAL.videoClipManager videoManager = new hypster_tv_DAL.videoClipManager();
            TopVideos = videoManager.getRandomVideos_cache(8);

            return View(TopVideos);
        }


    }
}
