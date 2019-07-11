using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    public class HandBlock : IShield
    {
        public int DamageReflection { get; set; }
        public int DamageBlock { get; set; }
        public string Name { get; set; }

        public HandBlock()
        {
            DamageBlock = 2;
            Name = "HandBlock";
        }
    }
}
