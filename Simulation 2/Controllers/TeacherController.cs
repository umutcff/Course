using Microsoft.AspNetCore.Mvc;

namespace Simulation_2.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
