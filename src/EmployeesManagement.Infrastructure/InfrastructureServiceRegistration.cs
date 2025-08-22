using EmployeesManagement.Application.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmployeesManagement.Infrastructure.Repositories;

namespace EmployeesManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Application Db Context
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("LocalDB"));
        });

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped(serviceType: typeof(IGenericRepository<>), implementationType: typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // services
        //services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

        // automapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
