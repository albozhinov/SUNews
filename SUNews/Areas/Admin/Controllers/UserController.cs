﻿namespace SUNews.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SUNews.Areas.Admin.Models;
    using SUNews.Data.Models;
    using SUNews.Providers;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IRoleManager<IdentityRole> roleManager;
        private readonly IUserManager<User> userManager;
        private readonly IRolesProvider createRolesProvider;

        public UserController(IRoleManager<IdentityRole> _roleManager,
                              IUserManager<User> _userManager,
        IRolesProvider _createRolesProvider)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            createRolesProvider = _createRolesProvider;
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Authorize(Roles = "Owner")]
        [Authorize(Roles = "Manager")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateRole(string role)
        {

            if (!ModelState.IsValid)
            {
                ViewData[MessageConstant.ErrorMessage] = $"Please submit correct value!";

                return View(role);
            }

            var result = await createRolesProvider.CreateRole(role);

            ViewData[result.Item1] = result.Item2;

            return View();
        }

        [Authorize(Roles = "Administrator, Owner, Manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = userManager.Users.ToList();

            var model = allUsers.Select(u => new AllUsersViewModel(u)).ToList();

            return View("Users", model);
        }

        //[Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Owner")]
        //[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> GetAllAdmins()
        //{
        //    var allAdmins = await userManager.GetUsersInRoleAsync("Administrator");


        //    return View("Users", allAdmins);
        //}

        public async Task<IActionResult> DetailOfUser(string userId)
        {
            //var users = await service.GetUsers();

            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            var idetityUser = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(idetityUser);

            var model = new UserEditViewModel()
            {
                FirstName = idetityUser.FirstName ?? idetityUser.Email,
                LastName = idetityUser.LastName ?? idetityUser.Email,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            //var model = await service.GetUserForEdit(id);

            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LockUser(UserModalModelView input)
        //{
        //    var user = userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
        //    if (user is null)
        //    {
        //        return this.RedirectToAction(nameof(Index));
        //    }

        //    var enableLockOutResult = await userManager.SetLockoutEnabledAsync(user, true);
        //    if (!enableLockOutResult.Succeeded)
        //    {
        //        return this.RedirectToAction(nameof(Index));
        //    }
        //    var lockoutTimeResult = await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));
        //    if (!lockoutTimeResult.Succeeded)
        //    {
        //        return this.RedirectToAction(nameof(Index));
        //    }
        //    return this.RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> UnlockUser(UserModalModelView input)
        //{
        //    var user = userManager.Users.Where(u => u.Id == input.ID).FirstOrDefault();
        //    if (user is null)
        //    {
        //        return this.RedirectToAction(nameof(Index));
        //    }

        //    var lockoutTimeResult = await userManager.SetLockoutEndDateAsync(user, DateTime.Now);
        //    if (!lockoutTimeResult.Succeeded)
        //    {
        //        return this.RedirectToAction(nameof(Index));
        //    }
        //    return this.RedirectToAction(nameof(Index));
        //}
    }
}
