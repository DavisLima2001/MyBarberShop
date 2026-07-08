using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyBarberShop.Models;
using MyBarberShop.Services;
using System.Security.Claims;
    

namespace MyBarberShop.Controllers
{
    public class AccountController(UserService _userService) : Controller
    {
        public IActionResult Login()
        {
            var viewModel = new LoginVM();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewmodel)
        {
            
            if (!ModelState.IsValid) return View(viewmodel);
            var found = await _userService.Login(viewmodel);

            if (found.UserId == 0)
            {
               
               
                ViewBag.message = "No se encontro, Ningun resultado.";
                return View();

            }
            else
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,found.UserId.ToString()),
                    new Claim(ClaimTypes.Name,found.FullName),
                    new Claim(ClaimTypes.Email, found.Email),
                    new Claim(ClaimTypes.Role, found.type)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties() { AllowRefresh = true }
                    );

                return RedirectToAction("Index", "Home");
            }


        }

        public IActionResult Register()
        {
            var viewModel = new UserVm();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVm viewmodel)
        {

            if (!ModelState.IsValid) return View(viewmodel);
            try
            {
                await _userService.Register(viewmodel);
                ViewBag.message = "Tu cuenta a sido registrada, por favor inicia sesion";
                ViewBag.Class="alert-success";
            }
            catch (Exception ex) {
                ViewBag.message = ex.Message;
                ViewBag.Class = "alert-danger";

            }

            return View();


        }

       
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
