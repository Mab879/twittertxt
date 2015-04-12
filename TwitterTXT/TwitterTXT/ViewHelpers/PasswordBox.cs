using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.ViewHelpers
{
    class PasswordBox : Textbox
    {
        public new Textbox.exitReason processInput()
        {
            Console.SetCursorPosition(x, y);
            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            while (true)
            {
                if (pressedKey.Key == ConsoleKey.Enter)
                {
                    return Textbox.exitReason.enter;
                }
                else if (pressedKey.Key == ConsoleKey.Tab)
                {
                    if (pressedKey.Modifiers.HasFlag(ConsoleModifiers.Shift))
                    {
                        return exitReason.shiftTab;
                    }
                    else
                    {
                        return Textbox.exitReason.tab;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Backspace)
                {
                    if (cursor > 0)
                    {
                        base.cursor--;
                        Console.CursorLeft = x + cursor;
                        Console.Write(" ");
                        Console.CursorLeft--;
                        base.value = base.value.Substring(0, base.value.Length - 1);
                    }
                }// TODO: arrow keys
                else
                {
                    base.cursor++;
                    base.value += pressedKey.KeyChar;
                    Console.Write("*");
                }
                pressedKey = Console.ReadKey(true);
            }
        }
    }
}
