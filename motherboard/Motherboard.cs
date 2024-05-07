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

        public static void Init()
        {
            Power = new(1);
        }
    }
}
