using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Potions
{
    public class DivinePotion : IPotion
    {
        public int Heal { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }

        public DivinePotion()
        {
            PurchasePrice = 500;
            Name = "Divine Potion";
        }

    }
}
