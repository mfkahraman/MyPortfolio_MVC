using System.Linq;
using System.Web.Mvc;
using MyPortfolio_MVC.Models;

namespace MyPortfolio_MVC.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var socialMediaList = db.SocialMedias.ToList();
            return View(socialMediaList);
        }

        public ActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSocialMedia(SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                db.SocialMedias.Add(socialMedia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialMedia);
        }

        public ActionResult UpdateSocialMedia(int id)
        {
            var socialMedia = db.SocialMedias.Find(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            return View(socialMedia);
        }

        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMedia socialMedia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(socialMedia).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(socialMedia);
        }

        public ActionResult DeleteSocialMedia(int id)
        {
            var socialMedia = db.SocialMedias.Find(id);
            if (socialMedia == null)
            {
                return HttpNotFound();
            }
            db.SocialMedias.Remove(socialMedia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
