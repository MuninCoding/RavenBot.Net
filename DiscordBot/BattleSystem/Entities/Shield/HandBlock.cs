using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    [JsonObject]
    public class HandBlock : BaseShield
    {
        public override int DamageBlock { get; set; }

        public HandBlock()
        {
            DamageBlock = 2;
        }
    }
}
