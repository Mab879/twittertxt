using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.ViewHelpers
{
    class TweetHelper
    {
        public Models.Tweet tweet;
        public int y = 0;
        public int x = 1;
        private int cursor = 0;
        public bool focused { get; set; }

        public TweetHelper(Models.Tweet tweet)
        {
            this.tweet = tweet;
        }

        //public ConsoleKeyInfo processInput()
        //{
        //    while (true)
        //    {
        //        return Console.ReadKey();
        //    }
        //}

        public void draw()
        {
            String tweetString = "";

            string titleBG = "/b" + TextUtils.colorToString(TextUtils.colors.darkCyan);
            string nameFG = "/f" + TextUtils.colorToString(TextUtils.colors.white);
            string bodyBG = "/b" + TextUtils.colorToString(TextUtils.colors.gray);
            string bodyFG = "/f" + TextUtils.colorToString(TextUtils.colors.black);
            string screenNameFG = "/f" + TextUtils.colorToString(TextUtils.colors.gray);

            if (focused)
            {
                titleBG = "/b" + TextUtils.colorToString(TextUtils.colors.cyan);
                nameFG = "/f" + TextUtils.colorToString(TextUtils.colors.black);
                bodyFG = "/f" + TextUtils.colorToString(TextUtils.colors.black);
                screenNameFG = "/f" + TextUtils.colorToString(TextUtils.colors.darkGray);
            }

            // Content BG
            TextUtils.writeFormattedText(bodyBG);
            TextUtils.blankRegion(0, y, Console.BufferWidth, TextUtils.countWrappedLines(tweetString, Console.BufferWidth) + 1);
            // Title bar
            TextUtils.writeFormattedText(titleBG);
            TextUtils.blankRegion(0, y, Console.BufferWidth, 1);
            tweetString += titleBG + nameFG + tweet.user.name;
            tweetString += screenNameFG + " (@" + tweet.user.screen_name + ")\n";
            tweetString += bodyBG + bodyFG + tweet.text;

            TextUtils.writeTextInBox(tweetString, x, y, Console.BufferWidth - 2);
        }
    }
}
