using System;
using System.Linq;
using System.Web.Mvc;
using MyPortfolio_MVC.Models;

namespace MyPortfolio_MVC.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly MyPortfolioEntities db = new MyPortfolioEntities();

        public ActionResult Index()
        {
            var testimonials = db.Testimonials.ToList();
            return View(testimonials);
        }

        public ActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (testimonial.ImageFile != null && testimonial.ImageFile.ContentLength > 0)
                    {
                        string directoryPath = Server.MapPath("~/wwwroot/images/Testimonials/");
                        string fileName = Guid.NewGuid() + "_" + testimonial.ImageFile.FileName;

                        if (!System.IO.Directory.Exists(directoryPath))
                        {
                            System.IO.Directory.CreateDirectory(directoryPath);
                        }

                        string filePath = System.IO.Path.Combine(directoryPath, fileName);
                        testimonial.ImageFile.SaveAs(filePath);

                        testimonial.ImageUrl = "/wwwroot/images/Testimonials/" + fileName;
                    }

                    db.Testimonials.Add(testimonial);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }

            return View(testimonial);
        }
        public ActionResult UpdateTestimonial(int id)
        {
            var testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            return View(testimonial);
        }

        [HttpPost]
        public ActionResult UpdateTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingTestimonial = db.Testimonials.Find(testimonial.Id);
                    if (existingTestimonial == null)
                    {
                        return HttpNotFound();
                    }

                    if (testimonial.ImageFile != null && testimonial.ImageFile.ContentLength > 0)
                    {
                        string directoryPath = Server.MapPath("~/wwwroot/images/Testimonials/");
                        string fileName = Guid.NewGuid() + "_" + testimonial.ImageFile.FileName;

                        if (!System.IO.Directory.Exists(directoryPath))
                        {
                            System.IO.Directory.CreateDirectory(directoryPath);
                        }

                        string filePath = System.IO.Path.Combine(directoryPath, fileName);
                        testimonial.ImageFile.SaveAs(filePath);

                        if (!string.IsNullOrEmpty(existingTestimonial.ImageUrl))
                        {
                            string oldFilePath = Server.MapPath(existingTestimonial.ImageUrl);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        existingTestimonial.ImageUrl = "/wwwroot/images/Testimonials/" + fileName;
                    }

                    existingTestimonial.NameSurname = testimonial.NameSurname;
                    existingTestimonial.Title = testimonial.Title;
                    existingTestimonial.Comment = testimonial.Comment;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Bir hata oluştu: " + ex.Message);
                }
            }

            return View(testimonial);
        }

        public ActionResult DeleteTestimonial(int id)
        {
            var testimonial = db.Testimonials.Find(id);
            if (testimonial == null)
            {
                return HttpNotFound();
            }
            db.Testimonials.Remove(testimonial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
