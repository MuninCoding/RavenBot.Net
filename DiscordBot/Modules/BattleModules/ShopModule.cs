using Discord;
using Discord.Commands;
using DiscordBot.BattleSystem.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class ShopModule : ModuleBase<SocketCommandContext>
    {
        [Command("shop")]
        public async Task Shop (string type = null)
        {
            if (type != null)
            {
                switch(type)
                {
                    case "potion":
                        await ReplyAsync(embed: ShopEmbedHandler.PotionsEmbed());
                        break;
                    case "weapon":
                        await ReplyAsync(embed: ShopEmbedHandler.WeaponEmbed());
                        break;
                    case "armor":
                        await ReplyAsync(embed: ShopEmbedHandler.ArmorEmbed());
                        break;
                    case "shield":
                        await ReplyAsync(embed: ShopEmbedHandler.ShieldEmbed());
                        break;
                    case "battlepoints":
                        await ReplyAsync(embed: ShopEmbedHandler.BattlePointsEmbed());
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //send shopsInfoEmbed
                await ReplyAsync(embed: ShopEmbedHandler.ShopEmbed());
            }
        }
    }
}
