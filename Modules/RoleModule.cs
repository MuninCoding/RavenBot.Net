using Discord;
//NEEDED{
using Discord.Commands;
//}
using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    //Modules need to be public and derive from ModuleBase<SocketCommandContext> to get detected by the Discord.Net Library
    public class RoleModule : ModuleBase<SocketCommandContext>
    {
        //Specifiy the command to listen on here
        [Command("sharerole")]
        //Summary of what the command does - optional
        [Summary("Shares your current roles with the specified user")]
        //Require this permission of user
        [RequireUserPermission(GuildPermission.ManageNicknames)]
        //Require this permission of bot
        [RequireBotPermission(GuildPermission.ManageRoles)]

        //?sharerank {@maxmustermann#1234}
        public async Task GiveRanks(SocketGuildUser user)
        {
            //Getting the messagesender
            var rankGiver = Context.Message.Author as IGuildUser;
            //Getting all roles of the rankGiver
            var rolesOfGiver = rankGiver.RoleIds;
            //If there are more then 1 role (they have another role then the @everyone role)
            if (rolesOfGiver.Count > 1)
            {
                //Loop through all the roleIds in the roles of person who sent the command
                foreach (ulong roleId in rolesOfGiver)
                {
                    //If the role Id is anything else then the ID of the @everyone role
                    if (roleId != 355651293269983233)
                    {
                        //Get the role with the corresponding roleId
                        var roleToGive = Context.Guild.Roles.FirstOrDefault(x => x.Id == roleId);
                        //And give the role to the user specified in the passed argument of the command (user)
                        await (user as IGuildUser).AddRoleAsync(roleToGive);
                    }
                }
            }
            //If there is only the @everyone role assigned we send an error message
            else
            {
                await ReplyAsync("You do not have a role you could give away");
            }
        }
    }
}

