using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;

namespace DiscordAPP.commands
{

	public class Present
	{
		public static void TestInventery(string author, SQL _dbWork) 
		{
			string loot = "СиШарпер";
			_dbWork.UpdateInventery(author, loot);
			Console.WriteLine("Добавили loot");
		}

		public static void Testbalanse(string author, SQL _dbWork) 
		{
			double salary = 0.1;
			_dbWork.Updatebalanse(author, salary);
			Console.WriteLine("Добавили salary");
		}

		public static string[] TestCheking(string author, SQL _dbWork) 
		{
			string[] answer = _dbWork.CheckBalanse(author);
			return answer;
		}

		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
		{	
			string author = $"{message.Author.Username}"; 
			string[] allInfop = new string [2];

			string patter = @"^\$[a-zA-Z]+\b";
            Regex regex = new Regex(patter);
            string argument = regex.Replace(message.Content, string.Empty);
			argument = argument.ToLower().Trim(); 

			Embed embed;
			if (argument == "1") 
			{
				Testbalanse(author, _dbWork);

				embed = new EmbedBuilder()
                .WithTitle($"Testbalanse")
                .WithColor(Color.Purple)
                .WithDescription("Пополнили твой баланс")
				.Build();
			}
			else if(argument == "2")  
			{
				TestInventery(author, _dbWork);
				
				embed = new EmbedBuilder()
                .WithTitle($"TestInventery")
                .WithColor(Color.Purple)
                .WithDescription("Пополнили твой инветарь")
				.Build();
			}
			else 
			{
				allInfop = TestCheking(author, _dbWork);

				embed = new EmbedBuilder()
                .WithTitle($"TestTese  \n Выбери что желаешь")
                .WithColor(Color.Purple)
                .WithDescription($"1 - {allInfop[0]} \n 2 - {allInfop[1]}")
				.Build();
			}

	        await message.Channel.SendMessageAsync(embed: embed);
		}
	} 
}