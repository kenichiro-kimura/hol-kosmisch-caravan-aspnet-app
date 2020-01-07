using MyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string message = HttpContext.Session.GetString("message");
            return View(new MyForm {Message = message });
        }

        [HttpPost]
        public ActionResult Index(MyForm item)
        {
            HttpContext.Session.SetString("message", item.Message);
            return RedirectToAction("Index");
        }
    }
}