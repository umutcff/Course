using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Simulation_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeacherController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
