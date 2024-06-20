using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot_librarian.Handlers.Quiz.UkrainianCuisine
{
    public static class UkrainianCuisineKeyboards
    {
        #region [Вікторина - Українська кухня]
        public static InlineKeyboardMarkup start1Menu = new InlineKeyboardMarkup(
        new[] { new[] { InlineKeyboardButton.WithCallbackData("Почати вікторину", "start1") }, });

        public static InlineKeyboardMarkup quiz11 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Святий Вечір (вечір напередодні Різдва)", callbackData: "1quizFirst1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Весілля", callbackData: "1quizFirst2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Сватання", callbackData: "1quizFirst3") }});

        public static InlineKeyboardMarkup firstNext1 = new InlineKeyboardMarkup(
    new[] { new[] { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "firstNext1") }, });

        public static InlineKeyboardMarkup quiz12 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "100", callbackData: "1quizSecond1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "200", callbackData: "1quizSecond2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Понад 300", callbackData: "1quizSecond3") }});

        public static InlineKeyboardMarkup secondNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "secondNext1") }, });

        public static InlineKeyboardMarkup quiz13 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Хрін, кріп, кмин, м'ята, аніс", callbackData: "1quizThird1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Кориця, ваніль, чорний перець, кмин", callbackData: "1quizThird2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Хмелі-сунелі, паприка, імбир, мускатний горіх", callbackData: "1quizThird3") }});

        public static InlineKeyboardMarkup thirdNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "thirdNext1") }, });

        public static InlineKeyboardMarkup quiz14 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Селище біля Молдови", callbackData: "1quizFourth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Маленькі бендеревці", callbackData: "1quizFourth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Млинці з м'ясом", callbackData: "1quizFourth3") }});

        public static InlineKeyboardMarkup fourthNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fourthNext1") }, });

        public static InlineKeyboardMarkup quiz15 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Весілля", callbackData: "1quizFifth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "День народження", callbackData: "1quizFifth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Бейбі Шауер", callbackData: "1quizFifth3") }});

        public static InlineKeyboardMarkup fifthNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "fifthNext1") }, });

        public static InlineKeyboardMarkup quiz16 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Чорнослив", callbackData: "1quizSixth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Корінь селери", callbackData: "1quizSixth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Білий буряк", callbackData: "1quizSixth3") }});

        public static InlineKeyboardMarkup sixthNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "sixthNext1") }, });

        public static InlineKeyboardMarkup quiz17 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Борщ та каша - сила наша\"", callbackData: "1quizSeventh1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Найкращій букет - то шматок телятини\"", callbackData: "1quizSeventh2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "\"Голод не знає сорому\"", callbackData: "1quizSeventh3") }});

        public static InlineKeyboardMarkup seventhNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "seventhNext1") }, });

        public static InlineKeyboardMarkup quiz18 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Відкритий пиріг з будь-якою начинкою, приготований у печі", callbackData: "1quizEighth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Друга страва з м'яса та картоплі", callbackData: "1quizEighth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Будь-яка страва з печінки", callbackData: "1quizEighth3") }});

        public static InlineKeyboardMarkup eighthNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "eighthNext1") }, });

        public static InlineKeyboardMarkup quiz19 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "З крові диких тварин, наприклад, кабана", callbackData: "1quizNinth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "З бурякового квасу", callbackData: "1quizNinth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "З бульйону на ребрах", callbackData: "1quizNinth3") }});

        public static InlineKeyboardMarkup ninthNext1 = new InlineKeyboardMarkup(new[] { new[]
              { InlineKeyboardButton.WithCallbackData(text: "Наступне питання 👉", callbackData: "ninthNext1") }, });

        public static InlineKeyboardMarkup quiz10 = new InlineKeyboardMarkup(new[]{
        new[] { InlineKeyboardButton.WithCallbackData(text: "Гуцульська овеча бринза", callbackData: "1quizTenth1") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Галушки", callbackData: "1quizTenth2") },
        new[] { InlineKeyboardButton.WithCallbackData(text: "Свинячі ребра", callbackData: "1quizTenth3") }});
        #endregion
    }
}
