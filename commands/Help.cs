using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;


namespace DiscordAPP.commands
{

	public class Help
	{
		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork) 
		{	
				var embed = new EmbedBuilder()
                .WithTitle($"Мои команды")
                .WithColor(Color.Purple)
                .AddField("$money 💸", "курс ценных бумаг")
                .AddField("$weather + Желаемый город ⛅", "погода в любом городе")
                .AddField("$zamat 🔞", "хто ты cьoгoднi")
                .AddField("$stuffy 🤓", "Ha сколько % ты душный")
                .AddField("$swap + @UserName 🔄", "обменяться % душноты")
				.AddField("$list 📃", "узнать кто есть кто")
                .Build();

	        await message.Channel.SendMessageAsync(embed: embed);
		}
	} 
}
