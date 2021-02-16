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

                if (!IsValidEmail(user.Email))
                {
                    ModelState.AddModelError("LockError",
                   "ایمیل وارد شده اشتباه می باشد  !"
                    );
                    return View();

                }
                if (_userDataProvider.ExitUser(user.UserName))
                {
                    ModelState.AddModelError("LockError",
                  "نام کاربری تکراری می باشد !"
                  );
                    return View();
                }
                var result = _userDataProvider.Insert(user);
                if (!result)
                {
                    ModelState.AddModelError("LockError",
                    "خطا در ذخیره سازی !"
                     );
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Success",
                     "کاربر جدید با موفقیت ثبت شد."
                     );
                    ViewBag.Message = "کاربر جدید با موفقیت ثبت شد";
                    return View("Users", "Users");
                }
                //return RedirectToAction("Users");
            }
            else
            {
                // .....update website
                user.Id = inituserId ?? 0;
                var result = _userDataProvider.Update(user, inituserId ?? 0);
                if (result)
                {
                    ModelState.AddModelError("LockError",
                 "ویرایش با موفقیت انجام شد."
                 );
                    return RedirectToAction("Users");
                }
                else
                {
                    ModelState.AddModelError("LockError",
                                    "خطا در ذخیره سازی !"
                                    );
                }

            }
            return RedirectToAction("Users");

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

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            _userDataProvider.Delete(userId);
            ModelState.AddModelError("Success",
              "کاربر جدید با موفقیت ثبت شد."
              );
            ViewBag.Message = "کاربر جدید با موفقیت ثبت شد";
            return RedirectToAction("Users");
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public ActionResult LockUser(int userId)
        {
            _userDataProvider.Lock(userId);

            return RedirectToAction("Users");
        }

        [HttpPost]
        public ActionResult ChangePassword(int userId , string oldPass , string newPass)
        {
           var user = _userDataProvider.GetUser(userId);
            if (oldPass==user.Password)
            {
                ModelState.AddModelError("LockError",
                  "پسورد وارد شده اشتباه می باشد  !"
                   );
                return View();
            }
            _userDataProvider.ChangePassword(userId, newPass);
            return RedirectToAction("Users");

        }
    }
}