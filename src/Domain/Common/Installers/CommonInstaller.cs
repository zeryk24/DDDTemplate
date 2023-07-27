using Domain.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Domain.Common.Installers;

public static class CommonInstaller
{
    public static void InstallRegisterAttribute(this IServiceCollection services, Assembly assembly)
    {
        var classes = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(RegisterAttribute), true).FirstOrDefault() != null).ToList();

        foreach (var @class in classes)
        {
            var attribute = @class.GetCustomAttribute<RegisterAttribute>();

            Type? @interface = attribute!.Interface;
            if (@interface == null)
                @interface = @class;

            services.RegisterService(@interface, @class, attribute.Lifetime);
        }
    }

    public static void RegisterService(this IServiceCollection services, Type @interface, Type type, ServiceLifetime lifeTime)
    {
        switch (lifeTime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton(@interface, type);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped(@interface, type);
                break;
            case ServiceLifetime.Transient:
                services.AddTransient(@interface, type);
                break;
        }
    }
}