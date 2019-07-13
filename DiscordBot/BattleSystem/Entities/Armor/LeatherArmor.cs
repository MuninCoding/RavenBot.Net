using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Armor
{
    public class LeatherArmor : IArmor
    {
        public int BonusHealth { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }
        public LeatherArmor()
        {
            BonusHealth = 5;
            Name = "LeatherArmor";
        }
    }
}
