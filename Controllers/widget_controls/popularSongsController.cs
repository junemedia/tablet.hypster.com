using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class popularSongsController : ControllerBase
    {


        //----------------------------------------------------------------------------------------------------------
        // widget
        //
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Widget_MostPopularSongs()
        {
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();
            List<hypster_tv_DAL.Song> songs_list = songsManager.Get_MostPopularSong_Random();

            if(songs_list.Count > 20)
                songs_list.RemoveRange(20, songs_list.Count - 20);


            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------\
        //full page widget
        //
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult MostPopularSongs()
        {
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();
            
            List<hypster_tv_DAL.Song> songs_list = songsManager.Get_MostPopularSong_Random();

            return View(songs_list);
        }
        //----------------------------------------------------------------------------------------------------------










        //----------------------------------------------------------------------------------------------------------
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult Widget_MostViewedPlaylists()
        {
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> playlist_list = playlistManager.GetMostViewedPlaylists();

            return View(playlist_list);
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        public ActionResult Widget_MostViewedPlayers()
        {

            return View();
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        public ActionResult Widget_MostLikedPlaylists()
        {
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();

            List<hypster_tv_DAL.Playlist> playlist_list = new List<hypster_tv_DAL.Playlist>();

            return View(playlist_list);
        }
        //----------------------------------------------------------------------------------------------------------



    }
}
