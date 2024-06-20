using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot_librarian.Handlers.Quiz.UkrainianLanguage
{
    public class UkrainianLanguageKeyboards
    {
        #region [Вікторина - Що ти знаєш про українську мову]
        public static InlineKeyboardMarkup start2Menu = new InlineKeyboardMarkup(
    new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Почати вікторину", callbackData: "start2") }, });

        public static InlineKeyboardMarkup quiz21 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Мед та страва", callbackData: "2quizFirst1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Козак і Січ", callbackData: "2quizFirst2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Борщ та вареники", callbackData: "2quizFirst3") }});

        public static InlineKeyboardMarkup firstNext2 = new InlineKeyboardMarkup(
    new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "firstNext2") }, });

        public static InlineKeyboardMarkup quiz22 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Івана Котляревського", callbackData: "2quizSecond1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Тараса Шевченка", callbackData: "2quizSecond2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Івана Франка", callbackData: "2quizSecond3") }});

        public static InlineKeyboardMarkup secondNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "secondNext2") }, });

        public static InlineKeyboardMarkup quiz23 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Понад 2000 слів", callbackData: "2quizThird1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "15 000 слів", callbackData: "2quizThird2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "896 слів", callbackData: "2quizThird3") }});

        public static InlineKeyboardMarkup thirdNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "thirdNext2") }, });

        public static InlineKeyboardMarkup quiz24 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Ф", callbackData: "2quizFourth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "П", callbackData: "2quizFourth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "К", callbackData: "2quizFourth3") }});

        public static InlineKeyboardMarkup fourthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fourthNext2") }, });

        public static InlineKeyboardMarkup quiz25 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Несе Галя воду”", callbackData: "2quizFifth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"При долині кущ калини\"", callbackData: "2quizFifth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Дунаю, чому смутен течеш?”", callbackData: "2quizFifth3") }});

        public static InlineKeyboardMarkup fifthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fifthNext2") }, });

        public static InlineKeyboardMarkup quiz26 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Польська", callbackData: "2quizSixth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Білоруська", callbackData: "2quizSixth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Російська", callbackData: "2quizSixth3") }});

        public static InlineKeyboardMarkup sixthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "sixthNext2") }, });

        public static InlineKeyboardMarkup quiz27 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Енеїда” Івана Котляревського", callbackData: "2quizSeventh1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Лісова пісня” Лесі Українки", callbackData: "2quizSeventh2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "“Заповіт” Тараса Шевченка", callbackData: "2quizSeventh3") }});

        public static InlineKeyboardMarkup seventhNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "seventhNext2") }, });

        public static InlineKeyboardMarkup quiz28 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Близько 256 000", callbackData: "2quizEighth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Десь 153 000", callbackData: "2quizEighth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Приблизно 580 000", callbackData: "2quizEighth3") }});

        public static InlineKeyboardMarkup eighthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "eighthNext2") }, });

        public static InlineKeyboardMarkup quiz29 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "20 млн", callbackData: "2quizNinth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "41 - 45 млн", callbackData: "2quizNinth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "55 млн", callbackData: "2quizNinth3") }});

        public static InlineKeyboardMarkup ninthNext2 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "ninthNext2") }, });

        public static InlineKeyboardMarkup quiz20 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "19", callbackData: "2quizTenth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "26", callbackData: "2quizTenth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "31", callbackData: "2quizTenth3") }});
        #endregion
    }
}
