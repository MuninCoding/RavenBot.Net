using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities
{
    public abstract class BaseEnemy
    {
        public abstract int Health { get; set; }
        public abstract int Damage { get; set; }
        public abstract int Defense { get; set; }
    }
}
