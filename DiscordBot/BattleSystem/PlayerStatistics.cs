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


        public uint SkillPoints { get; set; }
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
