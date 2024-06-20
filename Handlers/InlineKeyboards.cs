using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot_librarian.Handlers
{
    public static class InlineKeyboards
    {
        #region [Кнопки]
        public static InlineKeyboardMarkup mainMenu = new InlineKeyboardMarkup(new[]{
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Про бібліотеку 📖", callbackData: "about") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Каталог книг 📚", callbackData: "catalog") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Підібрати книгу за описом 🤖", callbackData: "ai") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Цитата дня 💪🇺🇦", callbackData: "quote") },
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Вікторини🏆", callbackData: "quiz") }});

        public static InlineKeyboardMarkup aboutMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Соціальні мережі та контакти 📱", callbackData: "contacts")},
        new[]{ InlineKeyboardButton.WithCallbackData(text: "Комп'ютерний зал 💻", callbackData: "computer") },
        new[]{InlineKeyboardButton.WithCallbackData(text: "Адресса бібліотеки 🗺", callbackData: "address"),
              InlineKeyboardButton.WithCallbackData(text: "Графік роботи 🕐", callbackData: "schedule")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Історія бібліотеки - багато тексту 🏠", callbackData: "history")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu")},});

        public static InlineKeyboardMarkup socialMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Word файл зі списком наявної літератури", callbackData: "file")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Боржники/Неповернені книги", callbackData: "debt")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu")},});

        public static InlineKeyboardMarkup catalogMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithUrl(text: "Facebook", url: "https://www.facebook.com/BibliotekaKozelets") },
        new[]{InlineKeyboardButton.WithUrl(text: "Сайт бібліотеки", url: "http://www.kozelets-cbs.edukit.cn.ua/")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Повернутися в меню ↩️", callbackData: "toAboutMenu") },});

        public static InlineKeyboardMarkup quizMenu = new InlineKeyboardMarkup(new[]{
        new[]{InlineKeyboardButton.WithCallbackData(text: "Вікторина - Українська кухня", callbackData: "cookQuiz")},
        new[]{InlineKeyboardButton.WithCallbackData(text: "Вікторина - Що ти знаєш про українську мову", callbackData: "ukrQuiz")}, });

        public static InlineKeyboardMarkup backMenu = new InlineKeyboardMarkup(
    new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Повернутися в меню ↩️", callbackData: "toAboutMenu") }, });
        public static InlineKeyboardMarkup toMenu = new InlineKeyboardMarkup(
    new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Повернутися в головне меню ↩️", callbackData: "toMenu") }, });
        #endregion
    }
}
