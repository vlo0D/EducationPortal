using EducationPortal.BLL.DTO;
using EducationPortal.BLL.Interfaces;
using EducationPortal.BLL.Services;
using EducationPortal.DAL.DataContext;
using EducationPortal.DAL.Entities;
using EducationPortal.DAL.Initializer;
using EducationPortal.Web.Models;
using EducationPortal.Web.Validators;
using EducationPortal.Web.Validators.Materials;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IMaterialService, MaterialService>();
builder.Services.AddScoped<IUserService, UserService>();

//Add Validators
builder.Services.AddScoped<IValidator<AddingCourseModel>, CourseValidator>();
builder.Services.AddScoped<IValidator<SkillDTO>, SkillValidator>();
builder.Services.AddScoped<IValidator<ArticleMaterialDTO>, ArticleValidator>();
builder.Services.AddScoped<IValidator<BookMaterialDTO>, BookValidator>();
builder.Services.AddScoped<IValidator<VideoMaterialDTO>, VideoValidator>();

//Add connection to db
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PortalContext>(options => options.UseSqlServer(connection));
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<PortalContext>();

//
builder.Services.Configure<PageOptions>(builder.Configuration.GetSection(PageOptions.SectionName))
    .PostConfigure<PageOptions>(options =>
    {
        if (options.PageNumber < 1)
        {
            options.PageNumber = 1;
        }
        if (options.PageSize < 1 || options.PageSize > 100)
        {
            options.PageSize = 30;
        }
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);
        
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "DB initializing error");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=home}/{action=Index}/{id?}");

app.Run(); 