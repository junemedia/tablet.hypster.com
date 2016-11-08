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
using System.Collections;



namespace hypster.Controllers
{
    public class hypsterPlayerController : ControllerBase
    {

        //----------------------------------------------------------------------------------------------------------
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        public ActionResult Index()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------




            
            // 2.get requested media type (playlist, single song, default palylist)
            //--------------------------------------------------------------------------------------------
            string MEDIA_TYPE = "";
            if (Request.QueryString["media_type"] != null)
            {
                MEDIA_TYPE = Request.QueryString["media_type"];
                ViewBag.MEDIA_TYPE = MEDIA_TYPE;
            }
            else
            {
                ViewBag.MEDIA_TYPE = "";
            }
            //--------------------------------------------------------------------------------------------




            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            if (Request.QueryString["us_id"] != null)
            {
                ViewBag.UserID = Request.QueryString["us_id"];
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    member = memberManager.getMemberByUserName(User.Identity.Name);
                    ViewBag.UserID = member.id;
                }
            }





            // 3.parse media type
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            switch (MEDIA_TYPE)
            {

                    //
                    //display requested playlist
                case "playlist":
                    int PLAYLIST_ID = 0;
                    if (Request.QueryString["playlist_id"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out PLAYLIST_ID) == false)
                            PLAYLIST_ID = 0;
                    }

                    playlistManager.AddPlaylistView(PLAYLIST_ID);
                    ViewBag.PlaylistID = PLAYLIST_ID;
                    break;

                    //
                    //display single song
                case "":

                    ViewBag.MEDIA_TYPE = "song";


                    string SongGuid = "";
                    if (Request.QueryString["song_guid"] != null)
                    {
                        SongGuid = Request.QueryString["song_guid"];
                        ViewBag.SongGuid = SongGuid.Replace("&", "amp;");
                    }


                    string SongTitle = "";
                    if (Request.QueryString["song_title"] != null)
                    {
                        SongTitle = Request.QueryString["song_title"];
                        ViewBag.SongTitle = SongTitle.Replace("&", "amp;");
                    }



                    hypster_tv_DAL.PlaylistData_Song song = new hypster_tv_DAL.PlaylistData_Song();
                    song.YoutubeId = SongGuid;
                    song.Title = SongTitle;

                    model.songs_list.Add(song);


                    


                    if (model.songs_list.Count > 0)
                        ViewBag.SongGuid = model.songs_list[0].YoutubeId;
                    break;



                    //
                    //display default palylist
                case "DEFPL":
                    ViewBag.MEDIA_TYPE = "playlist";
                    
                    
                    playlistManager.AddPlaylistView(member.active_playlist);
                    ViewBag.PlaylistID = member.active_playlist;
                    ViewBag.UserID = member.id;
                    break;



                //
                //display default palylist
                case "Radio":
                    ViewBag.MEDIA_TYPE = "radio";


                    string Genre_str = "";
                    if (Request.QueryString["Genre"] != null)
                    {
                        Genre_str = Request.QueryString["Genre"];
                        Genre_str = Genre_str.Replace("&", "amp;");
                        ViewBag.Genre = Genre_str;
                    }

                    string search_str = "";
                    if (Request.QueryString["search"] != null)
                    {
                        search_str = Request.QueryString["search"];
                        ViewBag.search = search_str;
                    }


                    break;



                default:
                    break;

            }
            //--------------------------------------------------------------------------------------------



            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        // music player popup
        // this logic open player with playlists, songs, and other player related stuff
        public ActionResult MPL()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //--------------------------------------------------------------------------------------------





            // 2.get requested media type (playlist, single song, default palylist)
            //--------------------------------------------------------------------------------------------
            string MEDIA_TYPE = "";
            if (Request.QueryString["media_type"] != null)
            {
                MEDIA_TYPE = Request.QueryString["media_type"];
                ViewBag.MEDIA_TYPE = MEDIA_TYPE;
            }
            else
            {
                ViewBag.MEDIA_TYPE = "";
            }
            //--------------------------------------------------------------------------------------------






            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
            if (Request.QueryString["us_id"] != null)
            {
                ViewBag.UserID = Request.QueryString["us_id"];
                int us_id = 0;
                Int32.TryParse(Request.QueryString["us_id"], out us_id);
                if(us_id > 0)
                    member = memberManager.getMemberByID(us_id);
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {
                    member = memberManager.getMemberByUserName(User.Identity.Name);
                    ViewBag.UserID = member.id;
                }
            }
            //--------------------------------------------------------------------------------------------






            // 3.parse media type
            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.videoPlayerViewModel model = new ViewModels.videoPlayerViewModel();

            switch (MEDIA_TYPE)
            {

                //
                //display requested playlist
                case "playlist":
                    int PLAYLIST_ID = 0;
                    if (Request.QueryString["playlist_id"] != null)
                    {
                        if (Int32.TryParse(Request.QueryString["playlist_id"], out PLAYLIST_ID) == false)
                            PLAYLIST_ID = 0;
                    }

                    playlistManager.AddPlaylistView(PLAYLIST_ID);

                    if (ViewBag.UserID != null && PLAYLIST_ID != null)
                    {
                        model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID);
                    }

                    ViewBag.PlaylistID = PLAYLIST_ID;
                    break;

                //
                //display single song
                case "":

                    ViewBag.MEDIA_TYPE = "song";


                    string SongGuid = "";
                    if (Request.QueryString["song_guid"] != null)
                    {
                        SongGuid = Request.QueryString["song_guid"];
                        ViewBag.SongGuid = SongGuid.Replace("&", "amp;");
                    }


                    string SongTitle = "";
                    if (Request.QueryString["song_title"] != null)
                    {
                        SongTitle = Request.QueryString["song_title"];
                        ViewBag.SongTitle = SongTitle.Replace("&", "amp;");
                    }



                    hypster_tv_DAL.PlaylistData_Song song = new hypster_tv_DAL.PlaylistData_Song();
                    song.YoutubeId = SongGuid;
                    song.Title = SongTitle;

                    model.songs_list.Add(song);



                    if (model.songs_list.Count > 0)
                        ViewBag.SongGuid = model.songs_list[0].YoutubeId;
                    break;



                //
                //display default palylist
                case "DEFPL":
                    ViewBag.MEDIA_TYPE = "playlist";
                    int PLAYLIST_ID_DEF = member.active_playlist;

                    playlistManager.AddPlaylistView(member.active_playlist);

                    if (ViewBag.UserID != null && PLAYLIST_ID_DEF != null)
                    {
                        model.songs_list = playlistManager.GetSongsForPlayList(Int32.Parse(ViewBag.UserID.ToString()), (int)PLAYLIST_ID_DEF);
                    }

                    ViewBag.PlaylistID = member.active_playlist;
                    ViewBag.UserID = member.id;
                    break;



                //
                //display default palylist
                case "Radio":
                    ViewBag.MEDIA_TYPE = "radio";


                    string Genre_str = "";
                    if (Request.QueryString["Genre"] != null)
                    {
                        Genre_str = Request.QueryString["Genre"];
                        Genre_str = Genre_str.Replace("&", "amp;");
                        ViewBag.Genre = Genre_str;
                    }


                    #region GET_RADIO_GENRE
                    int G_User = 0;
                    int G_Plst = 0;
                    switch(Genre_str)
                    {
                        case "Dance":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Dance"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Dance"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Jazz":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Jazz"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Jazz"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Bluegrass":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Bluegrass"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Bluegrass"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Classical":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Classical"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Classical"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Reggae":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Reggae"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Reggae"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Rap":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Rap"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Rap"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Rock":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Rock"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Rock"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Soundtrack":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Soundtrack"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Soundtrack"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Blues":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Blues"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Blues"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Pop":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Pop"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Pop"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Country":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Country"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Country"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;



                        case "Opera":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Opera"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Opera"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Hip-Hop":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_HipHop"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_HipHop"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Latin":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Latin"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Latin"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Electronic":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Electronic"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Electronic"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "R&B":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_RandB"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_RandB"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "NewAge":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_NewAge"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_NewAge"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Folk":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Folk"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Folk"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "J-Pop":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_JPop"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_JPop"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Soul":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Soul"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Soul"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Instrumental":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Instrumental"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Instrumental"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Adult Contemporary":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_AdultContemp"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_AdultContemp"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;
                        case "Alternative":
                            G_User = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_User_Alternative"]);
                            G_Plst = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Radio_Plst_Alternative"]);
                            playlistManager.AddPlaylistView(member.active_playlist);
                            model.songs_list = playlistManager.GetSongsForPlayList(G_User, G_Plst);
                            break;



                        default: //if custom search started
                            
                            string search_str = "";
                            if (Request.QueryString["search"] != null)
                            {
                                search_str = Request.QueryString["search"];
                                ViewBag.search = search_str;
                            }
                            YouTubeRequestSettings settings = new YouTubeRequestSettings("hypster", "AI39si5TNjKgF6yiHwUhKbKwIui2JRphXG4hPXUBdlrNh4XMZLXu--lf66gVSPvks9PlWonEk2Qv9fwiadpNbiuh-9TifCNsqA");
                            YouTubeRequest request = new YouTubeRequest(settings);

                            int this_page = 1;
                            for (this_page = 1; this_page < 10; this_page++)
                            {
                                string feedUrl = "http://gdata.youtube.com/feeds/api/videos?q=" + Genre_str + "&category=Music&format=5&restriction=" + Request.ServerVariables["REMOTE_ADDR"] + "&safeSearch=none&start-index=" + ((this_page*25)-24) + "&orderby=viewCount";
                                Feed<Video> videoFeed = request.Get<Video>(new Uri(feedUrl));
                                foreach (Video item in videoFeed.Entries)
                                {
                                    hypster_tv_DAL.PlaylistData_Song song_add1 = new hypster_tv_DAL.PlaylistData_Song();
                                    song_add1.YoutubeId = item.VideoId;
                                    song_add1.Title = item.Title;
                                    model.songs_list.Add(song_add1);
                                }
                            }
                            break;
                    }
                    #endregion

                    ViewBag.UserID = G_User;
                    ViewBag.PlaylistID = G_Plst;
                    


                    break;



                default:
                    break;

            }
            //--------------------------------------------------------------------------------------------






            //--------------------------------------------------------------------------------------------
            if (member.id != 0)
            {
                model.userPlaylists_list = playlistManager.GetUserPlaylists(member.id);
            }
            //--------------------------------------------------------------------------------------------




            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------








    }
}
