using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recaptcha;


namespace hypster.Controllers
{
    public class hypsterController : ControllerBase
    {


        //
        // GET: /hypster/
        //----------------------------------------------------------------------------------------------------------
        public ActionResult Index()
        {
            return RedirectPermanent("/home/index");
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        public ActionResult Contact_Us()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------

        [RecaptchaControlMvc.CaptchaValidator]
        [HttpPost]
        public ActionResult Contact_Us(string YourEmail, string Subject, string Message, bool captchaValid, string captchaErrorMessage)
        {
            if (captchaValid)
            {
                hypster_tv_DAL.Hypster_Entities HypDB = new hypster_tv_DAL.Hypster_Entities();


                hypster_tv_DAL.userContact userContact = new hypster_tv_DAL.userContact();
                userContact.contactType = 1;
                userContact.contactEmail = YourEmail;
                userContact.contactSubject = Subject;
                userContact.contactText = Message;
                HypDB.userContacts.AddObject(userContact);
                HypDB.SaveChanges();


                hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
                emailManager.SendContactUsEmail("noah@baronsmedia.com", "orville@baronsmedia.com", "jim@baronsmedia.com", Subject, YourEmail, Message);


                return View("Contacts_Thanks");
            }

            return View("Contact_Us");
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        public ActionResult Contacts_Thanks()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------









        //----------------------------------------------------------------------------------------------------------
        public ActionResult About_Us()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Privacy_Policy()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult Terms_of_Service()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        public ActionResult How_To_Use()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        public ActionResult Hypster_On_Mobile()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        public ActionResult New_Player_For_Tumblr()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            ViewBag.User_ID = memberManager.getMemberByUserName(User.Identity.Name).id;
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code()
        {
            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code_1()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult New_Player_For_Tumblr_Code_2()
        {

            // 1.genral declarations
            //--------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            //--------------------------------------------------------------------------------------------




            //--------------------------------------------------------------------------------------------
            hypster.ViewModels.GetPlayerViewModel model = new ViewModels.GetPlayerViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.playlists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            //--------------------------------------------------------------------------------------------


            return View(model);
        }
        //----------------------------------------------------------------------------------------------------------




    }
}
