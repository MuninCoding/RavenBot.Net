using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Enemies
{
    public class Creep : IEnemy
    {
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public string Name { get; set; }
        public Creep()
        {
            var generator = new Random();

            int randomNumber = generator.Next(19, 24);
            Health = randomNumber;

            randomNumber = generator.Next(18, 24);
            Damage = randomNumber;
            Defense = 0;
            Name = "Creep baby"; 

        }
    }
}
