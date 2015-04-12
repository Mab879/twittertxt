using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterTXT.Models;

namespace TwitterTXT.Views
{
    class TweetDisplay
    {
        private ViewHelpers.TweetHelper focusedTweet;
        private int cursor = 0;

        public TweetDisplay()
        {
            Program.AddedToArrayTweet += Program_AddedToArrayTweet;
            Console.CursorVisible = false;
            redraw();
        }

        public void interactive()
        {
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    if (cursor < Program.tweets.Count - 1)
                    {
                        cursor++;
                        redraw();
                    }
                }
                else if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    if (cursor > 0)
                    {
                        cursor--;
                        redraw();
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Enter)
                {
                    System.Diagnostics.Process.Start("https://twitter.com/" + focusedTweet.tweet.user.screen_name + "/status/" + focusedTweet.tweet.id);
                }
                else if (pressedKey.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    for (int x = 0; x < ViewHelpers.Components.Keyboard.shortcuts.Count; x++)
                    {
                        if (pressedKey.Key.ToString().ToLower().ToCharArray()[0] == ViewHelpers.Components.Keyboard.shortcuts[x].key)
                        {
                            ViewHelpers.Components.Keyboard.shortcuts[x].shortcutHandler();
                        }
                    }
                }
            }
        }

        private void redraw()
        {
            int cursorY = 4;
            //Console.BackgroundColor = ConsoleColor.Black;
            //TextUtils.blankRegion(0, 4, Console.BufferWidth, Console.BufferHeight - 3 - cursorY);
            Console.BackgroundColor = ConsoleColor.Gray;
            //TextUtils.blankRegion(0, 3, Console.BufferWidth, Console.BufferHeight - 1 - cursorY);
            TextUtils.fillAreaWithColor(ConsoleColor.Gray, 0, 3, Console.BufferWidth, Console.BufferHeight - 1 - cursorY);
            int x = 0;
            while (x < Program.tweets.Count)
            {
                Models.Tweet t = Program.tweets[x];
                if (TextUtils.countWrappedLines(t.text, Console.BufferWidth) + cursorY > Console.BufferHeight - 1 - cursorY)
                {
                    Program.tweets.RemoveRange(x, Program.tweets.Count - x);
                    break;
                }
                ViewHelpers.TweetHelper th = new ViewHelpers.TweetHelper(t);
                if (x == cursor)
                {
                    th.focused = true;
                    focusedTweet = th;
                }
                th.y = cursorY;
                th.draw();
                cursorY += 2 + TextUtils.countWrappedLines(t.text, Console.BufferWidth - 2);
                x++; //Shh, don't tell Steve Kautz.
            }
        }

        private void Program_AddedToArrayTweet(Tweet tweet)
        {
            cursor = Math.Min(cursor, Program.tweets.Count - 1);
            redraw();
        }
    }
}
