using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;

namespace DiscordAPP.commands
{

	public class Stuffy
	{
		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
		{	
			string author = message.Author.Username;
			string patter = @"^\$[a-zA-Z]+\b";
            Regex regex = new Regex(patter);

            string argument = regex.Replace(message.Content, string.Empty);
			argument = argument.ToLower().Trim(); 
            string authorid = argument.Replace("<", "").Replace(">", "").Replace("@", "");
			string[] dataAnswer = _dbWork.Checking(author);

			Embed embed;
			if (dataAnswer[2].Length == 0)
			{
				Random rnd = new Random();
				int rate = rnd.Next(1, 101);

				_dbWork.AddProcent(author, authorid, rate);

				embed = new EmbedBuilder()
					.WithTitle($"Hy и ну...")
					.WithColor(Color.Orange)
					.WithDescription($"Так ты сегодня душный аж на {rate}%")
					.Build();
			}
			else {
				
				embed = new EmbedBuilder()
					.WithTitle($"Погодите-ка")
					.WithColor(Color.Magenta)
					.AddField($"Ты уже есть в списке", "P.S. Смотрите $list")
					.Build();
				};
			
			string response = $"<@{message.Author.Id}>";
	        await message.Channel.SendMessageAsync(response, embed: embed);
		}
	} 
}
