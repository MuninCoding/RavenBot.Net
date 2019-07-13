using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class Bat : IWeapon
    {
        public float Damage { get; set; }
        public string Name { get; set; }
        public float PurchasePrice { get; set; }
        public Bat()
        {
            Damage = 15;
            Name = "Bat";
            PurchasePrice = 300;
        }
    }
}
