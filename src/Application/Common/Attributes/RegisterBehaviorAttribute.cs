namespace Application.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterBehaviorAttribute<TRequest, TResponse> : RegisterBehaviorAttribute
{
    public override Type Request { get => typeof(TRequest); }
    public override Type Response { get => typeof(TResponse); }
}

[AttributeUsage(AttributeTargets.Class)]
public class RegisterOpenBehaviorAttribute : RegisterBehaviorAttribute
{
    public override Type Request { get; }

    public RegisterOpenBehaviorAttribute(Type request)
    {
        Request = request;
    }
}

public class RegisterBehaviorAttribute : Attribute
{
    public virtual Type Request { get; } = default;
    public virtual Type Response { get; } = default;
}
