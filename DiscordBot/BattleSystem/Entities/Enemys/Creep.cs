using DiscordBot.BattleSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Enemys
{
    public class Creep : BaseEnemy
    {
        public override int Health { get; set; }
        public override int Damage { get; set; }
        public override int Defense { get; set; }

        public Creep()
        {
            var generator = new Random();

            int randomNumber = generator.Next(15, 24);
            Health = randomNumber;

            randomNumber = generator.Next(20, 26);
            Damage = randomNumber;

            Defense = 0;

        }
    }
}
