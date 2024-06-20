namespace TgBot_librarian.Handlers.Interfaces
{
    public interface ISendDocumentHandler
    {
        Task SendDocumentAsync(long chatID, string documentPath, CancellationToken token);
    }
}
