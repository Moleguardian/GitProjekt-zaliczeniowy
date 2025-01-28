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
    options.Password.RequireDigit = true;          // Wym�g cyfr w has�ach
    options.Password.RequiredLength = 6;           // Minimalna d�ugo�� has�a
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Dodanie konfiguracji cookies dla Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";          // �cie�ka do strony logowania
    options.LogoutPath = "/Account/Logout";        // �cie�ka do wylogowania
    options.AccessDeniedPath = "/Account/AccessDenied"; // Strona wy�wietlana przy braku dost�pu
});

// Dodanie Razor Pages (konieczne do obs�ugi stron Razor)
builder.Services.AddRazorPages();

// Dodanie kontroler�w i widok�w
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

