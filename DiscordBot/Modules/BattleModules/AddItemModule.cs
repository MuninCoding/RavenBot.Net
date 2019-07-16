using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities.Armor;
using DiscordBot.BattleSystem.Entities.Potions;
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.BattleSystem.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Modules.BattleModules
{
    public class AddItemModule : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task AddItem(string itemSlot, string itemName, SocketUser user = null)
        {
            await Context.Message.DeleteAsync();
            if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ItemHandler.AddItem(itemSlot, typeof(Fist), Context, user);
                        break;
                    case "rock":
                        await ItemHandler.AddItem(itemSlot, typeof(Rock), Context, user);
                        break;
                    case "bat":
                        await ItemHandler.AddItem(itemSlot, typeof(Bat), Context, user);
                        break;
                    case "hoe":
                        await ItemHandler.AddItem(itemSlot, typeof(Hoe), Context, user);
                        break;
                    case "divinerapier":
                        await ItemHandler.AddItem(itemSlot, typeof(DivineRapier), Context, user);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Weapon not found");
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
                        await ItemHandler.AddItem(itemSlot, typeof(HandBlock), Context, user);
                        break;
                    case "woodenshield":
                        await ItemHandler.AddItem(itemSlot, typeof(WoodenShield), Context, user);
                        break;
                    case "bronzeshield":
                        await ItemHandler.AddItem(itemSlot, typeof(BronzeShield), Context, user);
                        break;
                    case "silbershield":
                        await ItemHandler.AddItem(itemSlot, typeof(SilberShield), Context, user);
                        break;
                    case "vikingshield":
                        await ItemHandler.AddItem(itemSlot, typeof(VikingShield), Context, user);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Shield not found");
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
                        await ItemHandler.AddItem(itemSlot, typeof(Naked), Context, user);
                        break;
                    case "leatherarmor":
                        await ItemHandler.AddItem(itemSlot, typeof(LeatherArmor), Context, user);
                        break;
                    case "woodenarmor":
                        await ItemHandler.AddItem(itemSlot, typeof(WoodenArmor), Context, user);
                        break;
                    case "bronzearmor":
                        await ItemHandler.AddItem(itemSlot, typeof(BronzeArmor), Context, user);
                        break;
                    case "divinearmor":
                        await ItemHandler.AddItem(itemSlot, typeof(DivineArmor), Context, user);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
                        await Task.Delay(10000);
                        await Context.Message.DeleteAsync();
                        break;
                }

            }
            else if (itemSlot.Equals("potion"))
            {
                switch (itemName)
                {
                    case "healing":
                        await ItemHandler.AddItem(itemSlot, typeof(HealingPotion), Context, user);
                        break;
                    case "divine":
                        await ItemHandler.AddItem(itemSlot, typeof(DivinePotion), Context, user);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Potion not found");
                        await Task.Delay(10000);
                        await Context.Message.DeleteAsync();
                        break;
                }
            }
            else
            {
                var botMsg = await ReplyAsync("Type not Found");
                await Task.Delay(6000);
                await botMsg.DeleteAsync();
            }

        }
    }
}
