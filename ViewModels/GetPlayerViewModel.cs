using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class GetPlayerViewModel
    {
        
        public hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
        public List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();

        public hypster_tv_DAL.Player player = new hypster_tv_DAL.Player();


        public List<hypster_tv_DAL.MusicGenre> music_genres_list = new List<hypster_tv_DAL.MusicGenre>();



        public GetPlayerViewModel()
        {
        }



    }
}