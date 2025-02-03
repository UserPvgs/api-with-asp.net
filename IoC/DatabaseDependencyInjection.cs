using Data.ApplicationDbContext;
using LearningCSharp.Models.Repositories;
using Microsoft.EntityFrameworkCore;

public static class DatabaseDependencyInjection {
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), 
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            );
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}