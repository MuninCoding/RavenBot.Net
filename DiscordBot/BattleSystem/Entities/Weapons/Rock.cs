using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class Rock : IWeapon
    {
        public float Damage { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }
        public Rock()
        {
            Damage = 10;
            Name = "Rock";
            PurchasePrice = 200;
        }
    }
}
