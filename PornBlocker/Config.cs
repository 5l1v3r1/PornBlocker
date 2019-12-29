// PornBlocker configuration file


namespace PornBlocker
{
    public class Config
    {

        // Blacklisted words
        public static string[] blacklist = new string[] {
                "аниме",
                "хентай",
                "hentai",
                "порно",
                "pornhub"
            };

        /*
         * Random text of messagebox 
         * {0} it's blacklisted word 
         */
        public static string[] messageText = new string[] {
                "Какого хуя ты смотришь {0}?",
                "Ты что пидор что-бы смотреть {0}?",
                "Тебе пизда",
                "Пизда тебе пидор",
                "Нихуя себе! Не знал что ты смотришь {0}",
                "Не ожидал от тебя такого",
                "Вас спалили за просмотром {0}",
                "Полиция вас застала за просмотром {0}"
            };

        // Random title of messagebox
        public static string[] messageTitle = new string[]
        {
                "Антивирус нашёл пидораса!",
                "Твой батя",
                "Твоя мамашка",
                "От учителя",
                "Кампухтер",
                "Пизда тебе!",
                "Обнаружен пидорас!",
                "Смерть тебе!",
                "У сука",
                "Я всё вижу",
                "Windows в ахуе от тебя!",
                "Пиздец",
                "Вас обнаружили",
                "Вас заметили",
                "FBI",
                "LOL"
        };


        // END
    }
}
