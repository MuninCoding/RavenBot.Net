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
    public class BattleStats
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
        public int BaseAttack { get; set; }
        public int BaseDefense { get; set; }
        public int Health
        {
            get
            {
                return BaseHealth + Armor.BonusHealth;
            }
        }
        public int Attack
        {
            get
            {
                return BaseAttack + Weapon.Damage;
            }
        }
        public int Defense
        {
            get
            {
                return BaseDefense + Shield.DamageBlock;
            }
        }

        public BaseWeapons Weapon { get; set; }
        public BaseShield Shield { get; set; }
        public BaseArmor Armor { get; set; }
        
    }
}
