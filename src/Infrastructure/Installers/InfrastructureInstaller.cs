using Domain.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Installers;

public static class InfrastructureInstaller
{
    public static void InstallInfrastructure(this IServiceCollection services)
    {
        services.InstallRegisterAttribute(System.Reflection.Assembly.GetExecutingAssembly());
    }
}
