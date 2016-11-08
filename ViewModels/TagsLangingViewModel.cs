using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hypster.ViewModels
{
    public class TagsLangingViewModel
    {
        public List<hypster_tv_DAL.Tag> tags_list = new List<hypster_tv_DAL.Tag>();
        public hypster_tv_DAL.Tag tag = new hypster_tv_DAL.Tag();
        public List<hypster_tv_DAL.Playlist> playlists_list = new List<hypster_tv_DAL.Playlist>();


        public TagsLangingViewModel()
        {
        }
    }
}