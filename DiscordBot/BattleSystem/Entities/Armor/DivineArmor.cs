using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Armor
{
    class DivineArmor : IArmor
    {
        public int BonusHealth { get; set; }
        public string Name { get; set; }
        public DivineArmor()
        {
            BonusHealth = 1000;
            Name = "DivineArmor";
        }

    }
}
