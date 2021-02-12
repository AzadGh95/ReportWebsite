using ReportWebsite.Models;
using ReportWebsite.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ReportWebsite.Controllers
{
    public class UsersController : Controller
    {
        public UserDataProvider _userDataProvider;
        // GET: Users

        public UsersController()
        {
            _userDataProvider = new UserDataProvider();
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Users()
        {
            return View(_userDataProvider.GetUsers());
        }

        public ActionResult Logout() // متد خروج کاربر از حساب کاربریش
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }





        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        //public ActionResult Form(WebSite webSite, int? initSiteId)
        public ActionResult User(User user, int? inituserId)
        {
            ViewBag.Title = "کاربر";
            if (inituserId == 0)
            {
                // .....insert website
                //check username is dublicate 

                if (_userDataProvider.GetUser(user.UserName) != null)
                {
                    ModelState.AddModelError("LockError",
                  "" + " < i class='fa fa-warning'></i>" +
                  "نام کاربری تکراری می باشد !"
                  );
                    return View(user);
                }
                var result = _userDataProvider.Insert(user);
                if (!result)
                {
                    ModelState.AddModelError("LockError",
           "" + " < i class='fa fa-warning'></i>" +
           "خطا در ذخیره سازی !"
           );
                    return View(user);
                }
                 

                ModelState.AddModelError("Success",
                 "" + " < i class='fa fa-check'></i>" +
                 "کاربر جدید با موفقیت ثبت شد."
                 );
                return RedirectToAction("Users");
            }
            else
            {
                // .....update website
                user.Id = inituserId ?? 0;
                var result = _userDataProvider.Update(user, inituserId ?? 0);
                if (result)
                {
                    ModelState.AddModelError("LockError",
                 "" + " < i class='fa fa-like'></i>" +
                 "کاربر ثبت با موفقیت انجام شد."
                 );
                    return RedirectToAction("Users");
                }
                ModelState.AddModelError("LockError",
                  "" + " < i class='fa fa-warning'></i>" +
                  "خطا در ذخیره سازی !"
                  );
                return RedirectToAction("Users");
                //todo : نمایش پیغام خطا
            }

        }


        [Authorize(Roles = "SuperAdmin")]
        //public ActionResult Form(int? siteId)
        public ActionResult User(int? userId)
        {
            ViewBag.Title = "کاربر";

            ViewBag.userId = userId ?? 0;

            if (userId == null)
            {
                var userModel = new User();

                return View(userModel);
            }
            else
            {
                ViewBag.siteId = userId;
                var userModel = _userDataProvider.GetUser(userId ?? 1);
                return View(userModel);
            }
        }

    }
}