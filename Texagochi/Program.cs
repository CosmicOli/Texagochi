using Discord.WebSocket;
using Discord;
using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Texagochi
{
    internal class Program
    {
        private static Player_Handler player_Handler = new Player_Handler();

        private static DiscordSocketClient client;
        public static async Task Main()
        {
            // When working with events that have Cacheable<IMessage, ulong> parameters,
            // you must enable the message cache in your config settings if you plan to
            // use the cached message entity. 
            var _config = new DiscordSocketConfig { MessageCacheSize = 100 };
            client = new DiscordSocketClient(_config);

            var token = "";

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            client.MessageUpdated += MessageUpdated;
            client.Ready += () =>
            {
                Console.WriteLine("Bot is connected!");
                Client_Ready();
                return Task.CompletedTask;
            };

            //client.Ready += Client_Ready;
            client.SlashCommandExecuted += SlashCommandHandler;

            await Task.Delay(-1);
        }

        private static async Task MessageUpdated(Cacheable<IMessage, ulong> before, SocketMessage after, ISocketMessageChannel channel)
        {
            // If the message was not in the cache, downloading it will result in getting a copy of `after`.
            var message = await before.GetOrDownloadAsync();
            Console.WriteLine($"{message} -> {after}");
        }

        public static async Task Client_Ready()
        {
            SocketGuild guild = client.GetGuild(735400487322386494);

            /*SlashCommandBuilder statusCommand = new SlashCommandBuilder();

            statusCommand.WithName("status");

            statusCommand.WithDescription("This displays the current status of your Tex!");


            SlashCommandBuilder registerCommand = new SlashCommandBuilder();

            statusCommand.WithName("register");

            statusCommand.WithDescription("This registers a tex in your name!");


            SlashCommandBuilder playCommand = new SlashCommandBuilder();

            playCommand.WithName("play");

            playCommand.WithDescription("This lets Tex play!");


            SlashCommandBuilder feedCommand = new SlashCommandBuilder();

            feedCommand.WithName("feed");

            feedCommand.WithDescription("This lets Tex eat!");*/


            SlashCommandBuilder chillingCommand = new SlashCommandBuilder();

            chillingCommand.WithName("chilling");

            chillingCommand.WithDescription("This lets Tex chill!");

            try
            {
                //await guild.CreateApplicationCommandAsync(statusCommand.Build());
                //await guild.CreateApplicationCommandAsync(registerCommand.Build());
                //await guild.CreateApplicationCommandAsync(playCommand.Build());
                //await guild.CreateApplicationCommandAsync(feedCommand.Build());
                await guild.CreateApplicationCommandAsync(chillingCommand.Build());
            }
            catch (ApplicationCommandException exception)
            {
                // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

                // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
                Console.WriteLine(json);
            }
        }

        private static async Task SlashCommandHandler(SocketSlashCommand command)
        {
            KeyValuePair<FileAttachment, string> output;

            switch (command.CommandName)
            {
                case "register":
                    output = player_Handler.RegisterTex(command.User.AvatarId);

                    await command.RespondWithFileAsync(output.Key, output.Value);
                    break;
                case "status":
                    output = player_Handler.Status(command.User.AvatarId);

                    await command.RespondWithFileAsync(output.Key, output.Value);
                    break;
                case "play":
                    output = player_Handler.Play(command.User.AvatarId);

                    await command.RespondWithFileAsync(output.Key, output.Value);
                    break;
                case "feed":
                    output = player_Handler.Feed(command.User.AvatarId);

                    await command.RespondWithFileAsync(output.Key, output.Value);
                    break;
                case "chilling":
                    output = player_Handler.Chilling(command.User.AvatarId);

                    await command.RespondWithFileAsync(output.Key, output.Value);
                    break;
            }
        }

    }
}
