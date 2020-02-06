using System.Web.Mvc;

namespace MyWebApp.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }

        private static async Task<string> HttpAccessAsync()
        {
            //string url = "https://192.168.1.1";

            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetStringAsync("http://192.168.1.1/hogehoge");
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