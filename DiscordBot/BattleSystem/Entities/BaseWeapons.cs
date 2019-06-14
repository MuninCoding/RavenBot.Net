using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities
{
    public abstract class BaseWeapons
    {
        public abstract int Damage{ get; set; }
        public abstract string Name { get; set; }
    }
}
