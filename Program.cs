using Discord;
using Discord.WebSocket;
using Newtonsoft.Json;
using PracticeBot.Classes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PracticeBot
{
    class Program
    {

        public static SocketGuild crewGuild;
        static readonly Logging logging = new Logging();

        // Create the client
        public static DiscordSocketClient client;
        // Instantiate the configuration for the bot. This is where the token is stored.
        public BotConfig botConfig = new BotConfig();

        // Entry point, immediately run everything async
        public static void Main(/* string[] args */)
        => new Program().MainAsync().GetAwaiter().GetResult();

        /// <summary>
        /// "Real" Main() so everything is async.
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            // Instantiate the client, and add the logging
            client = new DiscordSocketClient
            (new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            // Populate the configuration from the BotConfig.json file for the client to use when connecting
            BotStartup.Startup(ref botConfig);

            // Create event handlers and start the bot
            // discord.net handled event handlers
            //client.ChannelCreated += ChannelCreated;
            //client.ChannelDestroyed += ChannelDestroyed;
            //client.ChannelUpdated += ChannelUpdated;
            //client.Connected += Connected;
            //client.CurrentUserUpdated += CurrentUserUpdated;
            //client.Disconnected += Disconnected;
            //client.GuildAvailable += GuildAvailable;
            //client.GuildMembersDownloaded += GuildMembersDownloaded;
            //client.GuildMemberUpdated += GuildMemberUpdated;
            //client.GuildUnavailable += GuildUnavailable;
            //client.GuildUpdated += GuildUpdated;
            //client.JoinedGuild += JoinedGuild;
            //client.LatencyUpdated += LatencyUpdated;
            //client.LeftGuild += LeftGuild;
            client.Log += Log;
            //client.LoggedIn += LoggedIn;
            //client.LoggedOut += LoggedOut;
            //client.MessageDeleted += MessageDeleted;
            //client.MessageReceived += MessageReceived;
            //client.MessagesBulkDeleted += MessagesBulkDeleted;
            //client.MessageUpdated += MessageUpdated;
            //client.ReactionAdded += ReactionAdded;
            //client.ReactionRemoved += ReactionRemoved;
            //client.ReactionsCleared += ReactionsCleared;
            //client.Ready += Ready;
            //client.RecipientAdded += RecipientAdded;
            //client.RecipientRemoved += RecipientRemoved;
            //client.RoleCreated += RoleCreated;
            //client.RoleDeleted += RoleDeleted;
            //client.RoleUpdated += RoleUpdated;
            //client.UserBanned += UserBanned;
            //client.UserIsTyping += UserIsTyping;
            //client.UserJoined += UserJoined;
            //client.UserLeft += UserLeft;
            //client.UserUnbanned += UserUnbanned;
            //client.UserUpdated += UserUpdated;
            //client.UserVoiceStateUpdated += UserVoiceStateUpdated;
            //client.VoiceServerUpdated += VoiceServerUpdated;

            // Bot specific event hanlders here

            // Connect client
            string token = botConfig.Token;
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Wait for events to happen
            await Task.Delay(-1);
        }


        //private async Task UserJoined(SocketGuildUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //private async Task UserLeft(SocketGuildUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task ReactionRemoved(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        //{
        //    throw new NotImplementedException();
        //}

        //private Task MessageUpdated(Cacheable<IMessage, ulong> arg1, SocketMessage arg2, ISocketMessageChannel arg3)
        //{
        //    throw new NotImplementedException();
        //}

        //private async Task MessageDeleted(Cacheable<IMessage, ulong> deletedMessage, ISocketMessageChannel channel)
        //{
        //    throw new NotImplementedException();
        //}

        //private async Task MessageReceived(SocketMessage message)
        //{
        //    throw new NotImplementedException();
        //}

        public void SerializeJsonObject(string filename, object value)
        {
            _ = Log(new LogMessage(LogSeverity.Verbose, $"Program", $"SerializeJson"));
            using StreamWriter file = File.CreateText($"{filename}");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, value);
        }

        public static Task Log(LogMessage msg)
        {
            return logging.Log(msg);
        }
    }
}
