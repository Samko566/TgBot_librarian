using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBot_librarian.Handlers.Interfaces;

namespace TgBot_librarian.Handlers.Quiz
{
    public class QuizHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ISendPhotoHandler _sendPhotoHandler;

        public QuizHandler(ITelegramBotClient botClient, ISendPhotoHandler sendPhotoHandler)
        {
            _botClient = botClient;
            _sendPhotoHandler = sendPhotoHandler;
        }

        private async Task SendPhotoAsync(long chatId, string photoPath, string caption, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken)
        {
            await _sendPhotoHandler.SendPhotoAsync(chatId, photoPath, cancellationToken);
            await _botClient.SendTextMessageAsync(chatId, caption, replyMarkup: replyMarkup, cancellationToken: cancellationToken);
        }

        private async Task SendTextAsync(long chatId, string text, InlineKeyboardMarkup replyMarkup, CancellationToken cancellationToken)
        {
            await _botClient.SendTextMessageAsync(chatId, text, replyMarkup: replyMarkup, cancellationToken: cancellationToken);
        }

        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, Dictionary<string, (string PhotoPath, string Question, InlineKeyboardMarkup Answers)> quizSteps, CancellationToken cancellationToken)
        {
            var callbackData = callbackQuery.Data;
            var chatId = callbackQuery.Message.Chat.Id;

            if (quizSteps.ContainsKey(callbackData))
            {
                var (photoPath, question, answers) = quizSteps[callbackData];

                if (photoPath != null)
                {
                    await SendPhotoAsync(chatId, photoPath, question, answers, cancellationToken);
                }
                else
                {
                    await SendTextAsync(chatId, question, answers, cancellationToken);
                }
                // Перевірка останнього питання
                if (callbackData.EndsWith("Tenth1") || callbackData.EndsWith("Tenth2") || callbackData.EndsWith("Tenth3"))
                {
                    await SendTextAsync(chatId, "Сподіваємось, що вам було цікаво перевірити свої знання та дізнатися щось нове!.\nДякуємо, що прийняли участь у вікторині.", InlineKeyboards.toMenu, cancellationToken);
                }
            }
        }
    }
}