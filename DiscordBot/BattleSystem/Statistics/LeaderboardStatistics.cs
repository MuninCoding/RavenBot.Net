using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Statistics
{
    [JsonObject]
    public class LeaderboardStatistics
    {
        public uint LeaderboardPositionLevel { get; set; }
        public uint LeaderboardPositionXp { get; set; }
        public uint LeaderboardPositionBattlepoints { get; set; }
        public uint LeaderboardPositionCreepKills { get; set; }
        public uint LeaderboardPositionBossKills { get; set; }
        public uint LeaderboardPositionPvpKills { get; set; }
        public uint LeaderboardPositionCreepDrops { get; set; }
        public uint LeaderboardPositionBossDrops { get; set; }
        public uint LeaderboardPositionPvpDrops { get; set; }
    }
}
