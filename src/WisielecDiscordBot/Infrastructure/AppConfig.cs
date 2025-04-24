using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisielecDiscordBot.Infrastructure.Abstractions.Interfaces;
using WisielecDiscordBot.Infrastructure.Attributes;
using WisielecDiscordBot.Infrastructure.Exceptions;

namespace WisielecDiscordBot.Infrastructure
{
    [Injectable(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class AppConfig : IAppConfig
    {
        private readonly IConfiguration _configuration;
        public AppConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T GetValue<T>()
        {
            var t = Activator.CreateInstance<T>();
            _configuration.GetSection(t.GetType().Name).Bind(t);
            return t;
        }
    }
}
