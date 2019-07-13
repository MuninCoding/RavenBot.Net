using Discord;
using Discord.Commands;
using DiscordBot.BattleSystem.Entities.Armor;
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.BattleSystem.Handlers;
using DiscordBot.Core.UserAccounts;
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
        public async Task Shop(string type = null)
        {
            if (type != null)
            {
                switch (type)
                {
                    case "potions":
                        await ReplyAsync(embed: ShopEmbedHandler.PotionsEmbed());
                        break;
                    case "weapons":
                        await ReplyAsync(embed: ShopEmbedHandler.WeaponsEmbed());
                        break;
                    case "armors":
                        await ReplyAsync(embed: ShopEmbedHandler.ArmorsEmbed());
                        break;
                    case "shields":
                        await ReplyAsync(embed: ShopEmbedHandler.ShieldsEmbed());
                        break;
                    case "points":
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
        [Command("buy")]
        public async Task Buy(string itemSlot, string itemName)
        {
            var account = UserManager.GetAccount(Context.Message.Author);

            if (itemSlot.Equals("potions"))
            {
                await Context.Channel.SendMessageAsync("Not Implemented yet");
            }
            else if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(Fist), Context, account);
                        break;
                    case "rock":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(Rock), Context, account);
                        break;
                    case "bat":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(Bat), Context, account);
                        break;
                    case "hoe":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(Hoe), Context, account);
                        break;
                    case "divinerapier":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(DivineRapier), Context, account);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Weapon not found");
                        await Task.Delay(10000);
                        await Context.Message.DeleteAsync();
                        break;
                }
            }
            else if (itemSlot.Equals("armor"))
            {
                switch (itemName)
                {
                    case "naked":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(Naked), Context, account);
                        break;
                    case "leatherarmor":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(LeatherArmor), Context, account);
                        break;
                    case "woodenarmor":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(WoodenArmor), Context, account);
                        break;
                    case "bronzearmor":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(BronzeArmor), Context, account);
                        break;
                    case "divinearmor":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(DivineArmor), Context, account);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
                        await Task.Delay(10000);
                        await Context.Message.DeleteAsync();
                        break;
                }

            }
            else if (itemSlot.Equals("shield"))
            {
                switch (itemName)
                {
                    case "handblock":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(HandBlock), Context, account);
                        break;
                    case "woodenshield":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(WoodenShield), Context, account);
                        break;
                    case "bronzeshield":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(BronzeShield), Context, account);
                        break;
                    case "silbershield":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(SilberShield), Context, account);
                        break;
                    case "vikingshield":
                        await ShopItemHandler.BuyItem(itemSlot, typeof(VikingShield), Context, account);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Shield not found");
                        await Task.Delay(10000);
                        await Context.Message.DeleteAsync();
                        break;
                }

            }
            else if (itemSlot.Equals("points"))
            {
                await Context.Channel.SendMessageAsync("Not Implemented yet");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Please enter a valid type.");
            }
        }
    }
}
