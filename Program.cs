using System;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Xml.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordAPP.commands;
using DiscordAPP.db;

namespace DiscordAPP
{
    
    class Program
    {
        private DiscordSocketClient _client;           // "?" = часть типа данных. указывает, что переменная может принимать значение null
        private SQL _dbWork;                            //  объявляем SQL для постоянного подключения к БД
        private CommandsHandler _commandHandler;         // "_" используется для обозначения приватных переменных или полей

        public static Task Main(string[] args) => new Program().RunBotAsync();
        // Program() создает новый экземпляр класса Program, который содержит методы для запуска бота.
        // RunBotAsync() запускает асинхронную операцию запуска бота Discord.

        private async Task RunBotAsync()
        {
            var token = "YOUR_TOKEN";
            var _config = new DiscordSocketConfig { MessageCacheSize = 100, GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent };

            _dbWork = new SQL();
            _dbWork.init();
            _commandHandler = new CommandsHandler();
            _client = new DiscordSocketClient(_config);
             
            _client.Log += Log;
            _client.MessageReceived += EventMessageReceived;
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1); // Бесконечное ожидание завершения работы бота
        }

        private Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }

        private Task EventMessageReceived(SocketMessage message)
        {
            _commandHandler.MsgLogger(message);
            _commandHandler.Switcher(message, _dbWork);

            return Task.CompletedTask;
        }
    }

    class CommandsHandler
    {
        public void MsgLogger(SocketMessage message)
        {
            if (!message.Author.IsBot)
            {
                Console.WriteLine($"-------------");
                Console.WriteLine($"author -> {message.Author.Username}");
                Console.WriteLine($"content -> {message.Content}");
                Console.WriteLine($"-------------");
            }
        }

        public async  Task Switcher(SocketMessage message, SQL _dbWork)
        // public  Task Switcher(SocketMessage message, SQL _dbWork)
        {   
            if (!message.Content.StartsWith('$'))
                return; // Task.CompletedTask;

            switch (message.Content.Split(' ')[0])
            {
                 case "$help":
                    {
                        await Help.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$money":
                    {
                        await Money.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$weather":
                    {
                        await Weather.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$stuffy":
                    {
                        await Stuffy.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$zamat": 
                    {
                        await Zamat.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$list": 
                    {
                        await DataList.ExecuteAsync(message, _dbWork);
                        break;
                    }
                case "$swap": 
                    {
                        await Swap.ExecuteAsync(message, _dbWork);
                        break;
                    }    
                case "$presents": 
                    {
                        await Present.ExecuteAsync(message, _dbWork);
                        break;
                    } 
                    
                default:
                    {   
                        await message.Channel.SendMessageAsync($"Э, <@{message.Author.Id}>, кэфтемэ, такое еще не умею");
                        break;
                    }    
            }

            // return Task.CompletedTask;
        }
    }
}

