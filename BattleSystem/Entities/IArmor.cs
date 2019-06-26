using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities
{
    public interface IArmor
    {
        int BonusHealth{ get; set; }
        string Name { get; set; }
    }
}
