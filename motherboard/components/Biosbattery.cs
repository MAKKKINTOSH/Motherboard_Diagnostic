using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Motherboard_Diagnostic.motherboard.components
{
    internal class Biosbattery : Component
    {
        Random Rnd = new();
        public Biosbattery() {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.Oscilloscope,
                    faultId: 4,
                    dataType: DiagnosticDataType.Chart,
                    getWorkingData: OscilloscopeWorkingChart,
                    getBrokenData: OscilloscopeBrokenChart
                )
            };
        }
        private string OscilloscopeWorkingChart()
        {
            string filename = "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\rtcgood.png";
            return filename;
        }
        private string OscilloscopeBrokenChart()
        {
            string filename;
            filename = Rnd.Next(3) switch
            {
                0 => "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\rtcbad_1.png",
                1 => "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\rtcbad_2.png",
                2 => "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\bad.png"
            };
            return filename;
        }
    }
}
