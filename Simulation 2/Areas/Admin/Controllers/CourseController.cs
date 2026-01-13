using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Simulation_2.Contexts;
using Simulation_2.Helpers;
using Simulation_2.Models;
using Simulation_2.ViewModels;

namespace Simulation_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public CourseController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath,"images");
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Select(x => new CourseGetVM()
            {
                Tittle = x.Tittle,
                Description = x.Description,
                Id = x.Id,
                ImagePath = x.ImagePath

            }).ToListAsync();


            return View(courses);
        }


        public async Task<IActionResult> Create()
        {
            await _sendCategoriesWithViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateVM vm)
        {
            await _sendCategoriesWithViewBag();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var isExistItem = await _context.Courses.AnyAsync(x => x.Id == vm.Id);

            if (!isExistItem)
            {
                ModelState.AddModelError("CourseID", "This Course Id's not found!");
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("ImagePath", "Image's type must be image!");
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Image's size must be fewer than 2mb");
            }

            string uniqueFileName = await vm.Image.FileUploadAsync(_folderPath);




            Course course = new()
            {
                Tittle = vm.Tittle,
                Description = vm.Description,
                ImagePath = uniqueFileName
            };


            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            var deleteItem = await _context.Courses.FindAsync(id);

            if (deleteItem == null)
            {
                return BadRequest();
            }

            string deletedImagePath = Path.Combine(_folderPath, deleteItem.ImagePath);

            ExtensionMethods.FileDelete(deletedImagePath);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            await _sendCategoriesWithViewBag();

            var updateItem = await _context.Courses.FindAsync(id);

            if (updateItem == null)
            {
                return BadRequest();
            }



            CourseUpdateVM vm = new()
            {
                Tittle = updateItem.Tittle,
                Description = updateItem.Description,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateVM vm)
        {
            await _sendCategoriesWithViewBag();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!vm.Image.CheckType("image"))
            {
                ModelState.AddModelError("ImagePath", "Image's type must be image!");
            }

            if (!vm.Image.CheckSize(2))
            {
                ModelState.AddModelError("Image", "Image's size must be fewer than 2mb");
            }

            var existItem = await _context.Courses.FindAsync(vm.Id);

            if (existItem == null)
            {
                return BadRequest();
            }

            existItem.Tittle = vm.Tittle;
            existItem.Description = vm.Description;

            if (vm.Image is { })
            {
                string newImagePath = await vm.Image.FileUploadAsync(_folderPath);

                string oldImagePath = Path.Combine(_folderPath, existItem.ImagePath);
                ExtensionMethods.FileDelete(oldImagePath);

                existItem.ImagePath = newImagePath;
            }
            _context.Courses.Update(existItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }






        private async Task _sendCategoriesWithViewBag()
        {
            var courses = await _context.Courses.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Tittle
            }).ToListAsync();

            ViewBag.Courses = courses;
        }
    }
}

