using System;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;

namespace DiscordAPP.commands
{

	public class Swap
    {
        public async Task<Embed> DBWorker(string author, string authorid, string argument, SQL _dbWork)
        {
            string want = author;
            string prey = authorid;

            string[] preyAnswer = _dbWork.PreyFound(prey);
            string[] wantAnswer = _dbWork.Checking(author);

            Embed embed;
            if (preyAnswer[3].Length != 0 && wantAnswer[3].Length != 0)
            {
                string tempCurse = preyAnswer[3];
                _dbWork.AddCurse(preyAnswer[0], wantAnswer[3]);
                _dbWork.AddCurse(author, preyAnswer[3]);

                embed = new EmbedBuilder()
                    .WithTitle($"Ясненько")
                    .WithColor(Color.Orange)
                    .WithDescription($"{preyAnswer[0]} и {wantAnswer[0]} взялись за голандский штурвал")
                    .Build();

            }
            else
            {
                embed = new EmbedBuilder()
                .WithTitle($"Oops, something went wrong")
                .WithColor(Color.Red)
                .WithDescription("Увы, кто-то из вас все еще не прошел $zamat")
				.Build();
            }

            return embed;
        }

        public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
        {
            string author = $"{message.Author.Username}";
            string patter = @"^\$[a-zA-Z]+\b";
            Regex regex = new Regex(patter);

            string argument = regex.Replace(message.Content, string.Empty);
            argument = argument.ToLower().Trim();
            string authorid = argument.Replace("<", "").Replace(">", "").Replace("@", "");

            Swap swap = new Swap(); // Создаем экземпляр класса Swap

            Embed embed = await swap.DBWorker(author, authorid, argument, _dbWork); // Вызываем метод DBWorker через экземпляр

            await message.Channel.SendMessageAsync(embed: embed);
        }
    }

}






