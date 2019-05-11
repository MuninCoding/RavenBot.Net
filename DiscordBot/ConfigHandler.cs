using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using DiscordBot;
using DiscordBot.Entities;

namespace DiscordBot
{
    public static class ConfigHandler
    {
        private const string configFolder = "Resources";
        private const string configFile = "config.json";

        public static BotConfig config;

        static ConfigHandler()
        {
            Console.WriteLine("ConfigHandler instantiated");
            Console.WriteLine("Trying to retrieve info from Ravenplays Storage.");
            if (ConfigExists())
            {
                Console.WriteLine("Saved BrutelConfig found");
                GetConfig();
            }
            else
            {
                Console.WriteLine("No RavenConfig found.");
                SetConfig();
            }
        }

        public static bool ConfigExists()
        {
            return File.Exists(configFolder + "/" + configFile);
        }

        public static void GetConfig()
        {

            string json = File.ReadAllText(configFolder + "/" + configFile);
            try
            {
                var deserializedConfig = JsonConvert.DeserializeObject<BotConfig>(json);
                config = deserializedConfig;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The following exception occured while deserializing the config:\n" + e.Message);
                Console.WriteLine("Please check your config file or set it up again");
                SetConfig();
                throw;
            }
        }

        public static void SetConfig()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
            }

            BotConfig newConfig = new BotConfig();

            Console.WriteLine("Please Input your Bot Token");

            string token = Console.ReadLine();
            newConfig.Token = token;

            Console.WriteLine("Token initialized, please enter your desired command prefix.");
            Console.WriteLine("Using a special character is advised!");

            char prefix = Console.ReadKey().KeyChar;
            newConfig.Prefix = prefix;

            Console.WriteLine(" - inizialized as prefix, At last specify the game the bot is playing");

            string botGame = Console.ReadLine();
            newConfig.BotGame = botGame;

            config = newConfig;

            string json = JsonConvert.SerializeObject(newConfig, Formatting.Indented);
            File.WriteAllText(configFolder + "/" + configFile, json);
        }
    }
}