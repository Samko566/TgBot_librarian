using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Python.Runtime;

namespace TgBot_librarian
{
    class Program
    {
        private static Py.GILState _gilState; // Змінна для ініціалізації Python
        private static DebtService _debtService;
        static TelegramBotClient botClient = new TelegramBotClient("5953908796:AAEkg6yZDvqSb5u0Ik_Oj3kv8xsFMjoIDgQ");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            #region [Кнопки]
            InlineKeyboardMarkup mainMenu = new InlineKeyboardMarkup(new[]{
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Про бібліотеку 📖", callbackData: "about") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Каталог книг 📚", callbackData: "catalog") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Підібрати книгу за описом 🤖", callbackData: "ai") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Цитата дня 💪🇺🇦", callbackData: "quote") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Вікторини🏆", callbackData: "quiz") }});

            InlineKeyboardMarkup aboutMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Соціальні мережі та контакти 📱", callbackData: "contacts")},
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Комп'ютерний зал 💻", callbackData: "computer") },
        new[]{InlineKeyboardButton.WithCallbackData(text: "Адресса бібліотеки 🗺", callbackData: "address"),
              InlineKeyboardButton.WithCallbackData(text: "Графік роботи 🕐", callbackData: "schedule")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Історія бібліотеки - багато тексту 🏠", callbackData: "history")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu")},});

            InlineKeyboardMarkup socialMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Word файл зі списком наявної літератури", callbackData: "file")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Боржники/Неповернені книги", callbackData: "debt")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu")},});

            InlineKeyboardMarkup catalogMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithWebApp(text: "Facebook", new WebAppInfo() { Url = "https://www.facebook.com/BibliotekaKozelets" }) },
        new[]{InlineKeyboardButton.WithUrl(text: "Сайт бібліотеки", url: "http://www.kozelets-cbs.edukit.cn.ua/")},
        new[]{InlineKeyboardButton.WithUrl(text: "Пошта бібліотеки", url: "mailto:libkozelets@gmail.com")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в меню ↩️", callbackData: "toAboutMenu") },});

            InlineKeyboardMarkup quizMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Вікторина - Українська кухня", callbackData: "cookQuiz")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Вікторина - Що ти знаєш про українську мову", callbackData: "ukrQuiz")}, });

            #region [Вікторина - Українська кухня]
            InlineKeyboardMarkup start1Menu = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Почати вікторину", callbackData: "start1") }, });

            InlineKeyboardMarkup quiz11 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Святий Вечір (вечір напередодні Різдва)", callbackData: "1quizFirst1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Весілля", callbackData: "1quizFirst2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Сватання", callbackData: "1quizFirst3") }});

            InlineKeyboardMarkup firstNext = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "firstNext") }, });

            InlineKeyboardMarkup quiz12 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "100", callbackData: "1quizSecond1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "200", callbackData: "1quizSecond2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Понад 300", callbackData: "1quizSecond3") }});

            InlineKeyboardMarkup secondNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "secondNext") }, });

            InlineKeyboardMarkup quiz13 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Хрін, кріп, кмин, м'ята, аніс", callbackData: "1quizThird1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Кориця, ваніль, чорний перець, кмин", callbackData: "1quizThird2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Хмелі-сунелі, паприка, імбир, мускатний горіх", callbackData: "1quizThird3") }});

            InlineKeyboardMarkup thirdNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "thirdNext") }, });

            InlineKeyboardMarkup quiz14 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Селище біля Молдови", callbackData: "1quizFourth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Маленькі бендеревці", callbackData: "1quizFourth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Млинці з м'ясом", callbackData: "1quizFourth3") }});

            InlineKeyboardMarkup fourthNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fourthNext") }, });

            InlineKeyboardMarkup quiz15 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Весілля", callbackData: "1quizFifth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "День народження", callbackData: "1quizFifth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Бейбі Шауер", callbackData: "1quizFifth3") }});

            InlineKeyboardMarkup fifthNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fifthNext") }, });

            InlineKeyboardMarkup quiz16 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Чорнослив", callbackData: "1quizSixth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Корінь селери", callbackData: "1quizSixth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Білий буряк", callbackData: "1quizSixth3") }});

            InlineKeyboardMarkup sixthNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "sixthNext") }, });

            InlineKeyboardMarkup quiz17 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Борщ та каша - сила наша\"", callbackData: "1quizSeventh1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Найкращій букет - то шматок телятини\"", callbackData: "1quizSeventh2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Голод не знає сорому\"", callbackData: "1quizSeventh3") }});

            InlineKeyboardMarkup seventhNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "seventhNext") }, });

            InlineKeyboardMarkup quiz18 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Відкритий пиріг з будь-якою начинкою, приготований у печі", callbackData: "1quizEighth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Друга страва з м'яса та картоплі", callbackData: "1quizEighth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Будь-яка страва з печінки", callbackData: "1quizEighth3") }});

            InlineKeyboardMarkup eighthNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "eighthNext") }, });

            InlineKeyboardMarkup quiz19 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "З крові диких тварин, наприклад, кабана", callbackData: "1quizNinth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "З бурякового квасу", callbackData: "1quizNinth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "З бульйону на ребрах", callbackData: "1quizNinth3") }});

            InlineKeyboardMarkup ninthNext = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "ninthNext") }, });

            InlineKeyboardMarkup quiz10 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Гуцульська овеча бринза", callbackData: "1quizTenth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Галушки", callbackData: "1quizTenth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Свинячі ребра", callbackData: "1quizTenth3") }});
            #endregion

            #region [Вікторина - Що ти знаєш про українську мову]
            InlineKeyboardMarkup start2Menu = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Почати вікторину", callbackData: "start2") }, });

            InlineKeyboardMarkup quiz21 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Мед та страва", callbackData: "2quizFirst1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Козак і Січ", callbackData: "2quizFirst2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Борщ та вареники", callbackData: "2quizFirst3") }});

            InlineKeyboardMarkup firstNext2 = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "firstNext2") }, });

            InlineKeyboardMarkup quiz22 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Івана Котляревського", callbackData: "2quizSecond1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Тараса Шевченка", callbackData: "2quizSecond2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Івана Франка", callbackData: "2quizSecond3") }});

            InlineKeyboardMarkup secondNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "secondNext2") }, });

            InlineKeyboardMarkup quiz23 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Понад 2000 слів", callbackData: "2quizThird1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "15 000 слів", callbackData: "2quizThird2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "896 слів", callbackData: "2quizThird3") }});

            InlineKeyboardMarkup thirdNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "thirdNext2") }, });

            InlineKeyboardMarkup quiz24 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Ф", callbackData: "2quizFourth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "П", callbackData: "2quizFourth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "К", callbackData: "2quizFourth3") }});

            InlineKeyboardMarkup fourthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fourthNext2") }, });

            InlineKeyboardMarkup quiz25 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Несе Галя воду”", callbackData: "2quizFifth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"При долині кущ калини\"", callbackData: "2quizFifth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Дунаю, чому смутен течеш?”", callbackData: "2quizFifth3") }});

            InlineKeyboardMarkup fifthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fifthNext2") }, });

            InlineKeyboardMarkup quiz26 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Польська", callbackData: "2quizSixth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Білоруська", callbackData: "2quizSixth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Російська", callbackData: "2quizSixth3") }});

            InlineKeyboardMarkup sixthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "sixthNext2") }, });

            InlineKeyboardMarkup quiz27 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Енеїда” Івана Котляревського", callbackData: "2quizSeventh1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Лісова пісня” Лесі Українки", callbackData: "2quizSeventh2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Заповіт” Тараса Шевченка", callbackData: "2quizSeventh3") }});

            InlineKeyboardMarkup seventhNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "seventhNext2") }, });

            InlineKeyboardMarkup quiz28 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Близько 256 000", callbackData: "2quizEighth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Десь 153 000", callbackData: "2quizEighth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Приблизно 580 000", callbackData: "2quizEighth3") }});

            InlineKeyboardMarkup eighthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "eighthNext2") }, });

            InlineKeyboardMarkup quiz29 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "20 млн", callbackData: "2quizNinth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "41 - 45 млн", callbackData: "2quizNinth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "55 млн", callbackData: "2quizNinth3") }});

            InlineKeyboardMarkup ninthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "ninthNext2") }, });

            InlineKeyboardMarkup quiz20 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "19", callbackData: "2quizTenth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "26", callbackData: "2quizTenth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "31", callbackData: "2quizTenth3") }});
            #endregion

            InlineKeyboardMarkup backMenu = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Повернутися в меню ↩️", callbackData: "toAboutMenu") }, });
            InlineKeyboardMarkup toMenu = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu") }, });
            #endregion

            if (update.Type == UpdateType.Message)
            {
                var chatid = update.Message.Chat.Id;
                var messageText = update.Message.Text;
                string firstName = update.Message.From.FirstName;
                string secondName = update.Message.From.LastName;
                using var context = new DebtDbContext();
                var DebtService = new DebtService(context);

                #region [Рекомендація книг]
                 if (messageText == "/ai")
                 {
                    using (Py.GIL())
                    {
                        // Виклик функції Python та отримання результату
                        string result = GetRecommendationsFromPython(messageText);

                        // Відправка результату користувачу
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, result);

                    }
                    // Завершити роботу з Python
                    PythonEngine.Shutdown();
                 }
                 #endregion 

                #region [Перше повідомлення]
                if (messageText == "/start")
                {
                    var task = SendPhoto(chatid, @"pic/libphoto.jpg", cancellationToken);
                    task.Wait();
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: $"Привіт {firstName} {secondName}! \nЯ чат бот-бібліотекар, що б ви хотіли дізнатись?",
                        replyMarkup: mainMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Боржники]
                if (update.Message.Chat.Id == -1001611949669)
                {
                    if (messageText == "/debt")
                    {
                        var text = DebtService.GetDebtText();
                        await botClient.SendTextMessageAsync(chatid, text, cancellationToken: cancellationToken);
                    }
                    if (messageText.StartsWith("/save"))
                    {
                        var lines = messageText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                                              .Select(line => line.Trim())
                                              .ToList();
                        if (lines.Any())
                        {// якщо користувач надіслав багато рядків, то зберігаємо їх
                            var text = string.Join(Environment.NewLine, lines);
                            DebtService.UpdateDebtText(text);
                            await botClient.SendTextMessageAsync(chatid, "Список боржників оновлено.", cancellationToken: cancellationToken);
                        }
                        else
                        {// якщо користувач надіслав один рядок, то зберігаємо його
                            var newText = messageText.Replace("/save ", "");
                            DebtService.UpdateDebtText(newText);
                            await botClient.SendTextMessageAsync(chatid, "Список боржників оновлено.", cancellationToken: cancellationToken);
                        }
                    }
                }
            }
            #endregion

            #region [Обробники кнопок]
            if (update.Type == UpdateType.CallbackQuery)
            {
                var chatid = update.CallbackQuery.Message.Chat.Id;
                var callbackData = update.CallbackQuery.Data;

                using var context = new DebtDbContext();
                var DebtService = new DebtService(context);
                var debtText = DebtService.GetDebtText();

                #region [Аі]
                if (callbackData == "ai")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatid,
                    text: "Бот може порекомендувати книгу по темі. \nДля цього введіть команду " +
                    "/ai або сліва снизу в меню нажміть на команду і введіть запрос. \nТакож ви можете змінювати " +
                    "кількість рекомендацій в повідомленні, написавши цифри 1 2 3 у своєму запросі.",
                    cancellationToken: cancellationToken);

                }
                #endregion

                #region [Про бібліотеку]
                if (callbackData == "about")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Натисніть на одну із кнопок, щоб отримати інформацію про бібліотеку 👇:",
                        replyMarkup: aboutMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Word файл]
                if (callbackData == "file")
                {
                    SendDocument(chatid, @"files/Каталог.docx", cancellationToken);
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Word файл зі списком наявних книг бібліотеки",
                        replyMarkup: toMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Неповернені книги]
                if (callbackData == "debt")
                {

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: debtText,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Каталог книг]
                if (callbackData == "catalog")
                {
                    var pic2 = SendPhoto(chatid, @"pic/shelves.jpg", cancellationToken);
                    pic2.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "В цьому розділі ви можете отримати інформацію про те які книги фізично наявні в бібліотеці. \nФункція прочитати книгу онлайн не передбачена." +
                        "\n\nОберіть, що саме стосовно асортименту ви б хотіли переглянути:",
                        replyMarkup: socialMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Цитата дня]
                if (callbackData == "quote")
                {
                    Random random = new Random();
                    int quoteNumber = random.Next(1, 16);
                    string quoteText = "";
                    switch (quoteNumber)
                    {
                        case 1:
                            quoteText = "🇺🇦Світ ловив мене, та не спіймав.🇺🇦\r\nГригорій Сковорода";
                            break;
                        case 2:
                            quoteText = "🇺🇦Не можна змінювати світ, не змінивши самого себе 🇺🇦\r\nТарас Шевченко";
                            break;
                        case 3:
                            quoteText = "🇺🇦У світі немає нічого могутнішого за істину🇺🇦\r\nЛеся Українка";
                            break;
                        case 4:
                            quoteText = "🇺🇦Ми не можемо змінити напрямок вітру, але можемо налаштувати вітрила, щоб досягти свого курсу🇺🇦\r\nВасиль Сухомлинський";
                            break;
                        case 5:
                            quoteText = "🇺🇦Вірити в себе - це перша таємниця успіху🇺🇦\r\nМарія Заньковецька";
                            break;
                        case 6:
                            quoteText = "🇺🇦Тільки розвинений духом народ може бути вільним🇺🇦\r\nМихайло Грушевський";
                            break;
                        case 7:
                            quoteText = "🇺🇦Ми досі ще рятуємо дистрофію тіл, а за прогресуючу дистрофію душ — нам байдуже🇺🇦\r\nВасиль Стус";
                            break;
                        case 8:
                            quoteText = "🇺🇦Людина може забути своє ім'я, але вона ніколи не забуде свою історію🇺🇦\r\nОлександр Довженко";
                            break;
                        case 9:
                            quoteText = "🇺🇦Щастя-це не мати те, чого хочеш, а хотіти те, що маєш🇺🇦\r\nСофія Русова";
                            break;
                        case 10:
                            quoteText = "🇺🇦Мужність не дається напрокат🇺🇦\r\nЛіна Костенко";
                            break;
                        case 11:
                            quoteText = "🇺🇦Нема любові без ненависті, як нема білого без чорного! Хочете любові, то мусите ненавидіти🇺🇦\r\nВолодимир Винниченко";
                            break;
                        case 12:
                            quoteText = "🇺🇦Тяжко, тяжко в світі жить І нікого не любить🇺🇦\r\nТарас Шевченко";
                            break;
                        case 13:
                            quoteText = "🇺🇦Мова росте елементарно, разом з душею народу🇺🇦\r\nІван Франко";
                            break;
                        case 14:
                            quoteText = "🇺🇦Якщо ви вдало виберете справу і вкладете в неї всю свою душу, то щастя саме відшукає вас.🇺🇦\r\nКостянтин Ушинський";
                            break;
                        case 15:
                            quoteText = "🇺🇦А хто з приятеля перекинувся в ворога, той, значить, і раніше не був приятелем і не буде.🇺🇦\r\nІван Франко";
                            break;
                    }
                    await botClient.SendTextMessageAsync(chatId: chatid, text: quoteText);

                }
                #endregion

                #region [Вікторини]
                if (callbackData == "quiz")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Оберіть вікторину 👇:",
                        replyMarkup: quizMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Вікторина - Українська кухня]
                if (callbackData == "cookQuiz")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz0.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вітаю вас у нашій вікторині що присвячена українській кухні!\n ",
                        replyMarkup: start1Menu,
                        cancellationToken: cancellationToken);
                }

                if (callbackData == "start1")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz1.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "До якого свята українці готують такий стіл, як на фото?\r\n ",
                        replyMarkup: quiz11,
                        cancellationToken: cancellationToken);
                }

                if (callbackData == "1quizFirst1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Правильно 👍 \r\nПереддень Різдва — Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя",
                        replyMarkup: firstNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFirst2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Невірно, \r\nТакий стіл готували на Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя",
                        replyMarkup: firstNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFirst3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ну майже але невірно, \r\nТакий стіл готували на Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя",
                        replyMarkup: firstNext,
                        cancellationToken: cancellationToken);
                }
                //2
                if (callbackData == "firstNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz2.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Скільки років такій традиційній страві як український борщ?",
                        replyMarkup: quiz12,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSecond1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Нуу...ні, трішки більше\r\n\r\n Українському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.",
                        replyMarkup: secondNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSecond2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Нуу...ні, трішки більше\r\n\r\nУкраїнському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.",
                        replyMarkup: secondNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSecond3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Це правильна відповідь.\r\n\r\nУкраїнському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.",
                        replyMarkup: secondNext,
                        cancellationToken: cancellationToken);
                }
                //3
                if (callbackData == "secondNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz3.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Обери вірний перелік традиційних українських спецій.",
                        replyMarkup: quiz13,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizThird1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Це правильна відповідь. Ти молодець!",
                        replyMarkup: thirdNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizThird2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Клацав навмання? Ні",
                        replyMarkup: thirdNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizThird3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Хмелі-сунелі - це найпопулярніша кавказька приправа... далі можна не продовжувати",
                        replyMarkup: thirdNext,
                        cancellationToken: cancellationToken);
                }
                //4
                if (callbackData == "thirdNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz4.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Бендерики - це...",
                        replyMarkup: quiz14,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFourth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Тобі навіть картинка не допомогла ? Ні.\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.",
                        replyMarkup: fourthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFourth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Серйозно? У нас кулінарний тест...\r\n\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.",
                        replyMarkup: fourthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFourth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вірно! А ти розбираєшся в їжі!\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.",
                        replyMarkup: fourthNext,
                        cancellationToken: cancellationToken);
                }
                //5
                if (callbackData == "fourthNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz5.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Атрибутом якого свята є каравай?",
                        replyMarkup: quiz15,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFifth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так, ти молодець!\r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят. ",
                        replyMarkup: fifthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFifth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні, там інші атрибути\r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят. ",
                        replyMarkup: fifthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizFifth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні, \r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят. ",
                        replyMarkup: fifthNext,
                        cancellationToken: cancellationToken);
                }
                //6
                if (callbackData == "fifthNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz6.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "В гуцульському борщі має бути",
                        replyMarkup: quiz16,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSixth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. В деяких регіонах дійсно є борщ з чорносливом, але не в гуцульському \r\nДля гуцульского борщу використовують два види буряку - звичайний і кормовий (білий). ",
                        replyMarkup: sixthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSixth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні \r\nДля гуцульского борщу використовують два види буряку - звичайний і кормовий (білий). Червоний буряк дає червоний колір, а кормовий - робить страву солодкуватою.",
                        replyMarkup: sixthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSixth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Правильно! Для гуцульского борщу використовують два види буряку - звичайний і кормовий (білий). Червоний буряк дає червоний колір, а кормовий - робить страву солодкуватою.",
                        replyMarkup: sixthNext,
                        cancellationToken: cancellationToken);
                }
                //7
                if (callbackData == "sixthNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz7.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Відомий вислів Тараса Шевченка про їжу звучить так:\r\n",
                        replyMarkup: quiz17,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSeventh1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "То звичайно сила, але Шеченко так не говорив.\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні  " +
                        "в своїй автобіографічній повісті «Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.",
                        replyMarkup: seventhNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSeventh2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Зовсім мимо.\r\n\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні  в своїй автобіографічній " +
                        "повісті «Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.",
                        replyMarkup: seventhNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizSeventh3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "ТАК!\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні  в своїй автобіографічній повісті " +
                        "«Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.",
                        replyMarkup: seventhNext,
                        cancellationToken: cancellationToken);
                }
                //8
                if (callbackData == "seventhNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz8.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Печеня - це...",
                        replyMarkup: quiz18,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizEighth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Як смачно, але ні..\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.",
                        replyMarkup: eighthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizEighth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вірно. Мабудь, ти з їжею на \"ти\"\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.",
                        replyMarkup: eighthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizEighth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.",
                        replyMarkup: eighthNext,
                        cancellationToken: cancellationToken);
                }
                //9
                if (callbackData == "eighthNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz9.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "На Миколаївщині популярний матвіївський зелений борщ із бичками. А на основі чого готують борщ у Шацьку, що борщ має абсолютно чорний колір?",
                        replyMarkup: quiz19,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizNinth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так!\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.",
                        replyMarkup: ninthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizNinth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ну майже) Ні\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.",
                        replyMarkup: ninthNext,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizNinth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так роблять багато де, але не у Шацьку.\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.",
                        replyMarkup: ninthNext,
                        cancellationToken: cancellationToken);
                }
                //10
                if (callbackData == "ninthNext")
                {
                    var task = SendPhoto(chatid, @"pic/1quiz/quiz10.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "В Україні цей продукт має статус географічно зазначеного продукту, адже може виготовлятися лише тут",
                        replyMarkup: quiz10,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizTenth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вірно!\r\nНещодавно такий сир пройшов офіційну реєстрацію й отримав «захищену назву з географічним зазначенням». " +
                        "«Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizTenth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні, не вірно.\r\nНещодавно бринза пройшла офіційну реєстрацію й отримала «захищену назву з географічним зазначенням». " +
                        "«Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizTenth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ти напевно вже зголоднів/зголодніла. Але відповідь не вірна!\r\nНещодавно бринза пройшла офіційну реєстрацію й отримала " +
                        "«захищену назву з географічним зазначенням». «Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "1quizTenth3" || callbackData == "1quizTenth2" || callbackData == "1quizTenth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Сподіваємось, що вам було цікаво перевірити свої знання та дізнатися щось нове!.\nДякуємо, що прийняли участь у вікторині.",
                        replyMarkup: toMenu,
                        cancellationToken: cancellationToken);
                }

                #endregion

                #region [Вікторина - Що ти знаєш про українську мову]
                if (callbackData == "ukrQuiz")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz0.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вітаю у нашій вікторині, давай дізнаємося, як добре ти знаєш українську мову",
                        replyMarkup: start2Menu,
                        cancellationToken: cancellationToken);
                }

                if (callbackData == "start2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz1.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Якими були перші задокументовані українські слова?\r\n ",
                        replyMarkup: quiz21,
                        cancellationToken: cancellationToken);
                }

                if (callbackData == "2quizFirst1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вірно! Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.",
                        replyMarkup: firstNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFirst2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "А ось і ні. Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.",
                        replyMarkup: firstNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFirst3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "А ось і ні. Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.",
                        replyMarkup: firstNext2,
                        cancellationToken: cancellationToken);
                }
                //2
                if (callbackData == "firstNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz2.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Кого вважають зачинателем нової української літературної мови?\r\n",
                        replyMarkup: quiz22,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSecond1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.",
                        replyMarkup: secondNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSecond2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.",
                        replyMarkup: secondNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSecond3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.",
                        replyMarkup: secondNext2,
                        cancellationToken: cancellationToken);
                }
                //3
                if (callbackData == "secondNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz3.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Перший український словник називався “Лексис з тлумаченням слов’янських слів на просту мову”. Скільки там було слів?",
                        replyMarkup: quiz23,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizThird1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році й там було 896 слів.",
                        replyMarkup: thirdNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizThird2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році й там було 896 слів.",
                        replyMarkup: thirdNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizThird3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так. Тоді їх було зовсім мало. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році.",
                        replyMarkup: thirdNext2,
                        cancellationToken: cancellationToken);
                }
                //4
                if (callbackData == "thirdNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz4.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Яка літера з українського алфавіту є найбільш вживаною?\r\n",
                        replyMarkup: quiz24,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFourth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це літра п. А ще на цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.",
                        replyMarkup: fourthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFourth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Вірно. На цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.",
                        replyMarkup: fourthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFourth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це літра п. А ще на цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.",
                        replyMarkup: fourthNext2,
                        cancellationToken: cancellationToken);
                }
                //5
                if (callbackData == "fourthNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz5.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Яка українська пісня є найстарішою?",
                        replyMarkup: quiz25,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFifth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це вірш-пісня “Дунаю, чому смутен течеш?”. Її знайшли в рукописній граматиці 1571 року.",
                        replyMarkup: fifthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFifth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це вірш-пісня “Дунаю, чому смутен течеш?”. Її знайшли в рукописній граматиці 1571 року.",
                        replyMarkup: fifthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizFifth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так! Ця українська народна пісня, вважається найстарішим відомим з літератури текстом української народної пісні. Її знайшли в рукописній граматиці 1571 року.",
                        replyMarkup: fifthNext2,
                        cancellationToken: cancellationToken);
                }
                //6
                if (callbackData == "fifthNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz6.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Яка мова є найбільш близькою до української за лексичним запасом?\r\n",
                        replyMarkup: quiz26,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSixth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. З білоруською мовою у нас аж 84%, з польською 70%, а з російською всього 62%.",
                        replyMarkup: sixthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSixth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Це було легко. З білоруською мовою у нас аж 84% спільних слів, з польською 70%, з російською всього 62%.",
                        replyMarkup: sixthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSixth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. З білоруською мовою у нас аж 84%, з польською 70%, а з російською всього 62%.",
                        replyMarkup: sixthNext2,
                        cancellationToken: cancellationToken);
                }
                //7
                if (callbackData == "sixthNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz7.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Який український твір найбільше перекладали іншими мовами?\r\n",
                        replyMarkup: quiz27,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSeventh1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Правильна відповідь - “Заповіт” Тараса Шевченка. Його переклали 147 мовами світу.",
                        replyMarkup: seventhNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSeventh2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Правильна відповідь - “Заповіт” Тараса Шевченка. Його переклали 147 мовами світу.",
                        replyMarkup: seventhNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizSeventh3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Правильно. “Заповіт” Тараса Шевченка переклали 147 мовами світу.",
                        replyMarkup: seventhNext2,
                        cancellationToken: cancellationToken);
                }
                //8
                if (callbackData == "seventhNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz8.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Скільки слів у сучасній українській мові?\r\n",
                        replyMarkup: quiz28,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizEighth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.",
                        replyMarkup: eighthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizEighth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.",
                        replyMarkup: eighthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizEighth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.",
                        replyMarkup: eighthNext2,
                        cancellationToken: cancellationToken);
                }
                //9
                if (callbackData == "eighthNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz9.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Скільки осіб у світі розмовляють українською мовою?\r\n",
                        replyMarkup: quiz29,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizNinth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.",
                        replyMarkup: ninthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizNinth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.",
                        replyMarkup: ninthNext2,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizNinth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.",
                        replyMarkup: ninthNext2,
                        cancellationToken: cancellationToken);
                }
                //10
                if (callbackData == "ninthNext2")
                {
                    var task = SendPhoto(chatid, @"pic/2quiz/quiz10.jpg", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Скільки літер у найдовшому слові в українській мові?",
                        replyMarkup: quiz20,
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizTenth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів.",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizTenth2")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Ні. Це слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizTenth3")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Так, слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів.",
                        cancellationToken: cancellationToken);
                }
                if (callbackData == "2quizTenth3" || callbackData == "2quizTenth2" || callbackData == "2quizTenth1")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Сподіваємось, що вам було цікаво перевірити свої знання та дізнатися щось нове!.\nДякуємо, що прийняли участь у вікторині.",
                        replyMarkup: toMenu,
                        cancellationToken: cancellationToken);
                }

                #endregion

                #region [Повернення в головне меню]
                if (callbackData == "toMenu")
                {
                    await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatid,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: mainMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Повернення в меню]
                if (callbackData == "toAboutMenu")
                {
                    await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatid,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: aboutMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Комп'ютерний зал]
                if (callbackData == "computer")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Всіх охочих, для створення нових проєктів, зустрічі з друзями чи просто роботи на комп'ютері чекає комп'ютерний зал бібліотеки! 💼\r\n\r\n" +
                        "У затишному залі є доступ до комп'ютерної техніки, з Інтернетом.\r\n\r\nЗал працює:\r\nПн. - Пт. з 08:00 до 15:00.\r\nВихідні дні - субота",
                        replyMarkup: backMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Контакти]
                if (callbackData == "contacts")
                {
                    var pic3 = SendPhoto(chatid, @"pic/social.jpg", cancellationToken);
                    pic3.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatid,
                    text: "Соціальні мережі: ",
                    replyMarkup: catalogMenu,
                    cancellationToken: cancellationToken
                    );
                }
                #endregion

                #region [Графік]
                if (callbackData == "schedule")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Бібліотека обслуговує читачів щодня.\nз 9:00 до 18:00.\nВихідні - субота❗️",
                        replyMarkup: backMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Адресса]
                if (callbackData == "address")
                {
                    var message = await botClient.SendVenueAsync(
                    chatId: chatid,
                    latitude: 50.91012315263593f,
                    longitude: 31.11094093170507f,
                    title: "Козелецька центральна бібліотека:",
                    address: "(міський парк) вулиця Свято Преображенська, 3, Козелець, Чернігівська область, 17000",
                    replyMarkup: backMenu,
                    cancellationToken: cancellationToken);
                }
                #endregion

                #region [Історія бібліотеки]
                if (callbackData == "history")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Козелецька центральна бібліотека заснована у жовтні 1917 року, як публічна бібліотека м. Козельця. \n" +
                        "Першочергово її фонд складали газети, матеріали різних партій, а також книги, подаровані її засновниками. \n" +
                        "Особливо гостро представниками різних партій протягом 1917-19 рр. ставилось питання про завідуючого бібліотекою. \n" +
                        "В той час завідувачі змінювалися дуже часто, адже кожна політична сила, що приходила до влади в місті, призначала завідуючим свого представника.\n\n" +
                        "Після встановлення Радянської влади, бібліотека зазнає ряд організаційних змін: вона була і міською, і повітовою, і, нарешті з 1925 р. - районною.\n" +
                        "В тридцяті роки минулого століття до заслуг діяльності бібліотеки можна віднести лише роботу по ліквідації неграмотності серед населення. \n\n" +
                        "Під час Великої Вітчизняної війни коли німецькі війська увійшли в місто гинули мирні жителі і руйнувалися надбання культури. " +
                        "І сьогодні у фонді бібліотеки знаходиться том Енциклопедії Брокгауза та Ефрона, прострілений наскрізь кулею. Після визволення міста, бібліотека відновлює свою " +
                        "роботу лише 1 березня 1947 року. Фонд бібліотеки на той час складав лише 875 примірників. Це були, в основному, книги, передані з інших бібліотек та частина книг, " +
                        "які не були знищені фашистськими загарбниками. \n\nА вже з початку 50-х років фонд бібліотеки інтенсивно поповнюється. Так, 3-го серпня 1952 року районна газета " +
                        "«Розгорнутим фронтом» писала: «За сім місяців 1952 року в Козелецьку районну бібліотеку надійшло 1099 примірників літератури».\n\n" +
                        "В серпні - на початку вересня 1958 року бібліотека переїжджає в будинок полкової канцелярії Київського полку, де вона знаходиться і до цього часу.\n\n" +
                        "Сьогодні в Козелецькій бібліотеці працює 17 фахівців, в т. ч. 11 з вищою бібліотечною освітою. До послуг користувачів бібліотеки 33 тисячі документів, " +
                        "які розміщені у відділі обслуговування з читальним залом на 20 робочих місць та відділі краєзнавчої літератури та бібліографії. " +
                        "Щороку бібліотека обслуговує понад 3 тис. користувачів, видає їм понад 70 тис. документів. \n\nБудинок полкової канцелярії в якому зараз розташовується бібліотека, " +
                        "збудований на замовлення зятя Розумовських - київського полковника Юхима Дарагана у 1756 - 1765 рр. \nЦе пам’ятка цивільної архітектури XVIII ст. одна із двох збережених " +
                        "адміністративних будівель канцелярій козацьких полків",
                        replyMarkup: backMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion
            }
            #endregion
        }

        static void Main(string[] args)
        {
            Runtime.PythonDLL = @"C:\Users\Vitalii\AppData\Local\Programs\Python\Python311\python311.dll";
            PythonEngine.Initialize(); // Ініціалізація Python
           

            var connectionString = "Data Source=debts.db";
            var dbContextOptions = new DbContextOptionsBuilder<DebtDbContext>()
                .UseSqlite(connectionString)
                .Options;

            Console.WriteLine("Бот запущений" + botClient.GetMeAsync().Result.FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            botClient.StartReceiving(
                HandleUpdateAsync,
                HandlePollingErrorAsync,
                receiverOptions,
                cancellationToken
            );
            // Завершити роботу з Python
            PythonEngine.Shutdown();

            Console.ReadLine();
        }

        public static async Task<Task> HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram Api Error: \n [{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
        public static async Task SendPhoto(long chatID, string photoPath, CancellationToken token)
        {

            using var photoStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var inputFile = new InputOnlineFile(photoStream, Path.GetFileName(photoPath));

            Message message = await botClient.SendPhotoAsync(
                chatId: chatID,
                photo: inputFile,
                parseMode: ParseMode.Html,
                cancellationToken: token);
        }
        public static async Task SendDocument(long chatID, string documentPath, CancellationToken token)
        {
            using var documentStream = new FileStream(documentPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var inputFile = new InputOnlineFile(documentStream, Path.GetFileName(documentPath));

            Message message = await botClient.SendDocumentAsync(
                chatId: chatID,
                document: inputFile,
                parseMode: ParseMode.Html,
                cancellationToken: token);
        }

        public static string GetRecommendationsFromPython(string query)
        {
            using (Py.GIL())
            {
                dynamic program = Py.Import("python/program.py");

                // Виклик функції process_query з програми Python
                dynamic result = program.process_query(query);

                return result.ToString();
            }
        }
    }
}