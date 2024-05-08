using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motherboard_Diagnostic.motherboard
{
    internal class SouthBridge : Component
    {
        public SouthBridge() {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.Voltmeter,
                    faultId: 2,
                    getWorkingData: VoltmeterWorkingMessage,
                    getBrokenData: VoltmeterBrokenMessage
                )
            };
        }

        private string VoltmeterWorkingMessage()
        {
            string message = "Работает";
            return message;
        }
        private string VoltmeterBrokenMessage()
        {
            string message = "Не работает";
            return message;
        }

    }
}
