using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projekt_Zaliczeniowy.Data;


var builder = WebApplication.CreateBuilder(args);

// Dodanie DbContext do aplikacji
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie Identity do aplikacji
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Brak wymogu potwierdzenia konta
    options.Password.RequireDigit = true;          // Wymóg cyfr w has³ach
    options.Password.RequiredLength = 6;           // Minimalna d³ugoœæ has³a
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Dodanie konfiguracji cookies dla Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // Œcie¿ka do strony logowania
    options.LogoutPath = "/Account/Logout";        // Œcie¿ka do wylogowania
    options.AccessDeniedPath = "/Account/AccessDenied"; // Strona wyœwietlana przy braku dostêpu
});

// Dodanie Razor Pages (konieczne do obs³ugi stron Razor)
builder.Services.AddRazorPages();

// Dodanie kontrolerów i widoków
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfiguracja middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Middleware uwierzytelniania
app.UseAuthorization();  // Middleware autoryzacji

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Dodanie mapowania Razor Pages

app.Run();

