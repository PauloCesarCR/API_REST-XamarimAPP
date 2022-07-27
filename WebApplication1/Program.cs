
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositorio;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BancoContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("BancoContext")));

    builder.Services.AddHealthChecks()
    .AddDbContextCheck<BancoContext>();

builder.Services.AddScoped<IUsersRepositorio, UsersRepositorio>();

var app = builder.Build();

app.MapHealthChecks("/health");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
