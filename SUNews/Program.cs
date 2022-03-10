using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SUNews.Data.Context;
using SUNews.Data.Models;
using SUNews.Data.Repository;
using SUNews.Providers;
using SUNews.Services.Contracts;
using SUNews.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SUNewsDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SUNewsDbContext>();

/// <summary>
/// This isn't work! I don't known why...
/// </summary>
//builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<SUNewsDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

}
);

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IValidatorService, ValidatorService>();
builder.Services.AddScoped(typeof(IUserManager<>), typeof(UserManagerWrapper<>));
builder.Services.AddScoped(typeof(IRoleManager<>), typeof(RoleManagerWrapper<>));

builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

// Check this if work correct!!!
//builder.Services.Configure<ServiceProvider>(async (options) =>
//{
//    var roleManager = options.GetRequiredService<RoleManager<IdentityRole>>();
//    var userManager = options.GetRequiredService<UserManager<User>>();

//    //Adding Admin Role 
//    var roleCheck = await roleManager.RoleExistsAsync("Admin");
//    if (!roleCheck)
//    {
//        //create the roles and seed them to the database 
//        await roleManager.CreateAsync(new IdentityRole("Admin"));
//    }

//    //Assign Admin role to the main User here we have given our newly registered
//    //login id for Admin management
//    var user = await userManager.FindByEmailAsync("admin@abv.bg");
//    var User = new User();
//    await userManager.AddToRoleAsync(user, "Admin");
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "adminArea",
//    pattern: "{area:exists}/{controller=Users}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
