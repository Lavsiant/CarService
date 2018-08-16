using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarService.Services.Implementation;
using CarService.Services.Interfaces;
using CarService.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace CarService.Controllers
{
    public class AccountController : Controller
    {
        IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                IUser user = await _accountService.GetUser(model);
                if (user != null)
                {
                    await Authenticate(user.ID.ToString());

                    return RedirectToAction("Index", "Cars");
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                bool isLoginValid = await _accountService.CheckIfLoginValid(model.Login);
                if (isLoginValid)
                {
                    await _accountService.RegisterUser(model);
                    var user = await _accountService.GetUser(new LoginVM() { Login = model.Login, Password = model.Password });
                    await Authenticate(user.ID.ToString());

                    return RedirectToAction("Index", "Cars");
                }
                else
                    ModelState.AddModelError("", "Incorrect login or password");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]

        public async Task<IActionResult> Details(int id)
        {
            var detailsVM = new ProfileDetailsVm()
            {
                IsValidForEditing = (Convert.ToInt32(User.Identity.Name) == id),
                Owner = await _accountService.GetOwnerWithCars(id)
            };
            return View(detailsVM);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var owner = await _accountService.GetOwner(id);
            var vm = new ProfileEditVM()
            {
                ID = owner.ID,
                Surname = owner.Surname,
                Name = owner.Name,
                BornDate = owner.BornDate,
                Login = owner.Login,
                Password = owner.Password,
                DrivingExperience = owner.DrivingExperience
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfileEditVM profileVM)
        {
            if (id != profileVM.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _accountService.UpdateAccount(profileVM);

            }

            var vm = new ProfileDetailsVm()
            {
                IsValidForEditing = (Convert.ToInt32(User.Identity.Name) == id),
                Owner = await _accountService.GetOwnerWithCars(Convert.ToInt32(User.Identity.Name))
            };
            return View("../Account/Details", vm);         
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

            await _accountService.DeleteAccount(id);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string userName)
        {           
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
           
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}