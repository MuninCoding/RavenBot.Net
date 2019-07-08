using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Armor
{
    public class WoodenArmor
    {
        public int BonusHealth { get; set; }
        public string Name { get; set; }
        public WoodenArmor()
        {
            BonusHealth = 10;
            Name = "WoodenArmor";
        }
    }
}
