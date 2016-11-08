using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class createController : Controller
    {


        // GET: /create/
        //--------------------------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            hypster.ViewModels.createViewModel model = new ViewModels.createViewModel();



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        [Authorize]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult player(string id)
        {
            hypster.ViewModels.createPlayer_ViewModel model = new ViewModels.createPlayer_ViewModel();



            switch (id)
            {
                case "BarPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                case "ClassicPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                case "RadioPlayer":
                    ViewBag.Action = "EDT";
                    ViewBag.PlType = id;
                    break;
                default:
                    break;
            }


            return View(model);
        }


        //--------------------------------------------------------------------------------------------------------






        //--------------------------------------------------------------------------------------------------------
        [Authorize]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult playlist()
        {
            hypster.ViewModels.createPlaylist_ViewModel model = new ViewModels.createPlaylist_ViewModel();

            
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.songsManagement songsManager = new hypster_tv_DAL.songsManagement();


            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            //-----------------------------------------------------------------------------------------------------




            // 2.proccess user actions if any
            //-----------------------------------------------------------------------------------------------------
            // process user actions
            if (Request.QueryString["ACT"] != null)
            {

                switch (Request.QueryString["ACT"].ToString())
                {
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_playlist":
                        int d_playlist_id = 0;
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out d_playlist_id) == false)
                            d_playlist_id = 0;

                        if (d_playlist_id != 0)
                        {
                            playlistManager.Delete_Playlist(model.curr_user.id, d_playlist_id);
                            return RedirectPermanent("/create/playlist");
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_song":
                        int d_song_id = 0;
                        if (Int32.TryParse(Request.QueryString["song_id"], out d_song_id) == false)
                            d_song_id = 0;


                        string pl_id = "";
                        if(Request.QueryString["playlist_id"] != null)
                            pl_id = Request.QueryString["playlist_id"].ToString();


                        if (d_song_id != 0)
                        {
                            playlistManager.DeleteSong(model.curr_user.id, d_song_id);
                            return RedirectPermanent("/create/playlist?playlist_id=" + pl_id);
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                    case "delete_song_plr":
                        int d_song_id1 = 0;
                        if (Int32.TryParse(Request.QueryString["song_id"], out d_song_id1) == false)
                            d_song_id1 = 0;

                        if (d_song_id1 != 0)
                        {
                            playlistManager.DeleteSong(model.curr_user.id, d_song_id1);

                            if (Request.QueryString["ret_url"] == null)
                            {
                                return RedirectPermanent("/create/playlist");
                            }
                            else
                            {
                                return RedirectPermanent("/create/playlist");
                            }
                        }
                        break;
                    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


                    default:
                        break;
                }

            }
            //-----------------------------------------------------------------------------------------------------






            // 3.prepare output model
            //-----------------------------------------------------------------------------------------------------
            //hypster.ViewModels.AccountPlaylistsViewModel model = new ViewModels.AccountPlaylistsViewModel();
            //model.curr_user = member_user;
            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------




            // 4. get current requested playlist id
            // if no playlist selected display default
            //-----------------------------------------------------------------------------------------------------
            int playlist_id = 0;
            if (Request.QueryString["playlist_id"] != null)
            {
                if (Int32.TryParse(Request.QueryString["playlist_id"], out playlist_id) == false)
                    playlist_id = 0;
            }
            else
            {
                playlist_id = model.curr_user.active_playlist;
            }
            //-----------------------------------------------------------------------------------------------------




            // 5.get selected playlist details
            //-----------------------------------------------------------------------------------------------------
            foreach (var item in model.userPlaylists_list)
            {
                if (item.id == playlist_id)
                {
                    ViewBag.ActivePlaylistName = item.name;
                    ViewBag.ActivePlaylistID = item.id;
                }
            }
            //-----------------------------------------------------------------------------------------------------



            // 6.get songs for selected playlist
            //-----------------------------------------------------------------------------------------------------
            if (playlist_id != 0)
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, playlist_id);
            }
            else
            {
                model.userActivePlaylist_Songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.curr_user.active_playlist);
            }
            //-----------------------------------------------------------------------------------------------------



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------





        //--------------------------------------------------------------------------------------------------------
        [Authorize]
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult station()
        {
            hypster.ViewModels.createStation_ViewModel model = new ViewModels.createStation_ViewModel();

            string action = "";
            if (Request.QueryString["act"] != null)
                action = Request.QueryString["act"];

            switch (action)
            {
                case "err":
                    ViewBag.ErrorMessage = "Please enter Station Name and Artist or Genre";
                    break;
            }


            hypster_tv_DAL.visualSearchManager visualSearchManager = new hypster_tv_DAL.visualSearchManager();
            model.visualSearchList = visualSearchManager.getVisualSearchArtists_cached();



            return View(model);
        }
        //--------------------------------------------------------------------------------------------------------



       


    }
}
