using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT
{
    class Program
    {
        const int WINDOW_HEIGHT = 35;
        const int WINDOW_WIDTH = 100;
        public static List<Models.Tweet> tweets = new List<Models.Tweet>();
        public delegate void TweetAddedHandler(Models.Tweet tweet);
        public static event TweetAddedHandler AddedToArrayTweet;
        public static Views.TweetDisplay td;
        public static TwitterInterface ti;

        static void Main(string[] args)
        {
            Console.BufferWidth = WINDOW_WIDTH;
            Console.BufferHeight = WINDOW_HEIGHT;
            Console.WindowWidth = WINDOW_WIDTH;
            Console.WindowHeight = WINDOW_HEIGHT;

            Views.SplashScreen.show();

            Events.TwitterListener.NewTweet += NewTweetHandler;

            ti = new TwitterInterface();
            View.Login.doLogin(ti);
            Console.Clear();

            ViewHelpers.Components.drawHeader(ti);
            ViewHelpers.Components.drawFooter();
            td = new Views.TweetDisplay();
            ti.listenAsync();
            td.interactive();

            Console.ReadKey();
            ti.listenStop();
        }

        static void NewTweetHandler(Models.Tweet tweet)
        {
            tweets.Insert(0, tweet);
            AddedToArrayTweet(tweet);
        }
    }
}
