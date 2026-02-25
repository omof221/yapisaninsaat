using Microsoft.EntityFrameworkCore;
using yapisaninsaat.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Yapiİnsaat;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;"));

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "about",
    pattern: "hakkimizda",
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "projects-list",
    pattern: "projeler",
    defaults: new { controller = "Home", action = "Projects" });

app.MapControllerRoute(
    name: "project-detail",
    pattern: "proje/{slug}",
    defaults: new { controller = "Home", action = "ProjectDetail" });

app.MapControllerRoute(
    name: "team-members",
    pattern: "TeamMembers/{action=Index}/{id?}",
    defaults: new { controller = "TeamMembers" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
