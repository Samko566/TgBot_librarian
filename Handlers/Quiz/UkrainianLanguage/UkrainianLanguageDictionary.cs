using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot_librarian.Handlers.Quiz.UkrainianLanguage
{
    public static class UkrainianLanguageDictionary
    {
        public static Dictionary<string, (string? PhotoPath, string Question, InlineKeyboardMarkup Answers)> QuizSteps =
            new Dictionary<string, (string?, string, InlineKeyboardMarkup)>
        {
            { "ukrQuiz", ("Resources/Images/2quiz/quiz0.jpg", "Вітаю у нашій вікторині, давай дізнаємося, як добре ти знаєш українську мову", UkrainianLanguageKeyboards.start2Menu) },
            { "start2", ("Resources/Images/2quiz/quiz1.jpg", "Якими були перші задокументовані українські слова?", UkrainianLanguageKeyboards.quiz21) },
            { "2quizFirst1", (null, "Вірно! Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.", UkrainianLanguageKeyboards.firstNext2) },
            { "2quizFirst2", (null, "А ось і ні. Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.", UkrainianLanguageKeyboards.firstNext2) },
            { "2quizFirst3", (null, "А ось і ні. Слова мед та страва були записані ще у 448 році істориком з Візантії Пріском Панікійським.", UkrainianLanguageKeyboards.firstNext2) },
            { "firstNext2", ("Resources/Images/2quiz/quiz2.jpg", "Кого вважають зачинателем нової української літературної мови?", UkrainianLanguageKeyboards.quiz22) },
            { "2quizSecond1", (null, "Так. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.", UkrainianLanguageKeyboards.secondNext2) },
            { "2quizSecond2", (null, "Ні. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.", UkrainianLanguageKeyboards.secondNext2) },
            { "2quizSecond3", (null, "Ні. Українську мову вперше прирівняли до рівня літературної після виходу “Енеїди” Котляревського.", UkrainianLanguageKeyboards.secondNext2) },
            { "secondNext2", ("Resources/Images/2quiz/quiz3.jpg", "Перший український словник називався “Лексис з тлумаченням слов’янських слів на просту мову”. Скільки там було слів?", UkrainianLanguageKeyboards.quiz23) },
            { "2quizThird1", (null, "Ні. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році й там було 896 слів.", UkrainianLanguageKeyboards.thirdNext2) },
            { "2quizThird2", (null, "Ні. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році й там було 896 слів.", UkrainianLanguageKeyboards.thirdNext2) },
            { "2quizThird3", (null, "Так. Тоді їх було зовсім мало. Словник був складений невідомим автором, який підшив його до “Острозької Біблії”, яка вийшла у 1581 році.", UkrainianLanguageKeyboards.thirdNext2) },
            { "thirdNext2", ("Resources/Images/2quiz/quiz4.jpg", "Яка літера з українського алфавіту є найбільш вживаною?", UkrainianLanguageKeyboards.quiz24) },
            { "2quizFourth1", (null, "Ні. Це літра п. А ще на цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.", UkrainianLanguageKeyboards.fourthNext2) },
            { "2quizFourth2", (null, "Вірно. На цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.", UkrainianLanguageKeyboards.fourthNext2) },
            { "2quizFourth3", (null, "Ні. Це літра п. А ще на цю літеру починається найбільше слів. А якщо вам цікаво, то найменш вживана літера ‒ ф.", UkrainianLanguageKeyboards.fourthNext2) },
            { "fourthNext2", ("Resources/Images/2quiz/quiz5.jpg", "Яка українська пісня є найстарішою?", UkrainianLanguageKeyboards.quiz25) },
            { "2quizFifth1", (null, "Ні. Це вірш-пісня “Дунаю, чому смутен течеш?”. Її знайшли в рукописній граматиці 1571 року.", UkrainianLanguageKeyboards.fifthNext2) },
            { "2quizFifth2", (null, "Ні. Це вірш-пісня “Дунаю, чому смутен течеш?”. Її знайшли в рукописній граматиці 1571 року.", UkrainianLanguageKeyboards.fifthNext2) },
            { "2quizFifth3", (null, "Так! Ця українська народна пісня, вважається найстарішим відомим з літератури текстом української народної пісні. Її знайшли в рукописній граматиці 1571 року.", UkrainianLanguageKeyboards.fifthNext2) },
            { "fifthNext2", ("Resources/Images/2quiz/quiz6.jpg", "Яка мова є найбільш близькою до української за лексичним запасом?", UkrainianLanguageKeyboards.quiz26) },
            { "2quizSixth1", (null, "Ні. З білоруською мовою у нас аж 84%, з польською 70%, а з російською всього 62%.", UkrainianLanguageKeyboards.sixthNext2) },
            { "2quizSixth2", (null, "Це було легко. З білоруською мовою у нас аж 84% спільних слів, з польською 70%, з російською всього 62%.", UkrainianLanguageKeyboards.sixthNext2) },
            { "2quizSixth3", (null, "Ні. З білоруською мовою у нас аж 84%, з польською 70%, а з російською всього 62%.", UkrainianLanguageKeyboards.sixthNext2) },
            { "sixthNext2", ("Resources/Images/2quiz/quiz7.jpg", "Який український твір найбільше перекладали іншими мовами?", UkrainianLanguageKeyboards.quiz27) },
            { "2quizSeventh1", (null, "Ні. Правильна відповідь - “Заповіт” Тараса Шевченка. Його переклали 147 мовами світу.", UkrainianLanguageKeyboards.seventhNext2) },
            { "2quizSeventh2", (null, "Ні. Правильна відповідь - “Заповіт” Тараса Шевченка. Його переклали 147 мовами світу.", UkrainianLanguageKeyboards.seventhNext2) },
            { "2quizSeventh3", (null, "Правильно. “Заповіт” Тараса Шевченка переклали 147 мовами світу.", UkrainianLanguageKeyboards.seventhNext2) },
            { "seventhNext2", ("Resources/Images/2quiz/quiz8.jpg", "Скільки слів у сучасній українській мові?", UkrainianLanguageKeyboards.quiz28) },
            { "2quizEighth1", (null, "Так. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.", UkrainianLanguageKeyboards.eighthNext2) },
            { "2quizEighth2", (null, "Ні. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.", UkrainianLanguageKeyboards.eighthNext2) },
            { "2quizEighth3", (null, "Ні. Приблизно 256 000 слів. Про це свідчить словник Національної Академії Наук України.", UkrainianLanguageKeyboards.eighthNext2) },
            { "eighthNext2", ("Resources/Images/2quiz/quiz9.jpg", "Скільки осіб у світі розмовляють українською мовою?", UkrainianLanguageKeyboards.quiz29) },
            { "2quizNinth1", (null, "Ні. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.", UkrainianLanguageKeyboards.ninthNext2) },
            { "2quizNinth2", (null, "Так. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.", UkrainianLanguageKeyboards.ninthNext2) },
            { "2quizNinth3", (null, "Ні. За різними оцінками загалом у світі українською мовою говорить від 41 до 45 млн осіб, вона входить до другого десятка найпоширеніших мов світу.", UkrainianLanguageKeyboards.ninthNext2) },
            { "ninthNext2", ("Resources/Images/2quiz/quiz10.jpg", "Скільки літер у найдовшому слові в українській мові?", UkrainianLanguageKeyboards.quiz20) },
            { "2quizTenth1", (null, "Ні. Це слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів.", null) },
            { "2quizTenth2", (null, "Ні. Це слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів", null) },
            { "2quizTenth3", (null, "Так, слово з 31 літери — “рентгеноелектрокардіографічного”. І воно внесене до Книги рекордів.", null) },
        };
    }
}