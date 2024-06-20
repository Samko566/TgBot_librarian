namespace TgBot_librarian.Handlers.Interfaces
{
    public interface ISendPhotoHandler
    {
        Task SendPhotoAsync(long chatID, string photoPath, CancellationToken token);
    }
}
