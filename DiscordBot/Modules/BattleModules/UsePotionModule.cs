using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities;
using DiscordBot.BattleSystem.Entities.Potions;
using DiscordBot.BattleSystem.Handlers;
using DiscordBot.Core.UserAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class UsePotionModule : ModuleBase<SocketCommandContext>
    {
        [Command("use")]
        public async Task UsePotion(string potionName)
        {           
            var account = UserManager.GetAccount(Context.User);

            switch (potionName)
            {
                case "divine":
                    await PotionHandler.DivinePotion(account, Context);
                    break;
                case "bighealing":
                    await PotionHandler.BigHealingPotion(account, Context);
                    break;
                case "midhealing":
                    await PotionHandler.MidHealingPotion(account, Context);
                    break;
                case "lilhealing":
                    await PotionHandler.LilHealingPotion(account, Context);
                    break;
                default:
                    break;
            }
        }
    }
}
