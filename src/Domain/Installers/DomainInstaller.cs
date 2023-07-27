using Domain.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Installers;

public static class DomainInstaller
{
    public static void InstallDomain(this IServiceCollection services)
    {
        services.InstallRegisterAttribute(System.Reflection.Assembly.GetExecutingAssembly());
    }
}
