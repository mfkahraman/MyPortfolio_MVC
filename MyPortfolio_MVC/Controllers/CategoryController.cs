using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyPortfolio_MVC.Models;

namespace MyPortfolio_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Kategori başarıyla eklendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var categoryToUpdate = db.Categories.Find(model.Id);
                    if (categoryToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    categoryToUpdate.Name = model.Name;
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Kategori başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult DeleteCategory(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
