using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Repositories;

public static class DatabaseDependencyInjection {
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")));
        });
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}