using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PracticeBot.Classes
{
    static class BotStartup
    {
        public static void Startup(ref BotConfig bc)
        {
            BotConfig(ref bc);
        }

        private static void BotConfig(ref BotConfig bc)
        {
            try
            {
                JsonTextReader reader;
                // This is good for deployment where I've got the config with the executable
                reader = new JsonTextReader(new StreamReader("json/botConfig.json"));
                bc = JsonConvert.DeserializeObject<BotConfig>(File.ReadAllText("json/botConfig.json"));
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"BotStartup->BotConfig: Executable Level SetUp Exception:\n\t{e.Message}");
            }
        }
    }
}
