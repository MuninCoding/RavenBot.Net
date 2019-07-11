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
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public float GettingXp { get; set; }
        public float GettingGold { get; set; }
        public string Name { get; set; }

        public Creep2()
        {
            var generator = new Random();

            int randomNumber = generator.Next(24, 31);
            Health = randomNumber;

            randomNumber = generator.Next(21, 34);
            Damage = randomNumber;
            Defense = 7;
            Name = "Armored Creep";
            GettingXp = 20;
            GettingGold = 25;

        }
    }
}
