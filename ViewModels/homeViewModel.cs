using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{

    public class homeViewModel
    {
        //public List<hypster_tv_DAL.videoClip> TopVideos = new List<hypster_tv_DAL.videoClip>();

        public List<hypster_tv_DAL.Song> most_popular_songs = new List<hypster_tv_DAL.Song>();

        public List<hypster_tv_DAL.VisualSearch> visualSearch_list = new List<hypster_tv_DAL.VisualSearch>();



        public homeViewModel()
        {
        }




    }

}