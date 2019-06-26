using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities
{
    public interface IEnemy
    {
        int Health { get; set; }
        int Damage { get; set; }
        int Defense { get; set; }
    }
}
