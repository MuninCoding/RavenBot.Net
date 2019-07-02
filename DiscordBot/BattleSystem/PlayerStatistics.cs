using DiscordBot.BattleSystem.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem
{
    [JsonObject]
    public class PlayerStatistics
    {
        public uint Level
        {
            get
            {
                return (uint)Math.Sqrt(Xp/ 50);
            }
        }

        public int BaseHealth { get; set; }
        public int BaseDamage { get; set; }
        public int BaseDefense { get; set; }
        public int Health
        {
            get
            {
                return BaseHealth + Armor.BonusHealth;
            }
        }
        public int Damage
        {
            get
            {
                return BaseDamage + Weapon.Damage;
            }
        }
        public int Defense
        {
            get
            {
                return BaseDefense + Shield.DamageBlock;
            }
        }

        public double BattlePoints { get; set; }
        public double Xp { get; set; }



        public uint CreepRankPlacement { get; set; }
        public uint CreepBattlesFought { get; set; }
        public uint CreepBattlesWon { get; set; }
        public uint CreepBattlesLost { get; set; }
        public uint CurrentCreepKillStreak { get; set; }
        public uint HighestCreepKillStreak { get; set; }
        public uint CurrentCreepWinStreak { get; set; }
        public uint HighestCreepWinStreak { get; set; }
        public uint AmountOfCreepsKilled { get; set; }

        public uint PvPChallengesRequests { get; set; }
        public uint PvPBattlesDeclined { get; set; }
        public uint PvPBattlesAccepted { get; set; }
        public uint PvPBattlesFought { get; set; }
        public uint PvPBattlesWon { get; set; }
        public uint PvPBattlesLost { get; set; }
        public uint PvPRankPlacement { get; set; }
        public uint CurrentPvpKillStreak { get; set; }
        public uint HighestPvpKillStreak { get; set; }
        public uint CurrentPvpWinStreak { get; set; }
        public uint HighestPvpWinStreak { get; set; }
        public uint AmountOfPlayersKilled { get; set; }

        public uint BossBattlesFought { get; set; }
        public uint BossBattlesWon { get; set; }
        public uint BossBattlesLost { get; set; }
        public uint CurrentBossWinStreak { get; set; }
        public uint HighestBossWinStreak { get; set; }
        public uint CurrentBossKillStreak { get; set; }
        public uint HighestBossKillStreak { get; set; }
        public uint AmountOfBossesKilled { get; set; }
        public uint BossRankPlacement { get; set; }

        public uint CreepDrops { get; set; }
        public uint BossDrops { get; set; }
        public uint PvpDrops { get; set; }

        public uint LeaderboardPositionLevel { get; set; }
        public uint LeaderboardPositionXp { get; set; }
        public uint LeaderboardPositionBattlepoints { get; set; }
        public uint LeaderboardPositionCreepKills { get; set; }
        public uint LeaderboardPositionBossKills { get; set; }
        public uint LeaderboardPositionPvpKills { get; set; }
        public uint LeaderboardPositionCreepDrops { get; set; }
        public uint LeaderboardPositionBossDrops { get; set; }
        public uint LeaderboardPositionPvpDrops { get; set; }



        public IWeapon Weapon { get; set; }
        public IShield Shield { get; set; }
        public IArmor Armor { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public List<IShield> Shields { get; set; }
        public List<IArmor> Armors { get; set; }
    }
}
