using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using fp_18_web_aula_1_core.Identity;
using fp_18_web_aula_1_core.Models;
using fp_web_aula_1.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace fp_web_aula_1.Controllers
{
    public class AccountController : Controller
    {
        private const string ADMIN_ROLE = "admin";

        private readonly Manager _manager;
        private readonly Authentication _authentication;

        public AccountController(Authentication authentication, Manager manager)
        {
            _authentication = authentication;
            _manager = manager;
        }

        //[HttpGet]
        //public IActionResult Index(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Times");
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Index(ViewModel.LoginViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    if (ModelState.IsValid)
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(model.UseName, model.Password, model.IsPersistent, lockoutOnFailure: false);
        //        if (result.Succeeded)
        //            return RedirectToLocal(returnUrl);
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
        //            return View(model);
        //        }
        //    }

        //    return View(model);


        //    // If we got this far, something failed, redisplay form
        //    //return View(model);
        //    //if (model.UseName == "rodolfo" && model.Password == "123")
        //    //{
        //    //    var claims = new List<Claim>();
        //    //    claims.Add(new Claim(ClaimTypes.Name, model.UseName));
        //    //    var id = new ClaimsIdentity(claims, "password");
        //    //    claims.Add(new Claim(ClaimTypes.Role, "admin"));
        //    //    var principal = new ClaimsPrincipal(id);

        //    //    await HttpContext.SignInAsync("app", principal, new AuthenticationProperties() { IsPersistent = model.IsPersistent });

        //    //    return RedirectToAction("Index", "Times");
        //    //}
        //    //return View();
        //}

        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Register(string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.UseName, Email = model.UseName };
        //        var identityUser = await _userManager.CreateAsync(user, model.Password);


        //        if (identityUser.Succeeded)
        //        {
        //            var claims = new List<Claim>();
        //            claims.Add(new Claim(ClaimTypes.Name, model.UseName));
        //            var id = new ClaimsIdentity(claims, "password");
        //            claims.Add(new Claim(ClaimTypes.Role, "admin"));
        //            var principal = new ClaimsPrincipal(id);

        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToAction(nameof(HomeController.Index), "Home");
        //        }
        //    }

        //    return View(model);
        //}

        //public async Task<IActionResult> Logoff()
        //{
        //    await HttpContext.SignOutAsync();

        //    return RedirectToAction("Index", "Home");
        //}

        //private IActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction(nameof(HomeController.Index), "Home");
        //    }
        //}


        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Times");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            var result = await _authentication.Authenticate(vm.UserName, vm.Password);
            if (result)
                return Redirect("/");
            else
            {
                ModelState.AddModelError(string.Empty, "Login ou senha inválido!");
                return View(vm);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _authentication.Logout();
            return Redirect("/Account/Index");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel viewModel)
        {
            await _manager.CreateAsync(viewModel.UserName, viewModel.Password, ADMIN_ROLE);
            return Redirect("/Times/Index");
        }
    }
}