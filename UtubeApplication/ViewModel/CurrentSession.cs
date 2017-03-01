using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UtubeApplication.Models;
using UtubeApplication.Controllers;


namespace UtubeApplication
{
    public class CurrentSession
    {
        public static User CurrentUser
        {
            get
            {
                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    Controllers.AccountController.ResetCurrentUserSession();
                }

                return (User)HttpContext.Current.Session["CurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["CurrentUser"] = value;
            }
        }
    }
}