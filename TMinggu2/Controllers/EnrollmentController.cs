using Microsoft.AspNetCore.Mvc;
using TMinggu2.Models;
using TMinggu2.Services;

namespace TMinggu2.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollment;

        public EnrollmentController(IEnrollment enrollment)
        {
            _enrollment = enrollment;
        }
        public async Task<IActionResult> Index()
        {
           
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Enrollment> model;
            model = await _enrollment.GetAll();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment samurai)
        {
            try
            {
                var result = await _enrollment.Insert(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan course ke student dengan Id enrollment: {result.EnrollmentID}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }
    }
}
