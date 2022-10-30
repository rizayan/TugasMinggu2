using Microsoft.AspNetCore.Mvc;
using TMinggu2.Models;
using TMinggu2.Services;

namespace TMinggu2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(CreateUser user)
        {
            try
            {
                var result = await _user.Registration1(user);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data baru {result.Username}</div>";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(CreateUser user)
        {
            try
            {
                var result = await _user.Login(user);
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    HttpContext.Session.SetString("token", $"Bearer {result.Token}");
                }
                //TempData["pesan"] = $"<div class='alert alert-primary' role='alert'>Berhasil Login dengan Token : {result.Token} <button type='button' class='btn-close' data-bs-dismiss='alert'></button></ div > ";
                return RedirectToAction("Index", "Home");
                    //View();

            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
        }
    }
}
