using MyPortfolio_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MyPortfolioEntities db = new MyPortfolioEntities();
        public ActionResult Index()
        {
            var values = db.Messages.Where(x=> x.IsRead == false).ToList();
            return View(values);
        }

        public PartialViewResult MessageDetail(int? id)
        {
            if (id == null)
            {
                // Hata mesajını ViewBag ile taşıyoruz
                ViewBag.ErrorMessage = "Mesaj ID'si geçersiz.";
                return PartialView("_Error"); // Hata durumu için bir özel view döndürülüyor.
            }

            var message = db.Messages.Find(id);
            if (message == null)
            {
                // Mesaj bulunamazsa hata mesajı
                ViewBag.ErrorMessage = "Mesaj bulunamadı.";
                return PartialView("_Error"); // Hata durumu için aynı özel view kullanılıyor.
            }

            // Mesaj başarıyla bulunursa view'a modeli aktar
            return PartialView("MessageDetail", message);
        }


        [HttpPost]
        public ActionResult MakeMessageRead(int id)
        {
            var value = db.Messages.Find(id);
            value.IsRead = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}