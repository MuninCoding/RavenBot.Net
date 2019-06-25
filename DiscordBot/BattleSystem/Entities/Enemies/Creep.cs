using DiscordBot.BattleSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Enemies
{
    public class Creep : IEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }

        public Creep()
        {
            var generator = new Random();

            int randomNumber = generator.Next(18, 24);
            Health = randomNumber;

            randomNumber = generator.Next(18, 24);
            Damage = randomNumber;

            Defense = 0;

        }
    }
}
