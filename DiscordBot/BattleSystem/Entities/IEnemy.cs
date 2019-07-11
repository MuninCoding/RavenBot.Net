using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities
{
    public interface IEnemy
    {
        float Health { get; set; }
        float Damage { get; set; }
        float Defense { get; set; }
        float GettingXp { get; set; }
        float GettingGold { get; set; }
        string Name { get; set; }
    }
}
