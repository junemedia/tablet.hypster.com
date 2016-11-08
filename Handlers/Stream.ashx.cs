using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;
//using Hypster.Models;

namespace Hypster.Handlers
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class Stream : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //--------------------------------------------------------------------------------------------------------------------
            int id = 0;
            //--------------------------------------------------------------------------------------------------------------------


            //--------------------------------------------------------------------------------------------------------------------
            if (int.TryParse(context.Request.QueryString["id"], out id) == false)
                return;
            //--------------------------------------------------------------------------------------------------------------------


            //--------------------------------------------------------------------------------------------------------------------
            context.Response.Clear();
            context.Response.Write("N/A");
            //--------------------------------------------------------------------------------------------------------------------
        }






        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
