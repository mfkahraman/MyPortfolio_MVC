using MyPortfolio_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var experienceList = db.Experiences.ToList();
            return View(experienceList);
        }

        [HttpGet]
        public ActionResult CreateExperience()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateExperience(Experience model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Experiences.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Deneyim başarıyla eklendi!";
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
        public ActionResult UpdateExperience(int id)
        {
            var experience = db.Experiences.Find(id);
            if (experience == null)
            {
                return HttpNotFound();
            }

            return View(experience);
        }

        [HttpPost]
        public ActionResult UpdateExperience(Experience model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var experienceToUpdate = db.Experiences.Find(model.Id);
                    if (experienceToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    experienceToUpdate.CompanyName = model.CompanyName;
                    experienceToUpdate.Title = model.Title;
                    experienceToUpdate.StartDate = model.StartDate;
                    experienceToUpdate.EndDate = model.EndDate;
                    experienceToUpdate.Description = model.Description;

                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Deneyim başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult DeleteExperience(int id)
        {
            try
            {
                var experience = db.Experiences.Find(id);
                if (experience == null)
                {
                    return HttpNotFound();
                }

                db.Experiences.Remove(experience);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Deneyim başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
