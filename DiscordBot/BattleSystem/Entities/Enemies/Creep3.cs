using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Enemies
{
    public class Creep3 : IEnemy
    {
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Defense { get; set; }
        public float GettingXp { get; set; }
        public float GettingGold { get; set; }
        public string Name { get; set; }

        public Creep3()
        {
            var generator = new Random();

            int randomNumber = generator.Next(31, 51);
            Health = randomNumber;

            randomNumber = generator.Next(34, 54);
            Damage = randomNumber;
            Defense = 12;
            Name = "Big Creep";
            GettingXp = 35;
            GettingGold = 50;
        }
    }
}
