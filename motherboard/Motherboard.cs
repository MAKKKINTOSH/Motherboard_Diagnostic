using Motherboard_Diagnostic.motherboard;
using Motherboard_Diagnostic.motherboard.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motherboard_Diagnostic
{
    class Motherboard
    {
        public static Power Power;
        public static SouthBridge SouthBridge;
        public static BIOS BIOS;

        public static void Init()
        {
            Power = new();
            SouthBridge = new();
            BIOS = new();
        }
    }
}
