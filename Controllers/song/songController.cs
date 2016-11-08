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
    public class songController : ControllerBase
    {
        //
        //no additional methods here since it will be traslated into song url /song/GUID
        //



        //----------------------------------------------------------------------------------------------------------
        // for song landing page
        //[OutputCache(Duration = 1200, VaryByParam = "none")]
        public ActionResult getSongByID(string song_guid)
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.songsManagement song_manager = new hypster_tv_DAL.songsManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------

            int song_int_id = 0;
            if (Int32.TryParse(song_guid, out song_int_id) == false)
            {
                return RedirectPermanent("/");
            }


            // 2.get model
            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.SongLandingPageViewModel songModel = new ViewModels.SongLandingPageViewModel();
            songModel.song = song_manager.GetSongByID(song_guid);

            songModel.songComments_list = song_manager.Get_SongComments(songModel.song.id);
            //-----------------------------------------------------------------------------------------------------


            return View(songModel);
        }
        //----------------------------------------------------------------------------------------------------------

        


    }
}
