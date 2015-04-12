using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.ViewHelpers
{
    class Textbox : Control
    {
        /// <summary>
        /// The reason that the textbox lost focus.
        /// </summary>
        public enum exitReason
        {
            tab,
            shiftTab,
            enter,
            arrowDown,
            arrowUp
        }

        public int cursor = 0;
        public int width { get; set; }
        public string id { get; set; }
        public string value { get; set; }
        public bool focused { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        public Textbox() : this("") { }

        public Textbox(string id)
        {
            this.id = id;
            this.focused = false;
            this.value = "";
            width = 20;
        }

        public exitReason processInput()
        {
            Console.SetCursorPosition(x, y);
            focused = true;
            value = Console.ReadLine();
            return exitReason.enter;
        }
    }
}
