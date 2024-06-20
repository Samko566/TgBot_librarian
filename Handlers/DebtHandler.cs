using Telegram.Bot;
using Telegram.Bot.Types;
using TgBot_librarian.Services;

namespace TgBot_librarian.Handlers
{
    public class DebtHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly DebtService _debtService;

        public DebtHandler(ITelegramBotClient botClient, DebtService debtService)
        {
            _botClient = botClient;
            _debtService = debtService;
        }

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;
            var messageText = message.Text;

            if (chatId == -1001611949669)
            {
                if (messageText == "/debt")
                {
                    var text = _debtService.GetDebtText();
                    await _botClient.SendTextMessageAsync(chatId, text, cancellationToken: cancellationToken);
                }
                else if (messageText.StartsWith("/save"))
                {
                    var lines = messageText.Split('\n', StringSplitOptions.RemoveEmptyEntries)
                                          .Select(line => line.Trim())
                                          .ToList();
                    if (lines.Any())
                    {// якщо користувач надіслав багато рядків, то зберігаємо їх
                        var text = string.Join(Environment.NewLine, lines);
                        _debtService.UpdateDebtText(text);

                    }
                    else
                    {// якщо користувач надіслав один рядок, то зберігаємо його
                        var newText = messageText.Replace("/save ", "");
                        _debtService.UpdateDebtText(newText);
                    }
                    await _botClient.SendTextMessageAsync(chatId, "Список боржників оновлено.", cancellationToken: cancellationToken);
                }
            }
        }
        public async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            var chatId = callbackQuery.Message.Chat.Id;
            var callbackData = callbackQuery.Data;

            if (callbackData == "debt")
            {
                var debtText = _debtService.GetDebtText();
                var sentMessage = await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: debtText,
                    cancellationToken: cancellationToken);
            }
        }
    }
}
