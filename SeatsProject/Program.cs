using Microsoft.EntityFrameworkCore;
using SeatsProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SeatsProjectContext>(options =>
{
    options.UseSqlServer("Server=LMP2CSHPW\\SQLEXPRESS;Database=SeatsProject1;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=True");
});
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
    name: "default",
    pattern: "{controller=Sede}/{action=Index}/{id?}");

app.Run();
