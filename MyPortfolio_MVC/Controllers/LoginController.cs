using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPortfolio_MVC.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Tbl_Admin model)
        {
            var value = db.Tbl_Admins.FirstOrDefault(x=> x.Email == model.Email && x.Password==model.Password);
            if (value == null)
            {
                ModelState.AddModelError("", "Email veya Şifre hatalı");
                return View(model);
            }
            FormsAuthentication.SetAuthCookie(value.Email, false);

            Session["email"] = value.Email;
            return RedirectToAction("Index", "Profile");
        }
    }
}