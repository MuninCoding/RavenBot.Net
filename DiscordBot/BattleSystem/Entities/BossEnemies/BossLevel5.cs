using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.BossEnemies
{
    public class BossLevel5 : IEnemy
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public string Name { get; set; }

        public BossLevel5()
        {
            var random = new Random();
            int defense = random.Next(3, 10);
            Health = 125;         
            Damage = 25;
            Defense = defense;
            Name = "Ogre";
        }
    }
}
