using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Schema;
using WisielecDiscordBot.Infrastructure.Attributes;

namespace WisielecDiscordBot.Infrastructure.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAttributedInjectables(this IServiceCollection services)
        {
            var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes());

            var injectableTypes = allTypes
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    Attribute = t.GetCustomAttribute<InjectableAttribute>()
                })
                .Where(x => x.Attribute != null);

            foreach (var item in injectableTypes)
            {
                var interfaces = item.Type.GetInterfaces();

                foreach (var iface in interfaces)
                {
                    switch (item.Attribute.Lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(iface, item.Type);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(iface, item.Type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(iface, item.Type);
                            break;
                    }
                }
            }

            return services;
        }
    }
}
