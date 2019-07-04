using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities.Armor;
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
    public class ItemModule : ModuleBase<SocketCommandContext>
    {
        [Command("equip")]
        public async Task EquipItem(string itemSlot, string itemName)
        {
            await Context.Message.DeleteAsync();
            if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ItemHandler.EquipItem(itemSlot, typeof(Fist), Context);
                        break;
                    case "rock":
                        await ItemHandler.EquipItem(itemSlot, typeof(Rock), Context);
                        break;
                    case "bat":
                        await ItemHandler.EquipItem(itemSlot, typeof(Bat), Context);
                        break;
                    case "divinerapier":
                        await ItemHandler.EquipItem(itemSlot, typeof(DivineRapier), Context);
                        break;
                    default:
                        var botMessage = await Context.Channel.SendMessageAsync("Weapon not found");
                        await Task.Delay(10000);
                        await botMessage.DeleteAsync();
                        break;
                }
            }
            else if (itemSlot.Equals("shield"))
            {
                switch (itemName)
                {
                    case "handblock":
                        await ItemHandler.EquipItem(itemSlot, typeof(HandBlock), Context);
                        break;
                    case "woodenshield":
                        await ItemHandler.EquipItem(itemSlot, typeof(WoodenShield), Context);
                        break;
                    case "bronzeshield":
                        await ItemHandler.EquipItem(itemSlot, typeof(BronzeShield), Context);
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
                        await ItemHandler.EquipItem(itemSlot, typeof(Naked), Context);
                        break;
                    case "leatherarmor":
                        await ItemHandler.EquipItem(itemSlot, typeof(LeatherArmor), Context);
                        break;
                    case "divinearmor":
                        await ItemHandler.EquipItem(itemSlot, typeof(DivineArmor), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
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

        //TODO Split Class

        [Command("add")]
        public async Task AddItem(string itemSlot, string itemName)
        {
            //TODO set option to add item to other!!
            
            await Context.Message.DeleteAsync();
            if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ItemHandler.AddItem(itemSlot, typeof(Fist), Context);
                        break;
                    case "rock":
                        await ItemHandler.AddItem(itemSlot, typeof(Rock), Context);
                        break;
                    case "bat":
                        await ItemHandler.AddItem(itemSlot, typeof(Bat), Context);
                        break;
                    case "divinerapier":
                        await ItemHandler.AddItem(itemSlot, typeof(DivineRapier), Context);
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
                        await ItemHandler.AddItem(itemSlot, typeof(HandBlock), Context);
                        break;
                    case "woodenshield":
                        await ItemHandler.AddItem(itemSlot, typeof(WoodenShield), Context);
                        break;
                    case "bronzeshield":
                        await ItemHandler.AddItem(itemSlot, typeof(BronzeShield), Context);
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
                        await ItemHandler.AddItem(itemSlot, typeof(Naked), Context);
                        break;
                    case "leatherarmor":
                        await ItemHandler.AddItem(itemSlot, typeof(LeatherArmor), Context);
                        break;
                    case "divinearmor":
                        await ItemHandler.AddItem(itemSlot, typeof(DivineArmor), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
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
