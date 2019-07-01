using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.BossEnemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Utilities
{
    public class BossUtilities
    {
        internal static List<IEnemy> SpawnBoss(uint level, int damage)
        {
            List<IEnemy> boss = new List<IEnemy>();

            if (level == 5)
            {
                boss.Add(new BossLevel5());
            }
            return boss;
        }
    }
}
