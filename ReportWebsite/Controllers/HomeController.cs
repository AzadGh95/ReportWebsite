using ReportWebsite.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportWebsite.SqlConnections;
using static ReportWebsite.Enums.ReportWebSiteType;
using ReportWebsite.Plugins;
using ReportWebsite.Models;
using System.Web.Security;

namespace ReportWebsite.Controllers
{
    public class HomeController : Controller
    {

        public WebSiteDataProvider _websiteDataProvider;
        public UserDataProvider _userDataProvider;

        public HomeController()
        {
            _websiteDataProvider = new WebSiteDataProvider();
            _userDataProvider = new UserDataProvider();
        }
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return View("Login");

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(User user, int? userId)
        {
            var exitUser = _userDataProvider.ExitUser(user.UserName);
            if (!exitUser)
            {
                _userDataProvider.Insert(user);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            return View("Index", new User());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login model)
        {
            bool rememberMe = true;
            string username = model.UserName;
            string password = model.Password;

            var result = _userDataProvider.Login(username, password);
            if (result == true)
            {
                FormsAuthentication.SetAuthCookie(username, rememberMe/*RemmemberMe*/);

                return Redirect(FormsAuthentication.DefaultUrl);
            }
            else
            {
                ModelState.AddModelError("LockError",
                    "" + " < i class='fa fa-warning'></i>" +
                    "نام کاربری یا رمز عبور اشتباه می باشد ."
                    );
            }

            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public JsonResult CountWebSite(WebSiteType type)
        {
            return Json(_websiteDataProvider.Count(type));
            //return Json(WebSiteSqlConnection.Count(type), JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult SliderHomePartial()
        {
            return PartialView();
        }
    }
}