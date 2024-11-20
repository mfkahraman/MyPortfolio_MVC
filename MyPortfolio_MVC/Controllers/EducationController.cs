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

        [HttpGet]
        public ActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEducation(Education entity)
        {
            db.Educations.Add(entity);
            db.SaveChanges();
            return RedirectToAction(nameof(Index)); //nameof(Index) ile "Index" aynı işlemi yapıyor
        }

        [HttpGet]
        public ActionResult UpdateEducation(int id)
        {
            var entity = db.Educations.Find(id);
            return View(entity);
        }

        [HttpPost]
        public ActionResult UpdateEducation(Education model)
        {
            var tbl = db.Educations.Find(model.Id);
            tbl.SchoolName = model.SchoolName;
            tbl.Description = model.Description;
            tbl.StartDate = model.StartDate;
            tbl.EndDate = model.EndDate;
            tbl.Degree = model.Degree;
            tbl.Department = model.Department;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}