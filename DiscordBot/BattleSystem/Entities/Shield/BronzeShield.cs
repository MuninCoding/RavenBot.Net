﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Entities.Shield
{
    public class BronzeShield : IShield
    {
        public int DamageBlock { get; set; }
        public string Name { get; set; }

        public BronzeShield()
        {
            DamageBlock = 8;
            Name = "BronzeShield";
        }
    }
}