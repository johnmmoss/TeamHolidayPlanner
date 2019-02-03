using Microsoft.AspNetCore.Mvc;

namespace Web
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}