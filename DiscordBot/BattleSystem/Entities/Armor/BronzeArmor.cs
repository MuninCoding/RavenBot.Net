using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Armor
{
    public class BronzeArmor
    {
        public int BonusHealth { get; set; }
        public string Name { get; set; }
        public BronzeArmor()
        {
            BonusHealth = 15;
            Name = "BronzeArmor";
        }
    }
}
