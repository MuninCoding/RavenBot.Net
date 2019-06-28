using DiscordBot.BattleSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Enemies
{
    public class Creep2 : IEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public string Name { get; set; }

        public Creep2()
        {
            var generator = new Random();

            int randomNumber = generator.Next(24, 31);
            Health = randomNumber;

            randomNumber = generator.Next(21, 34);
            Damage = randomNumber;
            Defense = 3;
            Name = "";

        }
    }
}
