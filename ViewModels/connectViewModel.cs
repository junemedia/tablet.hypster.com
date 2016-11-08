using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class connectViewModel
    {
        public List<hypster_tv_DAL.Member> members_list = new List<hypster_tv_DAL.Member>();

        public List<hypster_tv_DAL.MusicGenre> genres_list = new List<hypster_tv_DAL.MusicGenre>();


        public int Current_Page = 1;
        public int Number_Of_Elements = 21;
        public int Number_Of_Pages = 1;



        public int New_User_ID = 0;


        public connectViewModel()
        {
        }


    }
}