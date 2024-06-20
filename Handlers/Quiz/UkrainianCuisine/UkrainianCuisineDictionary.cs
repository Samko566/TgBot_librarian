using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace TgBot_librarian.Handlers.Quiz.UkrainianCuisine
{
    public static class UkrainianCuisineDictionary
    {
        public static Dictionary<string, (string? PhotoPath, string Question, InlineKeyboardMarkup Answers)> QuizSteps =
            new Dictionary<string, (string?, string, InlineKeyboardMarkup)>
        {
            { "cookQuiz", ("Resources/Images/1quiz/quiz0.jpg", "Вітаю вас у нашій вікторині що присвячена українській кухні!", UkrainianCuisineKeyboards.start1Menu) },
            { "start1", ("Resources/Images/1quiz/quiz1.jpg", "До якого свята українці готують такий стіл, як на фото?", UkrainianCuisineKeyboards.quiz11) },
            { "1quizFirst1", (null, "Правильно 👍 \r\nПереддень Різдва — Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя", UkrainianCuisineKeyboards.firstNext1) },
            { "1quizFirst2", (null, "Невірно, \r\nТакий стіл готували на Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя", UkrainianCuisineKeyboards.firstNext1) },
            { "1quizFirst3", (null, "Ну майже але невірно, \r\nТакий стіл готували на Святий вечір — на стіл слід було подати аж 12 страв, обов'язковою серед яких була кутя", UkrainianCuisineKeyboards.firstNext1) },
            { "firstNext1", ("Resources/Images/1quiz/quiz2.jpg", "Скільки років такій традиційній страві як український борщ?", UkrainianCuisineKeyboards.quiz12) },
            { "1quizSecond1", (null, "Нуу...ні, трішки більше\r\n\r\n Українському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.", UkrainianCuisineKeyboards.secondNext1) },
            { "1quizSecond2", (null, "Нуу...ні, трішки більше\r\n\r\nУкраїнському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.", UkrainianCuisineKeyboards.secondNext1) },
            { "1quizSecond3", (null, "Це правильна відповідь.\r\n\r\nУкраїнському борщу вже понад 300 років. Тобто, традиційну українську страву почали готувати раніше, ніж була створена російська імперія.", UkrainianCuisineKeyboards.secondNext1) },
            { "secondNext1", ("Resources/Images/1quiz/quiz3.jpg", "Обери вірний перелік традиційних українських спецій.", UkrainianCuisineKeyboards.quiz13) },
            { "1quizThird1", (null, "Це правильна відповідь. Ти молодець!", UkrainianCuisineKeyboards.thirdNext1) },
            { "1quizThird2", (null, "Клацав навмання? Ні", UkrainianCuisineKeyboards.thirdNext1) },
            { "1quizThird3", (null, "Хмелі-сунелі - це найпопулярніша кавказька приправа... далі можна не продовжувати", UkrainianCuisineKeyboards.thirdNext1) },
            { "thirdNext1", ("Resources/Images/1quiz/quiz4.jpg", "Бендерики - це...", UkrainianCuisineKeyboards.quiz14) },
            { "1quizFourth1", (null, "Тобі навіть картинка не допомогла ? Ні.\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.", UkrainianCuisineKeyboards.fourthNext1) },
            { "1quizFourth2", (null, "Серйозно? У нас кулінарний тест...\r\n\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.", UkrainianCuisineKeyboards.fourthNext1) },
            { "1quizFourth3", (null, "Вірно! А ти розбираєшся в їжі!\r\nБенедерики - це трикутні млинці з начинкою, обсмажені на маслі або на олії.", UkrainianCuisineKeyboards.fourthNext1) },
            { "fourthNext1", ("Resources/Images/1quiz/quiz5.jpg", "Атрибутом якого свята є каравай?", UkrainianCuisineKeyboards.quiz15) },
            { "1quizFifth1", (null, "Так, ти молодець!\r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят.", UkrainianCuisineKeyboards.fifthNext1) },
            { "1quizFifth2", (null, "Ні, там інші атрибути\r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят.", UkrainianCuisineKeyboards.fifthNext1) },
            { "1quizFifth3", (null, "Ні, \r\nКоровай – невід’ємний атрибут традиційного українського весілля. Чим він більший, тим більше щастя і багатства буде в молодят.", UkrainianCuisineKeyboards.fifthNext1) },
            { "fifthNext1", ("Resources/Images/1quiz/quiz6.jpg", "В гуцульському борщі має бути", UkrainianCuisineKeyboards.quiz16) },
            { "1quizSixth1", (null, "Ні. В деяких регіонах дійсно є борщ з чорносливом, але не в гуцульському \r\nДля гуцульского борщу використовують два види буряку - звичайний і кормовий (білий).", UkrainianCuisineKeyboards.sixthNext1) },
            { "1quizSixth2", (null, "Ні \r\nДля гуцульского борщу використовують два види буряку - звичайний і кормовий (білий). Червоний буряк дає червоний колір, а кормовий - робить страву солодкуватою.", UkrainianCuisineKeyboards.sixthNext1) },
            { "1quizSixth3", (null, "Правильно! Для гуцульского борщу використовують два види буряку - звичайний і кормовий (білий). Червоний буряк дає червоний колір, а кормовий - робить страву солодкуватою.", UkrainianCuisineKeyboards.sixthNext1) },
            { "sixthNext1", ("Resources/Images/1quiz/quiz7.jpg", "Відомий вислів Тараса Шевченка про їжу звучить так:\r\n", UkrainianCuisineKeyboards.quiz17) },
            { "1quizSeventh1", (null, "То звичайно сила, але Шеченко так не говорив.\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні в своїй автобіографічній повісті «Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.", UkrainianCuisineKeyboards.seventhNext1) },
            { "1quizSeventh2", (null, "Ні. Зовсім мимо.\r\n\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні в своїй автобіографічній повісті «Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.", UkrainianCuisineKeyboards.seventhNext1) },
            { "1quizSeventh3", (null, "ТАК!\r\n«Голод не знає сорому», – напише Тарас Шевченко на засланні в своїй автобіографічній повісті «Музикант» і стає очевидним, що спогади дитинства, а потім – студентської молодості, пов’язані у Тараса з постійним недоїданням.", UkrainianCuisineKeyboards.seventhNext1) },
            { "seventhNext1", ("Resources/Images/1quiz/quiz8.jpg", "Печеня - це...", UkrainianCuisineKeyboards.quiz18) },
            { "1quizEighth1", (null, "Як смачно, але ні..\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.", UkrainianCuisineKeyboards.eighthNext1) },
            { "1quizEighth2", (null, "Вірно. Мабудь, ти з їжею на \"ти\"\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.", UkrainianCuisineKeyboards.eighthNext1) },
            { "1quizEighth3", (null, "Ні\r\nТрадиційна печеня – це м'ясо, з додованням картоплі та інших овочів, тушкованих на плиті або в духовці.", UkrainianCuisineKeyboards.eighthNext1) },
            { "eighthNext1", ("Resources/Images/1quiz/quiz9.jpg", "На Миколаївщині популярний матвіївський зелений борщ із бичками. А на основі чого готують борщ у Шацьку, що борщ має абсолютно чорний колір?", UkrainianCuisineKeyboards.quiz19) },
            { "1quizNinth1", (null, "Так!\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.", UkrainianCuisineKeyboards.ninthNext1) },
            { "1quizNinth2", (null, "Ну майже) Ні\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.", UkrainianCuisineKeyboards.ninthNext1) },
            { "1quizNinth3", (null, "Так роблять багато де, але не у Шацьку.\r\nВ Шацьку, Волинської області до страви наливають кров дикого кабана, тому борщ має абсолютно чорний колір.", UkrainianCuisineKeyboards.ninthNext1) },
            { "ninthNext1", ("Resources/Images/1quiz/quiz10.jpg", "В Україні цей продукт має статус географічно зазначеного продукту, адже може виготовлятися лише тут", UkrainianCuisineKeyboards.quiz10) },
            { "1quizTenth1", (null, "Вірно!\r\nНещодавно такий сир пройшов офіційну реєстрацію й отримав «захищену назву з географічним зазначенням». «Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.", null) },
            { "1quizTenth2", (null, "Ні, не вірно.\r\nНещодавно бринза пройшла офіційну реєстрацію й отримала «захищену назву з географічним зазначенням». «Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.", null) },
            { "1quizTenth3", (null, "Ти напевно вже зголоднів/зголодніла. Але відповідь не вірна!\r\nНещодавно бринза пройшла офіційну реєстрацію й отримала «захищену назву з географічним зазначенням». «Бриндзя» стала в один ряд зі всесвітньо відомими пармезаном, шампанським або портвейном.", null) },
        };
    }
}