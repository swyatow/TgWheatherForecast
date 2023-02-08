using Newtonsoft.Json;
using System.Text;
using System.Web;
using WheatherForcast;

internal class Program
{
    public static HttpClient client = new HttpClient();
    public static readonly string tgApiKey = "Telegram";
    public static string owmApiKey = "OWM";


    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        int offset = int.MinValue;

        Console.WriteLine("Бот запущен");

        while (true)
        {
            HttpResponseMessage response;
                response = offset == int.MinValue 
                ? await client.GetAsync(@$"https://api.telegram.org/bot{HttpUtility.UrlEncode(tgApiKey)}/getUpdates")
                : await client.GetAsync(@$"https://api.telegram.org/bot{HttpUtility.UrlEncode(tgApiKey)}/getUpdates?offset={offset}");
                       
            if (response.IsSuccessStatusCode)
            {
                string stringResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine(stringResponse);
                var update = JsonConvert.DeserializeObject<Update>(stringResponse);
                if (update.result.Length > 0)
                    offset = update.result.Last().update_id + 1;
                foreach (var item in update.result)
                {
                    if(item.message != null)
                        SendMessage(item.message);
                    else 
                        SendMessage(item.edited_message);
                }
            }
            Thread.Sleep(1000);
        }
    }
    async static void SendMessage(Message message)
    {
        string messageToBot;
        if (message.text.ToLower() == "/start")
        {
            messageToBot = "Добро пожаловать в ваш личный Гидрометцентр! Впишите название города, чтобы узнать текущую погоду.";
            await client.GetAsync(
                @$"https://api.telegram.org/bot{HttpUtility.UrlEncode(tgApiKey)}" 
                + @$"/sendMessage?chat_id={message.chat.id}&text={HttpUtility.UrlEncode(messageToBot)}");
            return;
        }
        var responseWheather = await client.GetAsync(@$"https://api.openweathermap.org/data/2.5/forecast?q={HttpUtility.UrlEncode(message.text)}&appid={owmApiKey}&units=metric&lang=ru");
        if (responseWheather.IsSuccessStatusCode)
        {
            var result = await responseWheather.Content.ReadAsStringAsync();
            var info = JsonConvert.DeserializeObject<WheatherInfo>(result);
            messageToBot =
                $"Погода в городе {info.City.Name}, {info.City.Country} на {DateTime.Now} - {info.List[0].Weather[0].Description}\n" +
                $"Температура воздуха: {Math.Round(info.List[0].Main.Temp, 1)}°С\n" +
                $"А по ощущениям: {Math.Round(info.List[0].Main.Feels_like, 1)}°С\n" +
                $"Ветер: {info.List[0].Wind.Speed}м/с, {GetWind(info.List[0].Wind.Deg)}\n" +
                $"Влажность: {info.List[0].Main.Humidity}%\n" +
                $"Давление: {Math.Round(info.List[0].Main.Grnd_level / 1.33322, 2)} мм рт. ст. (нормальное АД - 760 мм рт. ст.)";
        }
        else
        {
            messageToBot = "Неверно указан город! Проверьте введенные данные!";
        }
        await client.GetAsync(@$"https://api.telegram.org/bot{HttpUtility.UrlEncode(tgApiKey)}"
            + $@"/sendMessage?chat_id={message.chat.id}&text={HttpUtility.UrlEncode(messageToBot)}");
    }

    static string GetWind(int deg) =>
        deg switch
        {
            > 345 and <= 360 or >= 0 and < 15 => "северный",
            >= 15 and <= 75 => "северо-восточный",
            > 75 and < 105 => "восточный",
            >= 105 and <= 165 => "юго-восточный",
            > 165 and < 195 => "южный",
            >= 195 and <= 255 => "юго-западный",
            > 255 and < 285 => "западный",
            >= 285 and <= 345 => "северо-западный",
            _ => "???"
        };

}