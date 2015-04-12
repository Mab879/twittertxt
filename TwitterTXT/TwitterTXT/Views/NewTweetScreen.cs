using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Views
{
    class NewTweetScreen
    {
        public static string userInput;
        public static void interactive()
        {
            bool isValidTweet;
            screenInterface();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Please Compose your Tweet");
            Console.CursorVisible = true;
            userInput = Console.ReadLine();
            isValidTweet = tweetChecker(userInput);
            while (!isValidTweet)
            {
                Console.WriteLine("Something went wrong, please try again \\._./");
                userInput = Console.ReadLine();
                isValidTweet = tweetChecker(userInput);
            }
            userInput = Uri.EscapeDataString(userInput);
            Console.CursorVisible = false;            
        }
        public static void screenInterface()
        {
            // header
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            TextUtils.blankRegion(0, 0, Console.BufferWidth , 3);
            // body
            Console.BackgroundColor = ConsoleColor.Gray;
            TextUtils.blankRegion(0, 3, Console.BufferWidth , Console.BufferHeight - 5);
            // footer
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            TextUtils.fillAreaWithColor(ConsoleColor.DarkCyan, 0, Console.BufferHeight - 2, Console.BufferWidth, 2);
        }

        private static bool tweetChecker(string tweet)
        {
            if (string.IsNullOrWhiteSpace(tweet))
            {
                return false;
            }
            if (tweet.Length > 140)
            {
                return false;
            }
            return true;
        }
    }
}
