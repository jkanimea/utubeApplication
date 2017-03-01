using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtubeApplication.Models;

using System.Web.Security;
using UtubeApplication.ViewModel;

namespace UtubeApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginwithModel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginwithModel(LoginViewModel model)
        {
            using (var db = new MyTubeEntities())
            {
                var existUser = db.Users.Where(x => x.Username == model.UserName && x.Password == model.Password).FirstOrDefault();
                if (existUser == null)
                {
                    //Return error
                    ViewBag.IncorrectLogin = "Sorry we don't understand your combination";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    //Authorise cookie
                    //Generate new session 
                    ResetCurrentUserSession(model.UserName);
                    TempData["Success"] = "Login Successfully .. ";
                    return RedirectToAction("Index", "Home");
                }
            }


           
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, bool? rememberme)
        {
            using (var db = new MyTubeEntities())
            {
                var existUser = db.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                if(existUser == null)
                {
                    //Return error
                    ViewBag.IncorrectLogin = "Sorry we don't understand your combination";
                    return View();
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(username, rememberme ?? false);
                    //Authorise cookie
                    //Generate new session 
                    ResetCurrentUserSession(username);
                    TempData["Success"] = "Login Successfully .. ";
                   return RedirectToAction("Index", "Home");
                }
              }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            using (var db = new MyTubeEntities())
            {
                var newUser = new User
                {
                    Username = model.Username,
                    EmailAddress = model.EmailAddress,
                    Gender = model.Gender,
                    Password = model.Password,
                    CreatedOn = DateTime.Now,
                //    CreatedBy = CurrentSession.CurrentUser.Id,
                    IsAdmin = false,
                    IsActive = true,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(model.Username, true);
                //Authorise cookie
                //Generate new session 
                ResetCurrentUserSession(model.Username);
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            CurrentSession.CurrentUser = null;
            return RedirectToAction("Index","Home");
        }


        public static void ResetCurrentUserSession(string userName = null)
        {
            if (string.IsNullOrEmpty(userName))
            {
                userName = System.Web.HttpContext.Current.User.Identity.Name;
            }

            using (var db = new MyTubeEntities())
            {
                var user = db.Users.FirstOrDefault(n => n.Username == userName);
                // modified for forgot password
                if (user == null)
                {
                    return;
                }
                // Generic of CurrentUser that can user across system
                CurrentSession.CurrentUser = user;
            }
        }
    }
}