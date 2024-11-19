using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ProjectController : Controller
    {
        MyPortfolioEntities db = new MyPortfolioEntities();
        // GET: Project
        private void CategoryDropdown()
        {
            var categoryList = db.Categories.ToList();
            List<SelectListItem> categories = (from x in categoryList
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString(),
                                               }).ToList();
            ViewBag.categories = categories;
        }

        public ActionResult Index()
        {
            var projects = db.Projects.ToList();
            return View(projects);
        }

        [HttpGet]
        public ActionResult CreateProject()
        {
            CategoryDropdown();
            return View();
        }



        [HttpPost]
        public ActionResult CreateProject(Project project)
        {
            CategoryDropdown();

            if (!ModelState.IsValid)
            {
                return View(project);
            }

            db.Projects.Add(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProject(int id)
        {
            var value = db.Projects.Find(id);
            db.Projects.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateProject(int id)
        {
            CategoryDropdown();
            var value = db.Projects.Find(id);
            return View(value);
        }

        [HttpPost]
        public ActionResult UpdateProject(Project project)
        {
            CategoryDropdown();
            var value = db.Projects.Find(project.Id);
            value.Name = project.Name;
            value.Description = project.Description;
            value.CategoryId = project.CategoryId;
            value.ImageUrl = project.ImageUrl;
            value.GithubUrl = project.GithubUrl;
            if (!ModelState.IsValid)
            {
                return View(project);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}