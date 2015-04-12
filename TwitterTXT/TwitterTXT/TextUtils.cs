using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT
{
    class TextUtils
    {
        /// <summary>
        /// Is the number of spaces in each tab stop.
        /// </summary>
        public static int TabStops = 5;

        /// <summary>
        /// Colors for the command line
        /// </summary>
        public enum colors
        {
            black = 0,
            blue = 1,
            cyan = 2,
            darkBlue = 3,
            darkCyan = 4,
            darkGray = 5,
            darkGreen = 6,
            darkMagenta = 7,
            darkRed = 8,
            darkYellow = 9,
            gray = 10,
            green = 11,
            magenta = 12,
            red = 13,
            white = 14,
            yellow = 15,

        }
        private static ConsoleColor idenityToActualColor(colors id)
        {
            /*Switchcase allows us to go from the enum colors to actuall ConsoleColors, which allows us to change fore
             and background colors for the text.*/
            switch (id)
            {
                case colors.black:
                    return ConsoleColor.Black;
                case colors.blue:
                    return ConsoleColor.Blue;
                case colors.cyan:
                    return ConsoleColor.Cyan;
                case colors.darkBlue:
                    return ConsoleColor.DarkBlue;
                case colors.darkCyan:
                    return ConsoleColor.DarkCyan;
                case colors.darkGray:
                    return ConsoleColor.DarkGray;
                case colors.darkGreen:
                    return ConsoleColor.DarkGreen;
                case colors.darkMagenta:
                    return ConsoleColor.DarkMagenta;
                case colors.darkRed:
                    return ConsoleColor.DarkRed;
                case colors.darkYellow:
                    return ConsoleColor.DarkYellow;
                case colors.gray:
                    return ConsoleColor.Gray;
                case colors.green:
                    return ConsoleColor.Green;
                case colors.magenta:
                    return ConsoleColor.Magenta;
                case colors.red:
                    return ConsoleColor.Red;
                case colors.white:
                    return ConsoleColor.White;
                case colors.yellow:
                    return ConsoleColor.Yellow;
            }
            throw new Exception("Color is not Valid");
            return ConsoleColor.White;

        }
        public static string colorToString(colors color)
        {
            return string.Format("{0}", (int)color).PadLeft(2, '0');
        }

        /// <summary>
        /// Converts tab characters to the correct number of spaces that aligns with the tab stops.
        /// </summary>
        /// <param name="s">A string with tab characters</param>
        /// <returns>Formatted string</returns>
        public static string convertTabsToSpaces(string s)
        {

            StringBuilder sb = new StringBuilder(s);
            int numCharB4Tab, count, a;
            numCharB4Tab = 0;
            for (count = 0; count < sb.Length; count++)
            {
                if (sb[count] == '\t')
                {
                    sb.Remove(count, 1);
                    for (a = 0; a < (TabStops - (numCharB4Tab % TabStops)); a++)
                    {
                        sb.Insert(count, ' ');
                    }
                }
                numCharB4Tab++;
                if (sb[count] == '\n')
                {
                    numCharB4Tab = 0;
                }
            }

            return sb.ToString();
        }
        /// <summary>
        /// Formats the raw string by aligning into rows and correctly assigning colors to the fore and background
        /// </summary>
        /// <param name="formatted">A string that needs to be colored and properly aligned</param>
        public static void writeFormattedText(string formatted)
        {
            string newFormat = convertTabsToSpaces(formatted);
            int index, numColorVal;
            string a;
            for (index = 0; index < newFormat.Length; index++)
            {
                if (newFormat[index] == '/' && newFormat[index + 1] == 'f')
                {
                    a = newFormat.Substring(index + 2, 2);
                    numColorVal = Convert.ToInt32(a);
                    Console.ForegroundColor = idenityToActualColor((colors)numColorVal);
                    index += 3;
                }

                else if (newFormat[index] == '/' && newFormat[index + 1] == 'b')
                {
                    a = newFormat.Substring(index + 2, 2);
                    numColorVal = Convert.ToInt32(a);
                    Console.BackgroundColor = idenityToActualColor((colors)numColorVal);
                    index += 3;
                }
                else Console.Write(newFormat[index]);
            }

        }

        public static void writeTextInBox(string formatted, int x, int y, int width)
        {
            Console.SetCursorPosition(x, y);
            StringBuilder sb = new StringBuilder(formatted);
            int index, spaceCounter, numColorVal;
            int widthcounter = 0;
            int numCharB4Tab = 0;
            string colorCode;
            for (index = 0; index < sb.Length; index++)
            {
                widthcounter++;
                numCharB4Tab++;
                if (sb[index] == '\t')
                {
                    sb.Remove(index, 1);
                    for (spaceCounter = 0; spaceCounter < (TabStops - (numCharB4Tab % TabStops)); spaceCounter++)
                    {
                        sb.Insert(index, ' ');
                    }
                    Console.Write(' ');
                }

                else if (sb[index] == '\n')
                {
                    sb.Remove(index, 1);
                    sb.Insert(index, ' ');
                    Console.WriteLine();
                    Console.CursorLeft = x;
                    numCharB4Tab = 0;
                    widthcounter = 0;

                }
                else if (widthcounter == width)
                {
                    Console.WriteLine();
                    Console.CursorLeft = x;
                    widthcounter = 0;
                    numCharB4Tab = 0;
                }
                else if (sb[index] == '/')
                {
                    if (sb[index + 1] == 'f')
                    {
                        colorCode = sb.ToString().Substring(index + 2, 2);
                        numColorVal = Convert.ToInt32(colorCode);
                        Console.ForegroundColor = idenityToActualColor((colors)numColorVal);
                        index += 3;
                    }

                    else if (sb[index + 1] == 'b')
                    {
                        colorCode = sb.ToString().Substring(index + 2, 2);
                        numColorVal = Convert.ToInt32(colorCode);
                        Console.BackgroundColor = idenityToActualColor((colors)numColorVal);
                        index += 3;
                    }
                    else
                    {
                        Console.Write(sb[index]);
                    }
                }

                else Console.Write(sb[index]);
            }

        }

        public static int countExpandedChars(string formatted)
        {
            string reformatted = convertTabsToSpaces(formatted);
            Console.WriteLine(reformatted);
            int index;
            int count = 0;

            for (index = 0; index < reformatted.Length; index++)
            {
                if (reformatted[index] == '/')
                {
                    if (reformatted[index + 1] == 'f')
                    {
                        index += 3;
                    }

                    else if (reformatted[index + 1] == 'b')
                    {
                        index += 3;
                    }
                }
                else count++;

            }
            return count;
        }

        public static int countWrappedLines(string formatted, int width)
        {

            int howManyLines = 1;
            StringBuilder sb = new StringBuilder(formatted);
            int index, spaceCounter, charCounter;
            int widthcounter = 0;
            int numCharB4Tab = 0;
            charCounter = 0;
            for (index = 0; index < sb.Length; index++)
            {
                widthcounter++;
                numCharB4Tab++;
                if (sb[index] == '\t')
                {
                    sb.Remove(index, 1);
                    for (spaceCounter = 0; spaceCounter < (TabStops - (numCharB4Tab % TabStops)); spaceCounter++)
                    {
                        sb.Insert(index, ' ');
                    }
                    sb.Insert(index, ' ');
                    charCounter++;

                }
                else if (sb[index] == '\n')
                {
                    howManyLines++;

                    numCharB4Tab = 0;
                    widthcounter = 0;

                }
                else if (widthcounter == width)
                {

                    widthcounter = 0;
                    numCharB4Tab = 0;
                }
                else if (sb[index] == '/')
                {
                    if ((sb[index + 1] == 'f') || (sb[index + 1] == 'b'))
                    {
                        index += 3;
                    }

                }

                else charCounter++;
            }

            howManyLines += charCounter / width;
            if (charCounter % width >= 1)
            {
                howManyLines++;
            }
            return howManyLines;
        }

        public static void blankRegion(int x, int y, int width, int height)
        {
            Console.SetCursorPosition(x, y);
            int a, b;
            for (a = 0; a < height; a++)
            {
                for (b = 0; b < width; b++)
                {
                    Console.Write(' ');
                }
                if (Console.CursorLeft > 1)
                {
                    Console.Write('\n');
                }
                Console.CursorLeft = x;
            }
        }
        public static void logoName(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(" __          __  _                            _         ");
            Console.CursorLeft = x;
            Console.WriteLine(" \\ \\        / / | |                          | |      _ ");
            Console.CursorLeft = x;
            Console.WriteLine("  \\ \\  /\\  / /__| | ___ ___  _ __ ___   ___  | |_ ___(_)");
            Console.CursorLeft = x;
            Console.WriteLine("   \\ \\/  \\/ / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\ | __/ _ \\  ");
            Console.CursorLeft = x;
            Console.WriteLine("    \\  /\\  /  __/ | (_| (_) | | | | | |  __/ | || (_) | ");
            Console.CursorLeft = x;
            Console.WriteLine("  ___\\/__\\/ \\___|_|\\___\\___/|_| |_|_|_|\\___|__\\__\\___(_)");
            Console.CursorLeft = x;
            Console.WriteLine(" |__   __|     (_) | | |       |__   __\\ \\ / /__   __| |");
            Console.CursorLeft = x;
            Console.WriteLine("    | |_      ___| |_| |_ ___ _ __| |   \\ V /   | |  | |");
            Console.CursorLeft = x;
            Console.WriteLine("    | \\ \\ /\\ / / | __| __/ _ \\ '__| |    > <    | |  | |");
            Console.CursorLeft = x;
            Console.WriteLine("    | |\\ V  V /| | |_| ||  __/ |  | |   / . \\   | |  |_|");
            Console.CursorLeft = x;
            Console.WriteLine("    |_| \\_/\\_/ |_|\\__|\\__\\___|_|  |_|  /_/ \\_\\  |_|  (_)");
        }

        /// <summary>
        /// Fills the specified region with the specified color. All text will be overwritten with spaces.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void fillAreaWithColor(ConsoleColor color, int x, int y, int width, int height)
        {
            Console.MoveBufferArea(x, y, width, height, 0, Console.BufferHeight, ' ', ConsoleColor.Gray, color);
        }

    }

}
