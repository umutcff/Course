using Microsoft.AspNetCore.Mvc;

namespace Simulation_2.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
