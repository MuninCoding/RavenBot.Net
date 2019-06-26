using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.BattleSystem.Entities.Armor;
using DiscordBot.BattleSystem.Entities.Shield;
using DiscordBot.BattleSystem.Entities.Weapons;
using DiscordBot.BattleSystem.Utilities;
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
            if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ItemUtilities.EquipItem(itemSlot, typeof(Fist), Context);
                        break;
                    case "rock":
                        await ItemUtilities.EquipItem(itemSlot, typeof(Rock), Context);
                        break;
                    case "bat":
                        await ItemUtilities.EquipItem(itemSlot, typeof(Bat), Context);
                        break;
                    case "divinerapier":
                        await ItemUtilities.EquipItem(itemSlot, typeof(DivineRapier), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Weapon not found");
                        break;
                }
            }
            else if (itemSlot.Equals("shield"))
            {
                switch (itemName)
                {
                    case "handblock":
                        await ItemUtilities.EquipItem(itemSlot, typeof(HandBlock), Context);
                        break;
                    case "woodenshield":
                        await ItemUtilities.EquipItem(itemSlot, typeof(WoodenShield), Context);
                        break;
                    case "bronzeshield":
                        await ItemUtilities.EquipItem(itemSlot, typeof(BronzeShield), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Shield not found");
                        break;
                }

            }
            else if (itemSlot.Equals("armor"))
            {
                switch (itemName)
                {
                    case "naked":
                        await ItemUtilities.EquipItem(itemSlot, typeof(Naked), Context);
                        break;
                    case "leatherarmor":
                        await ItemUtilities.EquipItem(itemSlot, typeof(LeatherArmor), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
                        break;
                }

            }
            else
            {
                await ReplyAsync("Type not Found");
            }
        }

        [Command("add")]
        public async Task AddItem(string itemSlot, string itemName)
        {
            if (itemSlot.Equals("weapon"))
            {
                switch (itemName)
                {
                    case "fist":
                        await ItemUtilities.AddItem(itemSlot, typeof(Fist), Context);
                        break;
                    case "rock":
                        await ItemUtilities.AddItem(itemSlot, typeof(Rock), Context);
                        break;
                    case "bat":
                        await ItemUtilities.AddItem(itemSlot, typeof(Bat), Context);
                        break;
                    case "divinerapier":
                        await ItemUtilities.AddItem(itemSlot, typeof(DivineRapier), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Weapon not found");
                        break;
                }
            }
            else if (itemSlot.Equals("shield"))
            {
                switch (itemName)
                {
                    case "handblock":
                        await ItemUtilities.AddItem(itemSlot, typeof(HandBlock), Context);
                        break;
                    case "woodenshield":
                        await ItemUtilities.AddItem(itemSlot, typeof(WoodenShield), Context);
                        break;
                    case "bronzeshield":
                        await ItemUtilities.AddItem(itemSlot, typeof(BronzeShield), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Shield not found");
                        break;
                }

            }
            else if (itemSlot.Equals("armor"))
            {
                switch (itemName)
                {
                    case "naked":
                        await ItemUtilities.AddItem(itemSlot, typeof(Naked), Context);
                        break;
                    case "leatherarmor":
                        await ItemUtilities.AddItem(itemSlot, typeof(LeatherArmor), Context);
                        break;
                    default:
                        await Context.Channel.SendMessageAsync("Armor not found");
                        break;
                }

            }
            else
            {
                await ReplyAsync("Type not Found");
            }

        }
    }
}
