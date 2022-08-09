using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositorio;
using Serilog;
using Serilog.Events;

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, config) =>
    {
        var connectionString = context.Configuration.GetConnectionString("BancoContext");
        config.WriteTo.PostgreSQL(connectionString, "Logs", needAutoCreateTable: true).WriteTo.Console().MinimumLevel.Information();
    });
    builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

    builder.Services.AddControllersWithViews();

    builder.Services.AddScoped<IUsersRepository, UsersRepository>();

    builder.Services.AddDbContext<BancoContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("BancoContext")));


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

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<BancoContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();




