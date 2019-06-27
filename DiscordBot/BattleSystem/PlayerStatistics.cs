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
        public uint Xp { get; set; }
        public uint Level
        {
            get
            {
                return (uint)Math.Sqrt(Xp/ 50);
            }
        }


        public double BattlePoints { get; set; }
        public uint CreepBattlesFought { get; set; }
        public uint CreepBattlesWon { get; set; }
        public uint CreepBattlesLost { get; set; }
        public uint PvPBattlesFought { get; set; }
        public uint PvPBattlesWon { get; set; }
        public uint PvPBattlesLost { get; set; }
        public uint WinStreak { get; set; }
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

        public IWeapon Weapon { get; set; }
        public IShield Shield { get; set; }
        public IArmor Armor { get; set; }
        public List<IWeapon> Weapons { get; set; }
        public List<IShield> Shields { get; set; }
        public List<IArmor> Armors { get; set; }
    }
}
