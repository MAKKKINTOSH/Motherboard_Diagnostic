using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Motherboard_Diagnostic
{
    class Power : Component
    {
        public Power(string instrument, int faultId) : base(instrument, faultId) { }
    }
}
