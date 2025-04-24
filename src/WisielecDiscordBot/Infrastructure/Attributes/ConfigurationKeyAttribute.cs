using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisielecDiscordBot.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
    internal class ConfigurationKeyAttribute : Attribute
    {
    }
}
