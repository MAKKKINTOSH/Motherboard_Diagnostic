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
        public static USB USB;
        public static BIOS BIOS;
        public static Biosbattery Biosbattery;
        public static PCInterface PCInterface;
        public static Capacitor Capacitor;
        public static RAMSlot RAMSlot;
        public static RTC RTC;

        public static void Init()
        {
            Power = new();
            USB = new();
            BIOS = new();
            Biosbattery = new();
            PCInterface = new();
            Capacitor = new();
            RAMSlot = new();
            RTC = new RTC();
        }
    }
}
