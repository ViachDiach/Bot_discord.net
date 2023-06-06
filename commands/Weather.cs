using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using Newtonsoft.Json;
using DiscordAPP.db;


namespace DiscordAPP.commands
{

	public class Weather
	{	
		public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork) 
		{	
			
			string patter = @"^\$[a-zA-Z]+\b";
            Regex regex = new Regex(patter);

            string argument = regex.Replace(message.Content, string.Empty);
			argument = argument.ToLower().Trim(); 

			Embed embed;

			try{
				var WebClient = new WebClient();
				var MyApiKey = "aef3809ca552f0de2aec39a17bdea423";
				var json = WebClient.DownloadString($"https://api.openweathermap.org/data/2.5/weather?q={argument}&appid={MyApiKey}&units=metric&lang=ru");
				dynamic data = JsonConvert.DeserializeObject(json)!;

				var cityName = data.name;
				int minDegreesStr = data.main.temp_min;
				int maxDegreesStr = data.main.temp_max;
				int nowDegreesStr = data.main.feels_like;
				var cloudStr = data.weather[0].description;
				var humidity = data.main.humidity;
				var windSpeed = data.wind.speed;
				var icon = data.weather[0].icon;

				string iconUrl = $"https://openweathermap.org/img/wn/{icon}@2x.png";

				embed = new EmbedBuilder()
                .WithTitle($"Погоду в городе {cityName} сегодня:")
                .WithColor(Color.Blue)
				.WithCurrentTimestamp()
                .AddField("Минимально:", $"{minDegreesStr}°")
                .AddField("Максимально:", $"{maxDegreesStr}°")
                .AddField("Сейчас ощущается как:", $"{nowDegreesStr}°")
                .AddField("Осадки:", $"{cloudStr}")
                .AddField("Влажность:", $"{humidity}%")
				.AddField("Скорость ветра:", $"{windSpeed} м/c")
				.WithImageUrl(iconUrl)
                .Build();
			}
			catch
			{	
				embed = new EmbedBuilder()
                .WithTitle($"Oops, something went wrong")
                .WithColor(Color.Red)
                .AddField("Такой город, увы, найти не удалось.", "Попробуй еще раз")
				.Build();
			}
			 
			await message.Channel.SendMessageAsync(embed: embed);
            
		}
    } 
}