using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Enums;
using TgBot_librarian.Handlers.Interfaces;

namespace TgBot_librarian.Handlers.Send
{
    public class SendDocumentHandler : ISendDocumentHandler
    {
        private readonly ITelegramBotClient _botClient;

        public SendDocumentHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendDocumentAsync(long chatID, string documentPath, CancellationToken token)
        {
            using var documentStream = new FileStream(documentPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var inputFile = new InputOnlineFile(documentStream, Path.GetFileName(documentPath));

            Message message = await _botClient.SendDocumentAsync(
                chatId: chatID,
                document: inputFile,
                parseMode: ParseMode.Html,
                cancellationToken: token);
        }
    }
}