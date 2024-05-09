using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motherboard_Diagnostic.motherboard.components
{
    internal class BIOS : Component
    {
        public BIOS() {
            this.DiagnosticData = new()
            {
            new ElementDiagnosticData(
                    instrument: Instruments.Oscilloscope,
                    faultId: 3,
                    dataType: DiagnosticDataType.Chart,
                    getWorkingData: OscilloscopeWorkingChart,
                    getBrokenData: OscilloscopeBrokenChart
                )
            };
        }

        private string OscilloscopeWorkingChart()
        {
            string filename = "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\biosgood.png";
            return filename;
        }
        private string OscilloscopeBrokenChart()
        {
            string filename = "C:\\Рабочий стол\\Motherboard_Diagnostic\\media\\charts\\bad.png";
            return filename;
        }
    }
}
