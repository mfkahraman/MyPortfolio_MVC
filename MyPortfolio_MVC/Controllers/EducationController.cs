using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class EducationController : Controller
    {
        MyPortfolioEntities db = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var educations = db.Educations.ToList();
            return View(educations);
        }

        public ActionResult DeleteEducation(int id)
        {
            var value = db.Educations.Find(id);
            db.Educations.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}