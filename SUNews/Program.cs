using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SUNews.Data.Context;
using SUNews.Data.Models;
using SUNews.Data.Repository;
using SUNews.Providers;
using SUNews.Services.Contracts;
using SUNews.Services.Providers;
using SUNews.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SUNewsDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SUNewsDbContext>()
    .AddDefaultTokenProviders();


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
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IValidatorService, ValidatorService>();
builder.Services.AddScoped(typeof(IUserManager<>), typeof(UserManagerWrapper<>));
builder.Services.AddScoped(typeof(IRoleManager<>), typeof(RoleManagerWrapper<>));
builder.Services.AddScoped<IRolesProvider, RolesProvider>();

// Global Automatically validate antiforgery tokens for unsafe HTTP methods only
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
})
                .AddRazorRuntimeCompilation();


/// <summary>
/// External login providers authentication
/// </summary>
builder.Services.AddAuthentication()
   .AddGoogle(googleOptions =>
   {
       googleOptions.ClientId = builder.Configuration["Google:ClientId"];
       googleOptions.ClientSecret = builder.Configuration["Google:ClientSecret"];
   })
   .AddFacebook(facebookOptions =>
   {
       facebookOptions.ClientId = builder.Configuration["Facebook:AppId"];
       facebookOptions.ClientSecret = builder.Configuration["Facebook:AppSecret"];
   });

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
    name: "adminArea",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
