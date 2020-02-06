using MyWebApp.Models;
using System.Web.Mvc;

namespace MyWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = Session["message"]?.ToString() ?? "";

            var webTask = HttpAccessAsync();
            webTask.Wait();

            return View();
        }

        [HttpPost]
        public ActionResult Index(MyForm item)
        {
            if (!string.IsNullOrWhiteSpace(item?.Message))
            {
                Session["message"] = item.Message;
            }
            return RedirectToAction("Index");
        }

        private static async Task<string> HttpAccessAsync()
        {
            //string url = "https://192.168.1.1";

            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetStringAsync("192.168.1.1");
                    // Webアクセス(GET)
                    return result;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }
}