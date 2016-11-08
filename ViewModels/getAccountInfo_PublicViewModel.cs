using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace hypster.ViewModels
{
    public class getAccountInfo_PublicViewModel
    {

        //INFO
        public hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
        public List<hypster_tv_DAL.Photo> userPhotos_list = new List<hypster_tv_DAL.Photo>();



        //MUSIC
        public List<hypster_tv_DAL.Playlist> userPlaylists_list = new List<hypster_tv_DAL.Playlist>();
        
        public List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
        
        public List<hypster_tv_DAL.MusicGenre> musicGenres_list = new List<hypster_tv_DAL.MusicGenre>();
        public List<hypster_tv_DAL.RadioStation> radioStation_list = new List<hypster_tv_DAL.RadioStation>();



        //FRIENDS
        public int NumberOfMyFollowers = 0;
        public int NumberOfMembersIFollow = 0;
        public List<hypster_tv_DAL.Member> MyFollowers_list = new List<hypster_tv_DAL.Member>();
        public List<hypster_tv_DAL.Member> MembersIFollow_list = new List<hypster_tv_DAL.Member>();




        public int New_User_ID = 0;



        public getAccountInfo_PublicViewModel()
        {
        }

    }
}
