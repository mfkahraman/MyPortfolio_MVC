using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        MyPortfolioEntities db = new MyPortfolioEntities();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DefaultBanner()
        {
            var values = db.Banners.Where(x=> x.IsShown==true).ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultExpertise()
        {
            var values = db.Expertises.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultExperience()
        {
            var values = db.Experiences.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultProject()
        {
            var values = db.Projects.ToList();
            return PartialView(values);
        }

        [HttpGet]
        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SendMessage(Message model)
        {
            model.IsRead = false;
            db.Messages.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult DefaultAbout()
        {
            var values = db.Abouts.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultEducation()
        {
            var values = db.Educations.ToList();
            return PartialView(values);
        }

        public PartialViewResult DefaultTestimonial()
        {
            var values = db.Testimonials.ToList();
            return PartialView(values);
        }
    }
}