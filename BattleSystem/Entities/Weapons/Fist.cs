using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Weapons
{
    public class Fist : IWeapon
    {
        public int Damage { get; set; }
        public string Name { get; set; }
        public Fist()
        {
            Damage = 5;
            Name = "fist";
        }
    }
}
