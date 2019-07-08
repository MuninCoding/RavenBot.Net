using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Statistics
{
    [JsonObject]
    public class PvpStatistics
    {
        public uint PvPChallengesRequests { get; set; }
        public uint PvPBattlesDeclined { get; set; }
        public uint PvPBattlesAccepted { get; set; }
        public uint PvPBattlesFought { get; set; }
        public uint PvPBattlesWon { get; set; }
        public uint PvPBattlesLost { get; set; }
        public uint CurrentPvpKillStreak { get; set; }
        public uint HighestPvpKillStreak { get; set; }
        public uint CurrentPvpWinStreak { get; set; }
        public uint HighestPvpWinStreak { get; set; }
        public uint AmountOfPlayersKilled { get; set; }
    }
}
