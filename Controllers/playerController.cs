using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{

    //
    //this controller manages players wizards
    public class playerController : ControllerBase
    {

        


        //----------------------------------------------------------------------------------------------------------
        //[HttpGet]
        [Authorize]
        public ActionResult getPlayer(string id)
        {
            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------





            //*******Check ACTION***********************************
            if (Request.QueryString["ACT"] != null)
            {
                switch (Request.QueryString["ACT"])
                {
                    case "DeletePlayer":
                        
                        int i_player_id = 0;
                        if (Request.QueryString["Pl_ID"] != null && Int32.TryParse(Request.QueryString["Pl_ID"], out i_player_id) == false)
                            i_player_id = 0;


                        if (i_player_id != 0)
                        {
                            playerManager.DeletePlayer(model.curr_user.id, i_player_id);
                            return RedirectPermanent("/account/music");
                        }

                        break;
                    default:
                        break;
                }
            }
            //******************************************************





            switch (id)
            {
                case "BarPlayer":
                    //get player for editing if exist
                    int player_ID1 = 0;
                    if (Request.QueryString["Pl_ID"] != null)
                    {
                        if(Int32.TryParse(Request.QueryString["Pl_ID"], out player_ID1) == false)
                            player_ID1 = 0;
                        if(player_ID1 != 0)
                            model.player = playerManager.GetPlayerByID(model.curr_user.id, player_ID1);
                    }
                    
                    
                    return View("getPlayer_Bar", model);
                    break;

                case "ClassicPlayer":
                    //get player for editing if exist
                    int player_ID2 = 0;
                    if (Request.QueryString["Pl_ID"] != null)
                    {
                        if(Int32.TryParse(Request.QueryString["Pl_ID"], out player_ID2) == false)
                            player_ID2 = 0;
                        if(player_ID2 != 0)
                            model.player = playerManager.GetPlayerByID(model.curr_user.id, player_ID2);
                    }

                    return View("getPlayer_Classic", model);
                    break;


                case "RadioPlayer":
                    //get player for editing if exist

                    hypster.ViewModels.GetPlayerViewModel model_Radio = new ViewModels.GetPlayerViewModel();

                    int player_ID3 = 0;
                    if (Request.QueryString["Pl_ID"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["Pl_ID"], out player_ID3) == false)
                            player_ID3 = 0;
                        if (player_ID3 != 0)
                            model_Radio.player = playerManager.GetPlayerByID(model.curr_user.id, player_ID3);
                    }

                    hypster_tv_DAL.MemberMusicGenreManager genreMeneger = new hypster_tv_DAL.MemberMusicGenreManager();
                    model_Radio.music_genres_list = genreMeneger.GetMusicGenresList();

                    return View("getPlayer_Radio", model_Radio);
                    break;

                default:
                    break;

            }


            return View();
        }
        //----------------------------------------------------------------------------------------------------------








        // FORM FOR BAR PLAYER
        //----------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        public ActionResult getPlayer_barPlayer(int PlayerID, string PlayerName, int? Sel_Playlist_ID, bool BAR_autostart, bool BAR_shufflePlayback, string Placement_of_the_Player, bool BAR_showPlaylistByDefault, string Player_Color_Scheme)
        {
            //--------------------------------------------------------------------------------------------
            if (Sel_Playlist_ID == null)
                return RedirectPermanent("/create");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.Player player = new hypster_tv_DAL.Player();
            player.user_ID = curr_user.id;
            player.Player_Type = "BarPlayer";


            player.player_Name = PlayerName;
            player.playlist_ID = Sel_Playlist_ID;
            player.BAR_playerSkin = Player_Color_Scheme;
            player.BAR_autostart = BAR_autostart;
            player.BAR_shufflePlayback = BAR_shufflePlayback;
            player.BAR_placementOfThePlayer = Placement_of_the_Player;
            player.BAR_showPlaylistByDefault = BAR_showPlaylistByDefault;



            //save only if player_id null - new player
            if (PlayerID == 0)
            {
                playerManager.hyDB.Players.AddObject(player);
                playerManager.hyDB.SaveChanges();
            }
            else //edit player
            {   
                if (PlayerID != 0)
                {   
                    hypster_tv_DAL.Player player_edit = new hypster_tv_DAL.Player();
                    player_edit = playerManager.GetPlayerByID(curr_user.id, PlayerID);

                    player_edit.player_Name = player.player_Name;
                    player_edit.playlist_ID = player.playlist_ID;
                    player_edit.BAR_playerSkin = player.BAR_playerSkin;
                    player_edit.BAR_autostart = player.BAR_autostart;
                    player_edit.BAR_shufflePlayback = player.BAR_shufflePlayback;
                    player_edit.BAR_placementOfThePlayer = player.BAR_placementOfThePlayer;
                    player_edit.BAR_showPlaylistByDefault = player.BAR_showPlaylistByDefault;

                    playerManager.EditPlayer(player_edit);

                    //returns edited player
                    return View("getPlayer_Bar_Code", player_edit);
                }
            }
            //--------------------------------------------------------------------------------------------




            //if here - returns new player
            return View("getPlayer_Bar_Code", player);
        }
        //----------------------------------------------------------------------------------------------------------


        /*

        //public getPlayer_classicPlayer
        //----------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        public ActionResult getPlayer_classicPlayer(int PlayerID, string PlayerName, int? Sel_Playlist_ID, bool CLASSIC_autostart, bool CLASSIC_shufflePlayback, string Player_Color_Scheme)
        {
            //need to log this case
            //--------------------------------------------------------------------------------------------
            if (Sel_Playlist_ID == null)
                return RedirectPermanent("/create");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------





            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            
            hypster_tv_DAL.Player player = new hypster_tv_DAL.Player();
            player.user_ID = curr_user.id;
            player.Player_Type = "ClassicPlayer";

            player.player_Name = PlayerName;
            player.playlist_ID = Sel_Playlist_ID;
            player.CLASSIC_playerSkin = Player_Color_Scheme;
            player.CLASSIC_autostart = (bool)CLASSIC_autostart;
            player.CLASSIC_shufflePlayback = (bool)CLASSIC_shufflePlayback;
            


            //save only if player_id null - new player
            if (PlayerID == 0)
            {
                playerManager.hyDB.Players.AddObject(player);
                playerManager.hyDB.SaveChanges();
            }
            else //edit player
            {
                if (PlayerID != 0)
                {   
                    hypster_tv_DAL.Player player_edit = new hypster_tv_DAL.Player();
                    player_edit = playerManager.GetPlayerByID(curr_user.id, PlayerID);

                    player_edit.player_Name = player.player_Name;
                    player_edit.playlist_ID = player.playlist_ID;
                    player_edit.CLASSIC_playerSkin = player.CLASSIC_playerSkin;
                    player_edit.CLASSIC_autostart = player.CLASSIC_autostart;
                    player_edit.CLASSIC_shufflePlayback = player.CLASSIC_shufflePlayback;

                    playerManager.EditPlayer(player_edit);

                    //returns edited player
                    return View("getPlayer_Classic_Code", player_edit);
                }
            }
            //--------------------------------------------------------------------------------------------




            //if here - returns new player
            return View("getPlayer_Classic_Code", player);
        }
        //----------------------------------------------------------------------------------------------------------

        */



        //public getPlayer_classicPlayer
        //----------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        public ActionResult getPlayer_classicPlayer(int PlayerID, string PlayerName, int? Sel_Playlist_ID, bool CLASSIC_autostart, bool CLASSIC_shufflePlayback, string Player_Color_Scheme, string Custom_Size_Sel, string hf_BackgroundColor, string hf_SongBackground, string hf_SongClicked, string hf_Buttons)
        {
            //need to log this case
            //--------------------------------------------------------------------------------------------
            if (Sel_Playlist_ID == null)
                return RedirectPermanent("/create");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);



            if (Player_Color_Scheme == "6")
            {

                hypster_tv_DAL.Player player_custom = new hypster_tv_DAL.Player();
                player_custom.user_ID = curr_user.id;
                player_custom.Player_Type = "ClassicPlayer";

                player_custom.player_Name = PlayerName;
                player_custom.playlist_ID = Sel_Playlist_ID;
                player_custom.CLASSIC_playerSkin = Player_Color_Scheme;
                player_custom.CLASSIC_autostart = (bool)CLASSIC_autostart;
                player_custom.CLASSIC_shufflePlayback = (bool)CLASSIC_shufflePlayback;

                if (Custom_Size_Sel == "2")
                {
                    ViewBag.Custom_Size_Sel_W = 56;
                    ViewBag.Custom_Size_Sel_H = 13;
                }
                else
                {   
                    ViewBag.Custom_Size_Sel_W = 370;
                    ViewBag.Custom_Size_Sel_H = 450;
                }

                ViewBag.hf_BackgroundColor = hf_BackgroundColor; 
                ViewBag.hf_SongBackground = hf_SongBackground;
                ViewBag.hf_SongClicked = hf_SongClicked;
                ViewBag.hf_Buttons = hf_Buttons;

                //if here - returns new player
                return View("getPlayer_Classic_Code_Custom", player_custom);
            }
            




            //--------------------------------------------------------------------------------------------
            
            hypster_tv_DAL.Player player = new hypster_tv_DAL.Player();
            player.user_ID = curr_user.id;
            player.Player_Type = "ClassicPlayer";

            player.player_Name = PlayerName;
            player.playlist_ID = Sel_Playlist_ID;
            player.CLASSIC_playerSkin = Player_Color_Scheme;
            player.CLASSIC_autostart = (bool)CLASSIC_autostart;
            player.CLASSIC_shufflePlayback = (bool)CLASSIC_shufflePlayback;



            //save only if player_id null - new player
            if (PlayerID == 0)
            {
                playerManager.hyDB.Players.AddObject(player);
                playerManager.hyDB.SaveChanges();
            }
            else //edit player
            {
                if (PlayerID != 0)
                {
                    hypster_tv_DAL.Player player_edit = new hypster_tv_DAL.Player();
                    player_edit = playerManager.GetPlayerByID(curr_user.id, PlayerID);

                    player_edit.player_Name = player.player_Name;
                    player_edit.playlist_ID = player.playlist_ID;
                    player_edit.CLASSIC_playerSkin = player.CLASSIC_playerSkin;
                    player_edit.CLASSIC_autostart = player.CLASSIC_autostart;
                    player_edit.CLASSIC_shufflePlayback = player.CLASSIC_shufflePlayback;

                    playerManager.EditPlayer(player_edit);

                    //returns edited player
                    return View("getPlayer_Classic_Code", player_edit);
                }
            }
            //--------------------------------------------------------------------------------------------


            //if here - returns new player
            return View("getPlayer_Classic_Code", player);
        }
        //----------------------------------------------------------------------------------------------------------







        //public getPlayer_classicPlayer
        //----------------------------------------------------------------------------------------------------------
        [HttpPost]
        [Authorize]
        public ActionResult getPlayer_radioPlayer(int PlayerID, string PlayerName, int? Sel_Genre_ID, bool RADIO_autostart)
        {
            //--------------------------------------------------------------------------------------------
            //need to log this case 
            if (Sel_Genre_ID == null)
                return RedirectPermanent("/create");
            //--------------------------------------------------------------------------------------------



            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.Player player = new hypster_tv_DAL.Player();
            player.user_ID = curr_user.id;
            player.Player_Type = "RadioPlayer";

            player.player_Name = PlayerName;
            player.RADIO_Genre_ID = (int)Sel_Genre_ID;
            player.RADIO_Genre = genreManager.GetMusicGenre_byID((int)player.RADIO_Genre_ID).GenreName;
            player.RADIO_autostart = RADIO_autostart;




            if (PlayerID == 0)
            {
                playerManager.hyDB.Players.AddObject(player);
                playerManager.hyDB.SaveChanges();
            }
            else //edit player
            {
                if (PlayerID != 0)
                {
                    hypster_tv_DAL.Player player_edit = new hypster_tv_DAL.Player();
                    player_edit = playerManager.GetPlayerByID(curr_user.id, PlayerID);

                    player_edit.player_Name = PlayerName;
                    player_edit.RADIO_Genre_ID = (int)Sel_Genre_ID;
                    player_edit.RADIO_Genre = genreManager.GetMusicGenre_byID((int)player.RADIO_Genre_ID).GenreName;
                    player_edit.RADIO_autostart = RADIO_autostart;

                    playerManager.EditPlayer(player_edit);

                    //returns edited player
                    return View("getPlayer_Radio_Code", player_edit);
                }
            }
            //--------------------------------------------------------------------------------------------




            //if here - returns new player
            return View("getPlayer_Radio_Code", player);
        }
        //----------------------------------------------------------------------------------------------------------

        

    }
}
