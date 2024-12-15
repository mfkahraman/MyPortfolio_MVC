using MyPortfolio_MVC.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolio_MVC.Controllers
{
    public class ContactController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var contacts = db.Contacts.ToList();
            return View(contacts);
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Contacts.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "İletişim bilgisi başarıyla oluşturuldu!";
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
        public ActionResult UpdateContact(int id)
        {
            var contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        [HttpPost]
        public ActionResult UpdateContact(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var contactToUpdate = db.Contacts.Find(model.Id);
                    if (contactToUpdate == null)
                    {
                        return HttpNotFound();
                    }

                    contactToUpdate.Phone = model.Phone;
                    contactToUpdate.Email = model.Email;
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "İletişim bilgisi başarıyla güncellendi!";
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
                var contact = db.Contacts.Find(id);
                if (contact == null)
                {
                    return HttpNotFound();
                }

                db.Contacts.Remove(contact);
                db.SaveChanges();
                TempData["SuccessMessage"] = "İletişim bilgisi başarıyla silindi!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
