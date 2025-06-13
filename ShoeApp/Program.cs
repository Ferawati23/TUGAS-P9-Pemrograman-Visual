using System;
using Microsoft.EntityFrameworkCore;
using ShoeApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan controller dan views (MVC)
builder.Services.AddControllersWithViews();

// Konfigurasi koneksi database MySQL/MariaDB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 6, 14)) // Sesuaikan versi MariaDB kamu di sini
    )
);

var app = builder.Build();

// Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // Tambahan untuk keamanan
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization(); // Jika kamu menggunakan autentikasi/otorisasi

// Routing default controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
