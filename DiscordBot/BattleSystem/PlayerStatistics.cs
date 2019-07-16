using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Statistics;
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
                return 1 + (uint)Math.Sqrt(Xp / 50);
            }
        }

        
        public float BaseHealth { get; set; }
        public float CurrentHealth { get; set; }
        public float BaseDamage { get; set; }
        public float BaseDefense { get; set; }
        public float Health
        {
            get
            {
                return BaseHealth + Armor.BonusHealth;
            }
        }
        public float Damage
        {
            get
            {
                return BaseDamage + Weapon.Damage;
            }
        }
        public float Defense
        {
            get
            {
                return BaseDefense + Shield.DamageBlock;
            }
        }
        public float Gold { get; set; }
        public double BattlePoints { get; set; }
        public double Xp { get; set; }

        public CreepStatistics CreepStatistics { get; set; }
        public BossStatistics BossStatistics { get; set; }
        public PvpStatistics PvpStatistics { get; set; }
        public LeaderboardStatistics LeaderboardStatistics { get; set; }
        public DropStatistics DropStatistics { get; set; }

        public string EquipedWeapon => this.Weapon.Name;
        public string EquipedShield => this.Shield.Name;
        public string EquipedArmor => this.Armor.Name;

        public IWeapon Weapon { get; set; }
        public IShield Shield { get; set; }
        public IArmor Armor { get; set; }
        public IPotion Potion { get; set; }

        public List<IWeapon> Weapons { get; set; }
        public List<IShield> Shields { get; set; }
        public List<IArmor> Armors { get; set; }
        public List<IPotion> Potions { get; set; }
    }
}
