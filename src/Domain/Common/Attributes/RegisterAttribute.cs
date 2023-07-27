using Microsoft.Extensions.DependencyInjection;

namespace Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class RegisterAttribute<TType> : RegisterAttribute
{
    public RegisterAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : base(typeof(TType), lifetime) {}
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterAttribute : Attribute
{
    public Type? Interface { get; set; }
    public ServiceLifetime Lifetime { get; }

    public RegisterAttribute(Type? @interface = null, ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Interface = @interface;
        Lifetime = lifetime;
    }
}
