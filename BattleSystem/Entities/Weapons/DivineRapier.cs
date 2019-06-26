using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class DivineRapier : IWeapon
    {
        public int Damage { get; set; }
        public string Name { get; set; }
        public DivineRapier()
        {
            Damage = 1000;
            Name = "DivineRapier";
        }
    }
}
