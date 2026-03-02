using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Sesión (simple, para proyecto estudiante)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AutosColombia.Session";
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseStaticFiles();
app.UseSession();

// "Protección" muy básica: si no hay sesión, no entra a Celdas/Vehículos
app.Use(async (context, next) =>
{
    var path = (context.Request.Path.Value ?? "").ToLowerInvariant();

    var requiereLogin =
        path.StartsWith("/vehiculos") ||
        path.StartsWith("/celdas");

    if (requiereLogin)
    {
        var usuario = context.Session.GetString("usuario");
        if (string.IsNullOrWhiteSpace(usuario))
        {
            context.Response.Redirect("/Login/Index");
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
