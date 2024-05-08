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
                    getWorkingData: OscilloscopeWorkingMessage,
                    getBrokenData: OscilloscopeBrokenMessage
                )
            };
        }

        private string OscilloscopeWorkingMessage()
        {
            string message = "Работает";
            return message;
        }
        private string OscilloscopeBrokenMessage()
        {
            string message = "Не работает";
            return message;
        }
    }
}
