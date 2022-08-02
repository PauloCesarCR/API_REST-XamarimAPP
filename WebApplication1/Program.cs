
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Repositorio;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BancoContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("BancoContext")));


builder.Services.AddScoped<IUsersRepository, UsersRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Meu Swagger", Version = "v1" });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (!app.Environment.IsDevelopment())
{
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
