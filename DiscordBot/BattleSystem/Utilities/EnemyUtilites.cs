using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.BattleSystem.Enemies;
using DiscordBot.BattleSystem.Entities;

namespace DiscordBot.BattleSystem.Utilities
{
    class EnemyUtilites
    {
        internal static List<IEnemy> SpawnEnemies(uint level, int damage)
        {
            List<IEnemy> enemies = new List<IEnemy>();

            if (level <= 2 || damage <= 10)
            {
                enemies.Add(new Creep());
            }
            else if (level <= 5 || damage <= 15)
            {
                enemies.Add(new Creep2());
            }
            

            

            return enemies;
        }
    }
}
