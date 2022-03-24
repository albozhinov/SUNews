namespace SUNews.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SUNews.Data.Models;
    using SUNews.Providers;
    using SUNews.Services.Constants;
    using SUNews.Services.Contracts;

    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly IUserService userSerivce;
        private readonly IRoleManager<IdentityRole> roleManager;
        private readonly IUserManager<User> userManager;
        private readonly ICreateRolesProvider createRolesProvider;

        public UserController(IUserService _userService,
                              IRoleManager<IdentityRole> _roleManager,
                              IUserManager<User> _userManager,
        ICreateRolesProvider _createRolesProvider)
        {
            userSerivce = _userService;
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

            var result = await createRolesProvider.CreateUserRoles(User, role);

            ViewData[result.Item1] = result.Item2;

            return View();
        }

        //public async Task<IActionResult> ManageUsers()
        //{
        //    //var users = await service.GetUsers();

        //    return View(users);
        //}

        //public async Task<IActionResult> Edit(string id)
        //{
        //    //var model = await service.GetUserForEdit(id);

        //    return View(model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(UserEditViewModel model)
        //{
        //    //var model = await service.GetUserForEdit(id);

        //    return View(model);
        //}
    }
}
