using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class createPlaylist_ViewModel
    {
        public hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();


        public List<hypster_tv_DAL.Playlist> userPlaylists_list = new List<hypster_tv_DAL.Playlist>();

        public List<hypster_tv_DAL.PlaylistData_Song> userActivePlaylist_Songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();




        public createPlaylist_ViewModel()
        {
        }



    }
}