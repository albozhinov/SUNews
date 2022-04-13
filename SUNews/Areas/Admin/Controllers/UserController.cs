namespace SUNews.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SUNews.Areas.Admin.Models;
    using SUNews.Data.Models;
    using SUNews.Providers;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;
    using X.PagedList;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IRoleManager<IdentityRole> roleManager;
        private readonly IUserManager<User> userManager;
        private readonly IRolesProvider createRolesProvider;

        private readonly int PAGE_SIZE = 3;

        public UserController(IRoleManager<IdentityRole> _roleManager,
                              IUserManager<User> _userManager,
        IRolesProvider _createRolesProvider)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            createRolesProvider = _createRolesProvider;
        }


        public async Task<IActionResult> Index()
        {
            var allUsers = userManager.Users.ToList();

            var model = new UserIndexViewModel(allUsers, 1, PAGE_SIZE);

            return View(model);
        }

        public IActionResult UserGrid(int? page)
        {
            var pagedUsers = userManager.Users
                                         .Select(u => new AllUsersViewModel(u))
                                         .ToPagedList(page ?? 1, PAGE_SIZE);

            return PartialView("_UserGrid", pagedUsers);
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
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


        //[Authorize(Roles = "Administrator")]
        //[Authorize(Roles = "Owner")]
        //[Authorize(Roles = "Manager")]
        //public async Task<IActionResult> GetAllAdmins()
        //{
        //    var allAdmins = await userManager.GetUsersInRoleAsync("Administrator");


        //    return View("Users", allAdmins);
        //}

        public async Task<IActionResult> Details(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user is null)
            {
                RedirectToAction(nameof(Index));
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var userViewModel = new UserDetailsViewModel(user);

            userViewModel.Role = String.Join(", ", userRoles);

            return View(userViewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var idetityUser = await userManager.FindByIdAsync(id);

            var roles = roleManager.Roles;
            var userRoles = await userManager.GetRolesAsync(idetityUser);


            var model = new UserEditViewModel(idetityUser);
            model.Roles = userRoles;
            model.RolesList = roles.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = model.Id });
            }

            var user = await userManager.FindByIdAsync(model.Id);
            var userRoles = await userManager.GetRolesAsync(user);


            var removeUserRoles = await userManager.RemoveFromRolesAsync(user, userRoles);


            var addUserToRoles = await userManager.AddToRolesAsync(user, model.Roles);
            if (!addUserToRoles.Succeeded)
            {
                return RedirectToAction("Edit", new { id = model.Id });
            }
            // Исках да вкарам повече фунцконалност с промянва на Firstname, Lastname and ect, но нямма време!

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LockUser(string id)
        {
            var user = userManager.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user is null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var enableLockOutResult = await userManager.SetLockoutEnabledAsync(user, true);
            if (!enableLockOutResult.Succeeded)
            {
                return this.RedirectToAction(nameof(Index));
            }
            var lockoutTimeResult = await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));
            if (!lockoutTimeResult.Succeeded)
            {
                return this.RedirectToAction(nameof(Index));
            }
            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnlockUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            var lockoutTimeResult = await userManager.SetLockoutEndDateAsync(user, null);
            if (!lockoutTimeResult.Succeeded)
            {
                return this.RedirectToAction(nameof(Index));
            }
            return this.RedirectToAction(nameof(Index));
        }
    }
}
