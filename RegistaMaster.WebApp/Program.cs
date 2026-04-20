using RegistaMaster.Infrastructure.Registiration;
using RegistaMaster.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(480);
});

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDatabase(connectionString);
builder.Services.MyRepository();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
