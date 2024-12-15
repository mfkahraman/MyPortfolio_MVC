using MyPortfolio_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ExpertiseController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var expertiseList = db.Expertises.ToList();
            return View(expertiseList);
        }

        [HttpGet]
        public ActionResult CreateExpertise()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateExpertise(Expertise model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Expertises.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Uzmanlık başarıyla eklendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult UpdateExpertise(int id)
        {
            var expertise = db.Expertises.Find(id);
            if (expertise == null)
            {
                return HttpNotFound();
            }

            return View(expertise);
        }

        [HttpPost]
        public ActionResult UpdateExpertise(Expertise model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var expertiseToUpdate = db.Expertises.Find(model.Id);
                    if (expertiseToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    expertiseToUpdate.Title = model.Title;
                    expertiseToUpdate.Description = model.Description;
                    expertiseToUpdate.Level = model.Level;
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Uzmanlık başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult DeleteExpertise(int id)
        {
            try
            {
                var expertise = db.Expertises.Find(id);
                if (expertise == null)
                {
                    return HttpNotFound();
                }

                db.Expertises.Remove(expertise);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Uzmanlık başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
