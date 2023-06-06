using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Net;
using Newtonsoft.Json;
using DiscordAPP.db;

namespace DiscordAPP.commands
{
    public class Money
    {
        public static void MoneyParser(out string day, out decimal eur, out decimal usd, out decimal cny, out decimal uah, out decimal kzt)
        {
            var webClient = new WebClient();
            var json = webClient.DownloadString("https://www.cbr-xml-daily.ru/latest.js");
            dynamic data = JsonConvert.DeserializeObject(json)!;

            eur = data.rates.EUR;
            usd = data.rates.USD;
            cny = data.rates.CNY;
            uah = data.rates.UAH;
            kzt = data.rates.KZT;
			day =  data.date;
        }

        public static async Task ExecuteAsync(SocketMessage message, SQL _dbWork)
        { 
            string day;
            decimal eur, usd, cny, uah, kzt;
            string bull = "📈";
            string bear = "📉";
            MoneyParser(out day, out eur, out usd, out cny, out uah, out kzt);

            string[] smile = new string[5];
            List<string> oldRate = _dbWork.SelectMoney();
            decimal[] newRate = { 1/eur, 1/usd, 1/cny, 1/uah, 1/kzt};

            for (int i = 0; i < 5; i++)
            {
                if(Convert.ToDecimal(oldRate[i]) < newRate[i]) smile[i] = bear;
                else smile[i] = bull;
            }

            var embed = new EmbedBuilder()
                .WithTitle($"💰 Kypc валют на {day}")
                .WithColor(Color.Gold) 
                .AddField("1 Евро €", $"{(1 / eur):F2} ₽ {smile[0]}")
                .AddField("1 Доллар $", $"{(1 / usd):F2} ₽ {smile[1]}")
                .AddField("1 Юань ¥", $"{(1 / cny):F2} ₽ {smile[2]}")
                .AddField("1 Гривна ₴", $"{(1 / uah):F2} ₽ {smile[3]}")
                .AddField("1 Тенге ₸", $"{(1 / kzt):F2} ₽ {smile[4]}")
                .Build();

            _dbWork.AddMoney(newRate);    
            await message.Channel.SendMessageAsync(embed: embed);
        }
    }
}

