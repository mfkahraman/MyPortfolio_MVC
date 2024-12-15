using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class AboutController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var abouts = db.Abouts.ToList();
            return View(abouts);
        }

        public ActionResult DeleteAbout(int id)
        {
            var about = db.Abouts.Find(id);
            db.Abouts.Remove(about);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAbout(About model)
        {
            if (model.AboutImage != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "wwwroot\\images\\About\\";
                var fileName = Path.Combine(saveLocation, model.AboutImage.FileName);
                model.AboutImage.SaveAs(fileName);
                model.ImageUrl = "/wwwroot/images/About/" + model.AboutImage.FileName;
            }
            else
            {
                ModelState.AddModelError("", "Hakkımda fotoğrafı yükleyin");
                return View(model);
            }

            if (model.CvFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "wwwroot\\CV\\";
                var fileName = Path.Combine(saveLocation, model.CvFile.FileName);
                model.CvFile.SaveAs(fileName);
                model.CvUrl = "/wwwroot/CV/" + model.CvFile.FileName;
            }
            else
            {
                ModelState.AddModelError("", "Cv yükleyin");
                return View(model);
            }
            db.Abouts.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {
            var entity = db.Abouts.Find(id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult UpdateAbout(About model)
        {
           var entity = db.Abouts.Find(model.Id);
           if (model.AboutImage != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "wwwroot\\images\\About\\";
                var fileName = Path.Combine(saveLocation, model.AboutImage.FileName);
                model.AboutImage.SaveAs(fileName);
                entity.ImageUrl = "/wwwroot/images/About/" + model.AboutImage.FileName;
            }
            else
            {
                ModelState.AddModelError("", "Hakkımda fotoğrafı yükleyin");
                return View(model);
            }

            if (model.CvFile != null)
            {
                var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                var saveLocation = currentDirectory + "wwwroot\\CV\\";
                var fileName = Path.Combine(saveLocation, model.CvFile.FileName);
                model.CvFile.SaveAs(fileName);
                entity.CvUrl = "/wwwroot/CV/" + model.CvFile.FileName;
            }
            else
            {
                ModelState.AddModelError("", "Cv yükleyin");
                return View(model);
            }
            entity.Title = model.Title;
            entity.Description = model.Description;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}