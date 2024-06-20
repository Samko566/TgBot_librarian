using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TgBot_librarian.Handlers.Send;
using TgBot_librarian.Services;
using TgBot_librarian.DataBase;
using TgBot_librarian.Services.Interfaces;
using TgBot_librarian.Handlers.Quiz;
using TgBot_librarian.Handlers.Quote;
using TgBot_librarian.Handlers;
using TgBot_librarian.Handlers.Interfaces;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace TgBot_librarian.Configuration
{
    public static class HostBuilderConfig
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("5953908796:AAEkg6yZDvqSb5u0Ik_Oj3kv8xsFMjoIDgQ"));
                    services.AddTransient<ISendPhotoHandler, SendPhotoHandler>();
                    services.AddTransient<ISendDocumentHandler, SendDocumentHandler>();
                    services.AddTransient<IRecommendationService, RecommendationService>();
                    services.AddTransient<QuizHandler>();
                    services.AddTransient<RecommendationHandler>();
                    services.AddTransient<DebtHandler>();
                    services.AddTransient<QuoteHandler>();
                    services.AddDbContext<DebtDbContext>(options => options.UseSqlite("Data Source=debts.db"));
                    services.AddScoped<DebtService>();
                });
    }
}