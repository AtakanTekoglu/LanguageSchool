using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LanguageSchool.Models;

namespace LanguageSchool.Controllers
{
    
    public class SecurityController : Controller
    {
        LanguageAcademyEntities db = new LanguageAcademyEntities();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Staff staff)
        {
            var user = db.Staffs.FirstOrDefault(x => x.Username == staff.Username && x.Password == staff.Password);
            if (user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.Username,false);
                ViewBag.layoutuser = user.Username;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Geçersiz kullanıcı adı veya şifre girdiniz!";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login","Security");
        }
    }
}