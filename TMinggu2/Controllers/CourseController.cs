using Microsoft.AspNetCore.Mvc;
using TMinggu2.Models;
using TMinggu2.Services;

namespace TMinggu2.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse _course;

        public CourseController(ICourse course)
        {
            _course = course;
        }
        public async Task<IActionResult> Index(string? name)
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Course> model;
            if (name == null)
            {
                model = await _course.GetAll();
            }
            else
            {
                model = await _course.GetByName(name);
            }
            return View(model);
        }

        public async Task<IActionResult> WithStudent()
        {
            var model = await _course.GetCourseWithStudent();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _course.GetById(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course samurai)
        {
            try
            {
                var result = await _course.Insert(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data student {result.Title}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }

        public async Task<IActionResult> Update(int id)
        {
            var model = await _course.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course samurai)
        {
            try
            {
                var result = await _course.Update(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data samurai {result.Title}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _course.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _course.Delete(id);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mendelete data student id: {id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
