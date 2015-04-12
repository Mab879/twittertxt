using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.ViewHelpers
{
    class Components
    {

        public static void drawFooter()
        {
            string formatted = "";
            //TextUtils.blankRegion(0, Console.BufferHeight - 2, Console.BufferWidth, 2);
            for (int x = 0; x < Keyboard.shortcuts.Count; x++)
            {
                formatted += String.Format("\t^{0} {1}", Keyboard.shortcuts[x].key, Keyboard.shortcuts[x].friendlyName);
            }
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            TextUtils.fillAreaWithColor(ConsoleColor.DarkCyan, 0, Console.BufferHeight - 2, Console.BufferWidth, 2); 
            TextUtils.writeTextInBox(formatted, 0, Console.BufferHeight - 2, Console.BufferWidth);
        }


        public static void drawHeader(TwitterInterface ti)
        {
            string formatted = "";
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            TextUtils.blankRegion(0, 0, Console.BufferWidth, 3);
            formatted = String.Format("TwitterTXT - {0} /f10/b04", ti.screenanme);
            TextUtils.writeTextInBox(formatted, 1, 1, Console.BufferWidth);
        }

        public class Keyboard
        {
            public struct shortcut
            {
                public char key;
                public string friendlyName;
                public delegate void shortcutHandlerDel();
                public Del shortcutHandler;

                public shortcut(char key, string friendlyName, Del handler)
                {
                    this.key = key;
                    this.friendlyName = friendlyName;
                    this.shortcutHandler = handler;
                }
            }

            public static List<shortcut> shortcuts = new List<shortcut> { 
                new shortcut('x', "Exit", Exit),
                new shortcut('t', "New Tweet", startNewTweet)
            };

            public delegate void Del();

            public static void Exit()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("Go Die Java. Long Live .NET! Long Live Ruby! Long Give Visual Basic! Long Live C#!");
                System.Threading.Thread.Sleep(542);
                Environment.Exit(0);
            }

            public static void startNewTweet()
            {
                Views.NewTweetScreen.interactive();
                Program.ti.postThatCrapToTwitter(Views.NewTweetScreen.userInput);
                Components.drawHeader(Program.ti);
                Components.drawFooter();
            }
        }
    }
}
