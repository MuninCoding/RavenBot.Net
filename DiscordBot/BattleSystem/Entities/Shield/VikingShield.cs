﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    public class VikingShield : IShield
    {
        public int DamageReflection { get; set; }
        public int DamageBlock { get; set; }
        public float PurchasePrice { get; set; }
        public string Name { get; set; }

        public VikingShield()
        {
            DamageBlock = 15;
            Name = "VikingShield";
        }
    }
}
