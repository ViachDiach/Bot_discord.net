using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;
using Deedle;



using System.Globalization;


namespace DiscordAPP.commands
{
	public class DataList
	{

		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
		{	
			List<string[]> dataAnswer = _dbWork.Select();

			EmbedBuilder embed = new EmbedBuilder();
			embed.Title = "Сейчас ты узнаешь кто есть кто";
			embed.Color = new Color(0, 255, 0); // Зеленый цвет

			embed.AddField("Имя", "UserName", true);
			embed.AddField("Духота", "%", true);
			embed.AddField("Обзывалка", "(Без обид*)", true);

			for (int i = 0; i < dataAnswer.Count; i++)
			{
				embed.AddField("==============", dataAnswer[i][0], true);
				embed.AddField("=======", dataAnswer[i][1], true);
				embed.AddField("==========", dataAnswer[i][2], true);
			}	
			embed.Footer = new EmbedFooterBuilder();

			await message.Channel.SendMessageAsync(embed: embed.Build());
		}

	} 
}