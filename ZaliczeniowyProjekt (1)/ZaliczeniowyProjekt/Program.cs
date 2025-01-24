using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZaliczeniowyProjekt.Models;

var builder = WebApplication.CreateBuilder(args);

// Dodajemy DbContext z SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodajemy Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Możesz skonfigurować zasady haseł itp.
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    // itp.
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Ustawiamy ścieżkę logowania na własny kontroler Account
builder.Services.ConfigureApplicationCookie(options =>
{
    // Gdzie przekieruje użytkownika, gdy spróbuje wejść na stronę wymagającą logowania
    options.LoginPath = "/Account/Login";
    // (Opcjonalnie) jeśli używasz ról i ograniczasz dostęp -> ścieżka dla AccessDenied
    // options.AccessDeniedPath = "/Account/AccessDenied";
});

// Dodaj usługi MVC (z widokami i kontrolerami)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Obsługa błędów i stron statycznych
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Używamy autentykacji i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Definiujemy domyślny routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
