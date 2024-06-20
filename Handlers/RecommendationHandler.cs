using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot_librarian.Services.Interfaces;
using Python.Runtime;

namespace TgBot_librarian.Handlers
{
    public class RecommendationHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IRecommendationService _recommendationService;
        private static bool _isWaitingForQuery = false;

        public RecommendationHandler(ITelegramBotClient botClient, IRecommendationService recommendationService)
        {
            _botClient = botClient;
            _recommendationService = recommendationService;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            var messageText = message.Text;

            if (messageText.StartsWith("/ai"))
            {
                _isWaitingForQuery = true;
                await _botClient.SendTextMessageAsync(chatId, "Будь ласка, введіть запит.", cancellationToken: cancellationToken);
            }
            else if (_isWaitingForQuery)
            {
                _isWaitingForQuery = false;
                string result;
                try
                {
                    await _botClient.SendTextMessageAsync(chatId, "Обробка запиту...", cancellationToken: cancellationToken);

                    using (Py.GIL())
                    {
                        result = _recommendationService.GetRecommendationsFromPython(messageText);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetRecommendationsFromPython: {ex.Message}");
                    result = "Помилка при отриманні рекомендацій.";
                }

                await _botClient.SendTextMessageAsync(chatId, result, cancellationToken: cancellationToken);
            }
        }
    }
}