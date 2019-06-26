using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiscordBot.Entities
{
    public class BotConfig
    {
        public string Token { get; set; }
        public char Prefix { get; set; }
        public string BotGame { get; set; }
    }
}
