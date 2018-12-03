using Microsoft.AspNetCore.Mvc;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class Holidays : Controller
    {
        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        [HttpGet]
        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Balance()
        {
            return View();
        }
    }
}
