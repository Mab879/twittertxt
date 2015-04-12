using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.Views
{
    class SplashScreen
    {
        public static void show()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            Console.Write(Properties.Resources.LogoASCIIArt);
            System.Threading.Thread.Sleep(1000);
            Console.Clear();
            Console.CursorVisible = true;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
