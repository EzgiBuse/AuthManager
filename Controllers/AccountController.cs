﻿using AuthManager.Models;
using AuthManager.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new();
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                { UserName = model.Email,
                    Email = model.Email ,
                    Name=model.Name
                
                };

               var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }
                AddErrors(result);
            }

           
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            LoginViewModel loginViewModel = new();
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,true);
                if (result.Succeeded)
                {
                    
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
                return View(model);
            }


            return View(model);
        }
        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            return View(model);
        }
    }
}
