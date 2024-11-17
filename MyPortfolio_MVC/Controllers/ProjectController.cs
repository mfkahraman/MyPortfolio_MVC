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
        public ActionResult Index()
        {
            var projects = db.Projects.ToList();
            return View(projects);
        }
    }
}