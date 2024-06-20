using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace TgBot_librarian.Handlers
{
    public class PollingErrorHandler
    {
        public static async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram Api Error: \n [{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            await Task.CompletedTask;
        }
    }
}