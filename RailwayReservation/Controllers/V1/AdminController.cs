using Microsoft.AspNetCore.Mvc;

namespace RailwayReservation.Controllers.V1
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
