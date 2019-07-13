using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class DivineRapier : IWeapon
    {
        private float damage;
        public float Damage
        {
            get
            {
                var generator = new Random();
                double random = generator.NextDouble();
                if (random <= 0.15)
                {
                    return damage * 2.25f;
                }
                else
                {
                    return damage;
                }
            }
            set { damage = 322; }
        }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }
        public DivineRapier()
        {
            Damage = 322;
            Name = "DivineRapier";
            PurchasePrice = 1000000;
        }
    }
}
