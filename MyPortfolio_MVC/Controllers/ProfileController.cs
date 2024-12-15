using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();
        public ActionResult Index()
        {
            string email = Session["email"].ToString();
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index","Login");
            }
            var admin = db.Tbl_Admins.FirstOrDefault(x => x.Email == email);
            return View(admin);
        }

        [HttpPost]
        public ActionResult Index(Tbl_Admin model)
        {
            string email = Session["email"].ToString();
            var admin = db.Tbl_Admins.FirstOrDefault(x=>x.Email == email);

            if (admin.Password == model.Password)
            {
                if (model.ImageFile != null)
                {
                    var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var saveLocation = currentDirectory + "wwwroot\\images\\ProfilePhoto\\";
                    var fileName = Path.Combine(saveLocation, model.ImageFile.FileName);
                    model.ImageFile.SaveAs(fileName);
                    admin.ImageUrl = "/wwwroot/images/ProfilePhoto/" + model.ImageFile.FileName;
                }

                admin.Name = model.Name;
                admin.Surname = model.Surname;
                admin.Email = model.Email;
                db.SaveChanges();
                Session.Abandon();
                return RedirectToAction("Index","Login");
            }

            ModelState.AddModelError("", "Girdiğiniz Şifre Hatalıdır");
            return View(model);


        }
    }
}