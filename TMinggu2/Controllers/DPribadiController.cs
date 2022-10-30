using Microsoft.AspNetCore.Mvc;
using TMinggu2.Models;
using TMinggu2.Services;

namespace TMinggu2.Controllers
{
    public class DPribadiController : Controller
    {
        private readonly IDPribadi _dpribadi;

        public DPribadiController(IDPribadi dpribadi)
        {
            _dpribadi = dpribadi;
        }
        public async Task<IActionResult> Index(string? name, int? nik)
        {
            /* var results = await _student.GetAll();
             string strResult = string.Empty;
             foreach(var result in results)
             {
                 strResult += result.FirstMidName + "\n";
             }
             return Content(strResult);*/
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<DPribadi> model;

            if (name == null)
            {
                model = await _dpribadi.GetAll();


            }
           
            else
            {
                model = await _dpribadi.GetByName(name);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var model = await _dpribadi.GetById(id);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DPribadi samurai)
        {
            try
            {
                var result = await _dpribadi.Insert(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data student {result.NamaLengkap}</div>";
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
            var model = await _dpribadi.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DPribadi samurai)
        {
            try
            {
                var result = await _dpribadi.Update(samurai);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil mengupdate data samurai {result.NamaLengkap}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _dpribadi.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _dpribadi.Delete(id);
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
