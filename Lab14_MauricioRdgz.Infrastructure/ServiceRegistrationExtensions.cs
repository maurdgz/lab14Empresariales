using Microsoft.Extensions.DependencyInjection;
using Lab14_MauricioRdgz.Domain.Interfaces;
using Lab14_MauricioRdgz.Infrastructure.Repositories;
namespace Lab14_MauricioRdgz.Infrastructure;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services.AddSingleton<IProductoRepository, ProductoRepository>();
}
