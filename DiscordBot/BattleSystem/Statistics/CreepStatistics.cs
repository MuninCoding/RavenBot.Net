using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Statistics
{
    [JsonObject]
    public class CreepStatistics
    {
        public uint CreepBattlesFought { get; set; }
        public uint CreepBattlesWon { get; set; }
        public uint CreepBattlesLost { get; set; }
        public uint CurrentCreepKillStreak { get; set; }
        public uint HighestCreepKillStreak { get; set; }
        public uint CurrentCreepWinStreak { get; set; }
        public uint HighestCreepWinStreak { get; set; }
        public uint AmountOfCreepsKilled { get; set; }
    }
}
