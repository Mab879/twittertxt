using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterTXT.ViewHelpers
{
    interface Control
    {
        string id { get; set; }
        string value { get; set; }
        bool focused { get; set; }
        int x { get; set; }
        int y { get; set; }
    }
}
