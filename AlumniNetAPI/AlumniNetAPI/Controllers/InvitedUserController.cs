using Microsoft.AspNetCore.Mvc;

namespace AlumniNetAPI.Controllers
{
    public class InvitedUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
