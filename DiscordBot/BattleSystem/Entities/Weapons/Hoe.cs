using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class Hoe : IWeapon
    {
        public float Damage { get; set; }
        public string Name { get; set; }
        public Hoe()
        {
            Damage = 20;
            Name = "Hoe";
        }
    }
}
