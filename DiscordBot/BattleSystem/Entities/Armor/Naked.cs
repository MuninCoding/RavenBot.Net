using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Armor
{
    public class Naked : IArmor
    {
        public int BonusHealth { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }
        public Naked()
        {
            BonusHealth = 0;
            Name = "Naked"; 
        }
    }
}
