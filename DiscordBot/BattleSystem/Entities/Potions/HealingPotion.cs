using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Potions
{
    public class HealingPotion : IPotion
    {
        public int Heal { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }

        public HealingPotion()
        {
            Heal = 50;
            PurchasePrice = 200;
            Name = "Healing Potion";
        }
    }
}
