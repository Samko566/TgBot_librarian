using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.Enums;
using TgBot_librarian.Handlers.Interfaces;

namespace TgBot_librarian.Handlers.Send
{
    public class SendPhotoHandler : ISendPhotoHandler
    {
        private readonly ITelegramBotClient _botClient;

        public SendPhotoHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task SendPhotoAsync(long chatID, string photoPath, CancellationToken token)
        {
            using var photoStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var inputFile = new InputOnlineFile(photoStream, Path.GetFileName(photoPath));

            Message message = await _botClient.SendPhotoAsync(
                chatId: chatID,
                photo: inputFile,
                parseMode: ParseMode.Html,
                cancellationToken: token);
        }
    }
}