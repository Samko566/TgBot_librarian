using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Python.Runtime;
using Microsoft.Extensions.DependencyInjection;
using TgBot_librarian.Handlers.Interfaces;
using TgBot_librarian.Handlers;
using TgBot_librarian.DataBase;
using TgBot_librarian.Handlers.Quiz;
using TgBot_librarian.Handlers.Quiz.UkrainianCuisine;
using TgBot_librarian.Handlers.Quiz.UkrainianLanguage;
using TgBot_librarian.Handlers.Quote;
using TgBot_librarian.Configuration;
using TgBot_librarian.Resources;

namespace TgBot_librarian
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Environment.SetEnvironmentVariable("PYTHONNET_PYDLL", @"C:\Users\Vitalii\AppData\Local\Programs\Python\Python311\python311.dll");
            PythonEngine.Initialize(); // Ініціалізація Python

            var host = HostBuilderConfig.CreateHostBuilder(args).Build();

            var botClient = host.Services.GetRequiredService<ITelegramBotClient>();
            var sendPhotoHandler = host.Services.GetRequiredService<ISendPhotoHandler>();
            var sendDocumentHandler = host.Services.GetRequiredService<ISendDocumentHandler>();
            var quizHandler = host.Services.GetRequiredService<QuizHandler>();
            var recommendationHandler = host.Services.GetRequiredService<RecommendationHandler>();
            var debtHandler = host.Services.GetRequiredService<DebtHandler>();
            var quoteHandler = host.Services.GetRequiredService<QuoteHandler>();

            var connectionString = "Data Source=debts.db";
            var dbContextOptions = new DbContextOptionsBuilder<DebtDbContext>()
                .UseSqlite(connectionString)
                .Options;

            Console.WriteLine("Бот запущений " + (await botClient.GetMeAsync()).FirstName);
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };

            botClient.StartReceiving(
                async (botClient, update, token) => await HandleUpdateAsync(botClient, update, token, sendPhotoHandler,
                sendDocumentHandler, quizHandler, recommendationHandler,
                debtHandler, quoteHandler, host.Services),
                PollingErrorHandler.HandlePollingErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
            PythonEngine.Shutdown(); // Завершити роботу з Python при завершенні програми
        }

        public static async Task HandleUpdateAsync(
            ITelegramBotClient botClient, 
            Update update, 
            CancellationToken cancellationToken, 
            ISendPhotoHandler sendPhotoHandler, 
            ISendDocumentHandler sendDocumentHandler, 
            QuizHandler quizHandler,
            RecommendationHandler recommendationHandler, 
            DebtHandler debtHandler, 
            QuoteHandler quoteHandler,
            IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DebtDbContext>();

            if (update.Type == UpdateType.Message)
            {
                var chatid = update.Message.Chat.Id;
                var messageText = update.Message.Text;
                string firstName = update.Message.From.FirstName;
                string secondName = update.Message.From.LastName;

                #region [Рекомендація книг]
                await recommendationHandler.HandleMessageAsync(update.Message, cancellationToken);
                #endregion

                #region [Перше повідомлення]
                if (messageText == "/start")
                {
                    var task = sendPhotoHandler.SendPhotoAsync(chatid, @"Resources/Images/libphoto.jpg", cancellationToken);
                    task.Wait();
                    Message sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: $"Привіт {firstName} {secondName}! \nЯ чат бот-бібліотекар, що б ви хотіли дізнатись?",
                        replyMarkup: InlineKeyboards.mainMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Боржники]
                await debtHandler.HandleMessageAsync(update.Message, cancellationToken);
                #endregion
            }

            #region [Обробники кнопок]
            if (update.Type == UpdateType.CallbackQuery)
            {
                var chatid = update.CallbackQuery.Message.Chat.Id;
                var callbackData = update.CallbackQuery.Data;

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
                        replyMarkup: InlineKeyboards.aboutMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Word файл]
                if (callbackData == "file")
                {
                    var task = sendDocumentHandler.SendDocumentAsync(chatid, @"Resources/Files/Каталог.docx", cancellationToken);
                    task.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Word файл зі списком наявних книг бібліотеки",
                        replyMarkup: InlineKeyboards.toMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Неповернені книги]
                if (callbackData == "debt")
                {
                    await debtHandler.HandleCallbackQueryAsync(update.CallbackQuery, cancellationToken);
                }
                #endregion

                #region [Каталог книг]
                if (callbackData == "catalog")
                {
                    var pic2 = sendPhotoHandler.SendPhotoAsync(chatid, @"Resources/Images/shelves.jpg", cancellationToken);
                    pic2.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "В цьому розділі ви можете отримати інформацію про те які книги фізично наявні в бібліотеці. \nФункція прочитати книгу онлайн не передбачена." +
                        "\n\nОберіть, що саме стосовно асортименту ви б хотіли переглянути:",
                        replyMarkup: InlineKeyboards.socialMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Цитата дня]
                if (callbackData == "quote")
                {
                    await quoteHandler.HandleQuoteAsync(update.CallbackQuery, cancellationToken);
                }
                #endregion

                #region [Вікторини]
                if (callbackData == "quiz")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: "Оберіть вікторину 👇:",
                        replyMarkup: InlineKeyboards.quizMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Вікторина - Українська кухня]
                if (callbackData.StartsWith("cookQuiz") || callbackData.StartsWith("start1") || callbackData.StartsWith("1quiz") || callbackData.EndsWith("Next1"))
                {
                    await quizHandler.HandleCallbackQueryAsync(update.CallbackQuery, UkrainianCuisineDictionary.QuizSteps, cancellationToken);
                }
                #endregion

                #region [Вікторина - Що ти знаєш про українську мову]
                if (callbackData.StartsWith("ukrQuiz") || callbackData.StartsWith("start2") || callbackData.StartsWith("2quiz") || callbackData.EndsWith("Next2"))
                {
                    await quizHandler.HandleCallbackQueryAsync(update.CallbackQuery, UkrainianLanguageDictionary.QuizSteps, cancellationToken);
                }
                #endregion

                #region [Повернення в головне меню]
                if (callbackData == "toMenu")
                {
                    await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatid,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: InlineKeyboards.mainMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Повернення в меню]
                if (callbackData == "toAboutMenu")
                {
                    await botClient.EditMessageReplyMarkupAsync(
                        chatId: chatid,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: InlineKeyboards.aboutMenu,
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
                        replyMarkup: InlineKeyboards.backMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion

                #region [Контакти]
                if (callbackData == "contacts")
                {
                    var pic3 = sendPhotoHandler.SendPhotoAsync(chatid, @"Resources/Images/social.jpg", cancellationToken);
                    pic3.Wait();
                    var sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatid,
                    text: "Соціальні мережі:\n\n" +
                    "Пошта бібліотеки: libkozelets@gmail.com",
                    replyMarkup: InlineKeyboards.catalogMenu,
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
                        replyMarkup: InlineKeyboards.backMenu,
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
                    replyMarkup: InlineKeyboards.backMenu,
                    cancellationToken: cancellationToken);
                }
                #endregion

                #region [Історія бібліотеки]
                if (callbackData == "history")
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId: chatid,
                        text: LibraryHistoryTexts.HistoryText,
                        replyMarkup: InlineKeyboards.backMenu,
                        cancellationToken: cancellationToken);
                }
                #endregion
            }
            #endregion
        }
    }
}
