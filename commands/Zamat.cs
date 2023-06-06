using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using Discord.WebSocket; 
using DiscordAPP.db;

namespace DiscordAPP.commands
{

	public class Zamat
	{

		public static string ListWords(int review)
		{
			string who = "";
			Random random = new Random();
			List<string> lst;

			var low = new List<string> {
                "лучик солнца", "милашка", "прелесть", "снежинка", "умничка", "пушок", "лапочка", "крошка", "ягодка",
                "персик", "конфетка", "ангелочек", "светлячок", "птенчик", "мурлыка", "котенок", "киса", "умница"};

			var mid = new List<string> {
                "хвойда", "шльондра", "курва", "курвенний", "шльодравий", "хвойдяний", "курвар", "шльондер", "хвойдник",
                "курварство", "піхва", "потка", "піхвяний", "піхвистий", "спіхварити", "вигнанець", "грайня", "алкомэн",
                "гейропеец", "довбограник", "награйка", "прутень", "прутня", "прутнелиз", "прутнявий", "three hundred bucks",
                "cum", "dungeon master", "полоумный", "худоумный", "человек очень среднего ума", "пузатый", "тюрюхайло", "нечёса", "рохля", "чванливый", "пыня",  
				"boss of this gym", "fucking slave", "suck some dick", "чешский разбойник",
                "балабоk", "рохля", "пузырь", "любопытный", "вошь", "бісова ковінька", "булька з носа", "пришелепкуватий",
                "шмаркач", "тюхтій", "нездара", "дурепа", "йолоп", "боров", "быдло", "пустобрех", "вшивота", "трутень",
                "лепешка", "обалдуй", "погань", "профурсетка", "хабалка", "хмырь", "shit", "bastard", "cunt",
                "motherfucker", "fucking ass", "slut", "dumbass", "бикукле", "бикуля", "рередикт" };

            var high = new List<string> {
                "фуфло", "душный козел", "свиняче рило", "підорко", "обезьяна", "шушера", "sucker", "son of a bitch",
                "желчный", "бобик", "loser", "конь педальный", "геморрой", "шелупонь", "пердун"};

			if (review >= 0 && review <= 30) lst = low; 
			else if (review >= 31 && review <= 70) lst = mid; 
			else lst = high; 

			return  who = lst[random.Next(0, lst.Count)];	
		}

		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
		{	

			string author = $"{message.Author.Username}"; 
			string[] dataAnswer = _dbWork.Checking(author);

			Embed embed;
			if (dataAnswer[3].Length == 0)
			{
				int answer = _dbWork.Check(author);

				if (answer != 0)
				{
					string curse = ListWords(answer);
					_dbWork.AddCurse(author, curse);

					embed = new EmbedBuilder()
					.WithTitle($"Я бы не обижался, но")
					.WithColor(Color.DarkGreen)
					.WithDescription($"Ты сегодня гордо называешься - {curse}")
					.Build();
				}
				else 
				{
					embed = new EmbedBuilder()
					.WithColor(Color.DarkGreen)
					.AddField($"Воу-воу. Еще рано", "Сперва проверься на $stuffy")
					.Build();
				}
			}
			else
			{
				embed = new EmbedBuilder()
					.WithTitle($"Погодите-ка")
					.WithColor(Color.DarkGreen)
					.AddField($"Ты уже есть в списке.", "Смотри $list")
					.Build();

			} 
			string response = $"<@{message.Author.Id}>";
	        await message.Channel.SendMessageAsync(response, embed: embed);
		}
	} 
}