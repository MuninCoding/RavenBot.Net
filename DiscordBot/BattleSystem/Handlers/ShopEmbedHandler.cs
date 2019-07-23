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
                     .AddField("Potions", "?shop potions - Shows the Potion shop embed")
                     .AddField("Weapons", "?shop weapons - Shows the Weapon shop embed")
                     .AddField("Armors", "?shop armors - Shows the Armor shop embed")
                     .AddField("Shields", "?shop shields- Shows the Shield shop embed")
                     .AddField("Points", "?shop points - Shows the Battlepoints shop embed")
                     .WithFooter(footer => footer.Text = "©DivineGuardianBot")
                     .WithCurrentTimestamp();

            var shopsInfoEmbed = shopsEmbed.Build();
            return shopsInfoEmbed;
        }
        //todo
        internal static Embed PotionsEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed WeaponsEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed ArmorsEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed ShieldsEmbed()
        {
            throw new NotImplementedException();
        }

        internal static Embed BattlePointsEmbed()
        {
            throw new NotImplementedException();
        }

    }
}
