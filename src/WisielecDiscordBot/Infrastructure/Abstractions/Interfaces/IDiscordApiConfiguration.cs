using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WisielecDiscordBot.Infrastructure.Abstractions.Interfaces
{

    public interface IDiscordApiConfiguration
    {
        string Token { get; set; }
    }
}
