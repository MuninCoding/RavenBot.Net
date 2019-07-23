using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Potions
{
    public class BigHealingPotion : IPotion
    {
        public int Heal { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }

        public BigHealingPotion()
        {
            PurchasePrice = 200;
            Name = "Big Healing Potion";
        }
    }
}
