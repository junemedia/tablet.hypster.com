using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class SongLandingPageViewModel
    {
        public hypster_tv_DAL.Song song = new hypster_tv_DAL.Song();
        public List<hypster_tv_DAL.Member> members_with_song_list = new List<hypster_tv_DAL.Member>();

        public List<hypster_tv_DAL.songComment> songComments_list = new List<hypster_tv_DAL.songComment>();



        public SongLandingPageViewModel()
        {
        }

    }
}