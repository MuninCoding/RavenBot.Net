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
    public class EquipItemModule : ModuleBase<SocketCommandContext>
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
                    case "hoe":
                        await ItemHandler.EquipItem(itemSlot, typeof(Hoe), Context);
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
                    case "silbershield":
                        await ItemHandler.EquipItem(itemSlot, typeof(SilberShield), Context);
                        break;
                    case "vikingshield":
                        await ItemHandler.EquipItem(itemSlot, typeof(VikingShield), Context);
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
                    case "woodenarmor":
                        await ItemHandler.EquipItem(itemSlot, typeof(WoodenArmor), Context);
                        break;
                    case "bronzearmor":
                        await ItemHandler.EquipItem(itemSlot, typeof(BronzeArmor), Context);
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
    }
}
