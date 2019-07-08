using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Statistics
{
    [JsonObject]
    public class DropStatistics
    {
        public uint CreepDrops { get; set; }
        public uint BossDrops { get; set; }
        public uint PvpDrops { get; set; }
    }
}
