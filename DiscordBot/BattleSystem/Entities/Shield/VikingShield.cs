using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    public class VikingShield : IShield
    {
        public int DamageBlock { get; set; }
        public string Name { get; set; }

        public VikingShield()
        {
            DamageBlock = 8;
            Name = "VikingShield";
        }
    }
}
