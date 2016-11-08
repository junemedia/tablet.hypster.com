using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class videoPlayerViewModel
    {
        public List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
        public List<hypster_tv_DAL.Playlist> userPlaylists_list = new List<hypster_tv_DAL.Playlist>();

        public videoPlayerViewModel()
        {
        }



    }
}