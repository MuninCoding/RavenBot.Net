using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using DiscordBot.Core.UserAccounts;

namespace DiscordBot.Core
{
    public static class DataStorage
    {
        public static void SaveUserManager(IEnumerable<UserAccount> accounts, string filePath)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented, settings);
            File.WriteAllText(filePath, json);
        }

        public static IEnumerable<UserAccount> LoadUserManager(string filePath)
        {
            if (!File.Exists(filePath)) return null;
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<UserAccount>>(json, settings);
        }

        public static bool SaveExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
