using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.BattleSystem.Handlers
{
    public class ShopEmbedHandler
    {
        internal static Embed ShopEmbed()
        {
            var shopsEmbed = new EmbedBuilder();

           shopsEmbed.WithColor(Color.DarkRed)
                     .WithTitle("Shops")
                     .WithDescription("Below is a list with all currently available Shop´s")
                     .AddField("Potions", "?help - Shows bot information with all available commands")
                     .AddField("Weapons", "?sharerank {user} - Shares your rank with the specified user")
                     .AddField("Armor", "?clear - Clear the chat in the Channel there you write the Message")
                     .AddField("Shields", "?mute<@playername><MuteState> - Mute player for Voicechannel (MuteState = True|False)")
                     .AddField("Points", "?kick<@playername><Reason> - Kick a player from Server with a specified reason")
                     .WithFooter(footer => footer.Text = "©RavenplaysGuardianBot")
                     .WithCurrentTimestamp();

            var shopsInfoEmbed = shopsEmbed.Build();
            return shopsInfoEmbed;
        }

        internal static Embed PotionsEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed WeaponEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed ArmorEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed ShieldEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed BattlePointsEmbed()
        {
            throw new NotImplementedException();
        }

    }
}
