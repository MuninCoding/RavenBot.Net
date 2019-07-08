using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Statistics
{
    [JsonObject]
    public class BossStatistics
    {
        public uint BossBattlesFought { get; set; }
        public uint BossBattlesWon { get; set; }
        public uint BossBattlesLost { get; set; }
        public uint CurrentBossWinStreak { get; set; }
        public uint HighestBossWinStreak { get; set; }
        public uint CurrentBossKillStreak { get; set; }
        public uint HighestBossKillStreak { get; set; }
        public uint AmountOfBossesKilled { get; set; }
        public bool BossBattleFoughtOrDeclined { get; set; }

    }
}
