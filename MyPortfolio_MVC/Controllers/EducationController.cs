using MyPortfolio_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class EducationController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var educationList = db.Educations.ToList();
            return View(educationList);
        }

        [HttpGet]
        public ActionResult CreateEducation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEducation(Education model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Educations.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Eğitim bilgisi başarıyla oluşturuldu!";
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
        public ActionResult UpdateEducation(int id)
        {
            var education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }

            return View(education);
        }

        [HttpPost]
        public ActionResult UpdateEducation(Education model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var educationToUpdate = db.Educations.Find(model.Id);
                    if (educationToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    educationToUpdate.SchoolName = model.SchoolName;
                    educationToUpdate.Department = model.Department;
                    educationToUpdate.StartDate = model.StartDate;
                    educationToUpdate.EndDate = model.EndDate;
                    educationToUpdate.Description = model.Description;
                    educationToUpdate.Degree = model.Degree;

                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Eğitim bilgisi başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var education = db.Educations.Find(id);
                if (education == null)
                {
                    return HttpNotFound();
                }

                db.Educations.Remove(education);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Eğitim bilgisi başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
