using Application.Common.Attributes;
using Domain.Common.Installers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Installers;

public static class ApplicationInstaller
{
    public static void InstallApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.InstallRegisterAttribute(assembly);
        services.InstallMediatr(assembly);
        services.InstallRegisterRequestHandlerAttribute(assembly);
    }

    private static void InstallMediatr(this IServiceCollection services, Assembly assembly)
    {
        var behaviors = assembly.GetTypes()
                   .SelectMany(t => t.GetCustomAttributes(typeof(RegisterBehaviorAttribute<,>), true))
                   .OfType<RegisterBehaviorAttribute>()
                   .ToList();

        var openBehaviors = assembly.GetTypes()
            .Select(t => t.GetCustomAttribute(typeof(RegisterOpenBehaviorAttribute), true))
            .OfType<RegisterBehaviorAttribute>()
            .ToList();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            foreach (var item in behaviors)
            {
                cfg.AddBehavior(item.Request, item.Response);
            }
            foreach (var item in openBehaviors)
            {
                cfg.AddOpenBehavior(item.Request);
            }
        });
    }

    private static void InstallRegisterRequestHandlerAttribute(this IServiceCollection services, Assembly assembly)
    {
        var classes = assembly.GetTypes().Where(t => t.GetCustomAttributes(typeof(RegisterRequestHandlerAttribute), true).FirstOrDefault() != null).ToList();

        foreach (var @class in classes)
        {
            var attribute = @class.GetCustomAttribute<RegisterRequestHandlerAttribute>();

            Type @interface = @class.GetInterfaces().Single();

            services.RegisterService(@interface, @class, ServiceLifetime.Scoped);
        }
    }
}
