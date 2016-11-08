using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class connectController : Controller
    {

        //
        // GET: /connect/

        [OutputCache(Duration = 240)] // chache page itself for short period of time to improve calls performance
        public ActionResult Index()
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();



            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();


            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["Connect_Members"] != null)
            {
                members_list_tmp = (List<hypster_tv_DAL.Member>)i_chache["Connect_Members"];
            }
            else
            {
                members_list_tmp = memManager.GetMembersRandom(800);
                i_chache.Add("Connect_Members", members_list_tmp, DateTime.Now.AddSeconds(1500)); //15 mins
            }
            




            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if(i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }



            // get music genres
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model.genres_list = genreManager.GetMusicGenresList();


            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ((members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;



            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);


            return View(model);
        }




        [OutputCache(Duration = 240)] // chache page itself for short period of time to improve calls performance
        public ActionResult page(int id)
        {
            hypster.ViewModels.connectViewModel model = new ViewModels.connectViewModel();
            model.Current_Page = id;



            // get members for page
            hypster_tv_DAL.memberManagement memManager = new hypster_tv_DAL.memberManagement();
            List<hypster_tv_DAL.Member> members_list_tmp = new List<hypster_tv_DAL.Member>();


            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["Connect_Members"] != null)
            {
                members_list_tmp = (List<hypster_tv_DAL.Member>)i_chache["Connect_Members"];
            }
            else
            {
                members_list_tmp = memManager.GetMembersRandom(800);
                i_chache.Add("Connect_Members", members_list_tmp, DateTime.Now.AddSeconds(1500)); //15 mins
            }





            int start_pos = 1;
            start_pos = (model.Current_Page * model.Number_Of_Elements) - model.Number_Of_Elements;

            int end_pos = 1;
            end_pos = start_pos + model.Number_Of_Elements;

            for (int i = start_pos; i < end_pos; i++)
            {
                if (i < members_list_tmp.Count)
                    model.members_list.Add(members_list_tmp[i]);
            }



            // get music genres
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            model.genres_list = genreManager.GetMusicGenresList();


            // get number of pages
            model.Number_Of_Pages = members_list_tmp.Count / model.Number_Of_Elements;
            if ( (members_list_tmp.Count % model.Number_Of_Elements) > 0)
                model.Number_Of_Pages += 1;


            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);


            return View("Index", model);
        }





    }
}
