using MyPortfolio_MVC.Models;
using System.Linq;
using System.Web.Mvc;

public class MessageController : Controller
{
    private readonly MyPortfolioEntities db = new MyPortfolioEntities();

    public ActionResult Index()
    {
        var messages = db.Messages.ToList();
        return View(messages);
    }

    public ActionResult MessageDetail(int id)
    {
        var message = db.Messages.Find(id);
        if (message == null)
        {
            return HttpNotFound();
        }

        if (!message.IsRead.HasValue || !message.IsRead.Value)
        {
            message.IsRead = true;
            db.SaveChanges();
        }

        return View(message); 
    }
    public ActionResult MarkAsRead(int id)
    {
        var message = db.Messages.Find(id);
        if (message == null)
        {
            return HttpNotFound();
        }

        message.IsRead = true;
        db.SaveChanges();

        TempData["SuccessMessage"] = "Mesaj başarıyla okundu olarak işaretlendi!";
        return RedirectToAction("Index");
    }
}
