using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBot_librarian.Handlers.Quote
{
    public class QuoteHandler
    {
        private readonly ITelegramBotClient _botClient;

        public QuoteHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task HandleQuoteAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            var chatId = callbackQuery.Message.Chat.Id;

            Random random = new Random();
            int quoteNumber = random.Next(1, 16);
            string quoteText = quoteNumber switch
            {
                1 => "🇺🇦Світ ловив мене, та не спіймав.🇺🇦\r\nГригорій Сковорода",
                2 => "🇺🇦Не можна змінювати світ, не змінивши самого себе 🇺🇦\r\nТарас Шевченко",
                3 => "🇺🇦У світі немає нічого могутнішого за істину🇺🇦\r\nЛеся Українка",
                4 => "🇺🇦Ми не можемо змінити напрямок вітру, але можемо налаштувати вітрила, щоб досягти свого курсу🇺🇦\r\nВасиль Сухомлинський",
                5 => "🇺🇦Вірити в себе - це перша таємниця успіху🇺🇦\r\nМарія Заньковецька",
                6 => "🇺🇦Тільки розвинений духом народ може бути вільним🇺🇦\r\nМихайло Грушевський",
                7 => "🇺🇦Ми досі ще рятуємо дистрофію тіл, а за прогресуючу дистрофію душ — нам байдуже🇺🇦\r\nВасиль Стус",
                8 => "🇺🇦Людина може забути своє ім'я, але вона ніколи не забуде свою історію🇺🇦\r\nОлександр Довженко",
                9 => "🇺🇦Щастя-це не мати те, чого хочеш, а хотіти те, що маєш🇺🇦\r\nСофія Русова",
                10 => "🇺🇦Мужність не дається напрокат🇺🇦\r\nЛіна Костенко",
                11 => "🇺🇦Нема любові без ненависті, як нема білого без чорного! Хочете любові, то мусите ненавидіти🇺🇦\r\nВолодимир Винниченко",
                12 => "🇺🇦Тяжко, тяжко в світі жить І нікого не любить🇺🇦\r\nТарас Шевченко",
                13 => "🇺🇦Мова росте елементарно, разом з душею народу🇺🇦\r\nІван Франко",
                14 => "🇺🇦Якщо ви вдало виберете справу і вкладете в неї всю свою душу, то щастя саме відшукає вас.🇺🇦\r\nКостянтин Ушинський",
                15 => "🇺🇦А хто з приятеля перекинувся в ворога, той, значить, і раніше не був приятелем і не буде.🇺🇦\r\nІван Франко",
                _ => "🇺🇦Невідома цитата🇺🇦"
            };

            await _botClient.SendTextMessageAsync(chatId: chatId, text: quoteText, cancellationToken: cancellationToken);
        }
    }
}