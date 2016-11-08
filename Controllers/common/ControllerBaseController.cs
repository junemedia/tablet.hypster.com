using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hypster.Controllers
{
    public class ControllerBase : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //try
            //{
            //    if (Request.Browser.IsMobileDevice == true)
            //    {
            //        Response.Redirect("http://mobile." + System.Configuration.ConfigurationManager.AppSettings["hypsterHostName"]);
            //    }
            //}
            //finally
            //{
            //}
        }



        //log exception
        protected override void OnException(ExceptionContext filterContext)
        {
            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();



            //-----------------------------------------------------------------------------
            //collect exc data
            string action = filterContext.RouteData.Values["action"].ToString();
            string controller = filterContext.RouteData.Values["controller"].ToString();
            string message = "";
            if (filterContext.Exception.Message != null)
            {
                message += filterContext.Exception.Message.ToString();
            }
            if (filterContext.Exception.InnerException != null)
            {
                message += " | " + filterContext.Exception.InnerException.ToString();
            }

            string url_string = filterContext.HttpContext.Request.RawUrl;
            string IP_Address;
            IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IP_Address == null)
                IP_Address = Request.ServerVariables["REMOTE_ADDR"];
            else
                IP_Address = "";
            //-----------------------------------------------------------------------------


            string member_str = "";
            if (User.Identity.IsAuthenticated)
            {
                member_str = User.Identity.Name;
            }


            //-----------------------------------------------------------------------------
            // save exc data
            hypster_tv_DAL.HypException i_exception = new hypster_tv_DAL.HypException();
            i_exception.ExcDate = DateTime.Now;
            i_exception.ExcIP = IP_Address;
            i_exception.ExcUrl = url_string;
            i_exception.ExcMethod = controller + ":" + action + ":" + member_str;
            i_exception.ExcMessage = message;

            if (i_exception.ExcUrl.Length > 145)
            {
                i_exception.ExcUrl = i_exception.ExcUrl.Substring(0, 145);
            }

            if (i_exception.ExcMethod.Length > 145)
            {
                i_exception.ExcMethod = i_exception.ExcMethod.Substring(0, 145);
            }

            if (i_exception.ExcMessage.Length > 345)
            {
                i_exception.ExcMessage = i_exception.ExcMessage.Substring(0, 345);

                
            }
            
            hyDB.HypExceptions.AddObject(i_exception);
            hyDB.SaveChanges();
            //-----------------------------------------------------------------------------




            //-----------------------------------------------------------------------------
            filterContext.ExceptionHandled = true;
        }




    }
}
