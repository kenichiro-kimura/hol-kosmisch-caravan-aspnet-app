using MyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache cache;

        public HomeController(IDistributedCache cache){
            this.cache = cache;
        }
        public IActionResult Index()
        {
            string message = cache.GetString("message");
            ViewBag.Message = message;
            return View(new MyForm {Message = message });
        }

        [HttpPost]
        public IActionResult Index(MyForm item)
        {
            cache.SetString("message", item.Message);
            return RedirectToAction("Index");
        }
    }
}