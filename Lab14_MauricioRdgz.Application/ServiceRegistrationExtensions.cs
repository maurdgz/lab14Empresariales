using Microsoft.Extensions.DependencyInjection;
namespace Lab14_MauricioRdgz.Application;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ServiceRegistrationExtensions).Assembly));
}
