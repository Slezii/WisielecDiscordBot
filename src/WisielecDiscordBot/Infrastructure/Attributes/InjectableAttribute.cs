using Microsoft.Extensions.DependencyInjection;

namespace WisielecDiscordBot.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectableAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; }

        public InjectableAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
    }
}
