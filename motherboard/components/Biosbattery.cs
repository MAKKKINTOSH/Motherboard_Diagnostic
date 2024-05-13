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
        private static Random Rnd = new();
        public int BrokenPictureType = Rnd.Next(0, 3);
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
            string filename = "charts/rtcgood.png";
            return filename;
        }
        private string OscilloscopeBrokenChart()
        {
            string filename;
            filename = BrokenPictureType switch
            {
                0 => "charts/rtcbad_1.png",
                1 => "charts/rtcbad_2.png",
                2 => "charts/bad.png",
                _ => throw new NotImplementedException()
            };
            return filename;
        }
    }
}
