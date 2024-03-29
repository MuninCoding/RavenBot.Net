﻿
using DiscordBot.BattleSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.UserAccounts
{
    public class UserAccount
    {
        public ulong ID { get; set; }
        public string Name { get; set; }
        public int Battletag { get; set; }

        public uint XP { get; set; }
        public uint LevelNumber
        {
            get
            {
                return (uint)Math.Sqrt(XP / 50);
            }
        }

        public bool IsMuted { get; set; }

        public uint NumberOfWarnings { get; set; }
        public uint MessageCount { get; set; }

        public PlayerStatistics BattleStatistics { get; set; }

    }
}