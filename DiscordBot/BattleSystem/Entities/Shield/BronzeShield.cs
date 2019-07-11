using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    public class BronzeShield : IShield
    {
        public int DamageReflection { get; set; }
        public int DamageBlock { get; set; }
        public string Name { get; set; }

        public BronzeShield()
        {
            int random = new Random().Next(0, 8);
            DamageReflection = random;
            DamageBlock = 8;
            Name = "BronzeShield";

        }
    }
}
