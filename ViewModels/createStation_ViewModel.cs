using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class createStation_ViewModel
    {

        public List<hypster_tv_DAL.Playlist> most_viewed_playlists = new List<hypster_tv_DAL.Playlist>();

        public List<hypster_tv_DAL.VisualSearch> visualSearchList = new List<hypster_tv_DAL.VisualSearch>();




        public createStation_ViewModel()
        {
        }



    }
}