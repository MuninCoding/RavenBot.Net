using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.BattleSystem.Enemies;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.BossEnemies;

namespace DiscordBot.BattleSystem.Handlers
{
    class SpawnHandler
    {
        internal static List<IEnemy> SpawnEnemies(uint level, float damage, bool isBossWave)
        {
            List<IEnemy> enemies = new List<IEnemy>();

            if (!isBossWave)
            {
                if (level <= 2 || damage <= 10)
                {
                    enemies.Add(new Creep());
                }
                else if (level <= 5 || damage <= 15)
                {
                    enemies.Add(new Creep());
                    enemies.Add(new Creep2());
                }

            }
            else if(isBossWave && level == 5)
            {
                enemies.Add(new BossLevel5());
            }

            return enemies;
        }
    }
}
