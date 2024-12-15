using MyPortfolio_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class BannerController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var banners = db.Banners.ToList();
            return View(banners);
        }

        public ActionResult DeleteBanner(int id)
        {
            try
            {
                var entity = db.Banners.Find(id);
                if (entity == null)
                {
                    return HttpNotFound();
                }

                db.Banners.Remove(entity);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Banner başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateBanner(Banner model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.IsShown = true;
                    db.Banners.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Banner başarıyla oluşturuldu!";
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
        public ActionResult UpdateBanner(int id)
        {
            var entity = db.Banners.Find(id);
            if (entity == null)
            {
                return HttpNotFound();
            }
            return View(entity);
        }

        [HttpPost]
        public ActionResult UpdateBanner(Banner model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entityToUpdate = db.Banners.Find(model.Id);
                    if (entityToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    entityToUpdate.Title = model.Title;
                    entityToUpdate.Description = model.Description;
                    entityToUpdate.IsShown = model.IsShown;
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Banner başarıyla güncellendi!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
                }
            }

            return View(model);
        }

        public ActionResult ActivateBanner(int id)
        {
            try
            {
                var entity = db.Banners.Find(id);
                if (entity == null)
                {
                    return HttpNotFound();
                }

                entity.IsShown = true;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Banner aktif hale getirildi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeactivateBanner(int id)
        {
            try
            {
                var entity = db.Banners.Find(id);
                if (entity == null)
                {
                    return HttpNotFound();
                }

                entity.IsShown = false;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Banner pasif hale getirildi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
