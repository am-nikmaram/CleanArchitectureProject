using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    #region Newmethod


    //public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    //{

    //    services.AddDbContext<ApplicationDbContext>(options =>
    //    {
    //        options
    //            .UseSqlServer(configuration.GetConnectionString("SqlServer"));
    //    });

    //    return services;
    //}

    //public static async Task ApplyMigrationsAsync(this  WebApplication app)
    //{
    //    await using var scope = app.Services.CreateAsyncScope();
    //    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

    //    if (context is null)
    //        throw new Exception("Database Context Not Found");

    //    await context.Database.MigrateAsync();
    //}
    #endregion
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
        return services;
    }

    public static async Task ApplyMigrationsAsync(this IApplicationBuilder app)
    {
        await using var scope = app.ApplicationServices.CreateAsyncScope();
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        if (context is null)
            throw new Exception("Database Context Not Found");

        await context.Database.MigrateAsync();
    }


}