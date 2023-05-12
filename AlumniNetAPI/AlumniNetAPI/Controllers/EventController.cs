using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
