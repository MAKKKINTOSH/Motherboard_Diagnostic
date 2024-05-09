using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motherboard_Diagnostic
{
    internal class PCInterface : Component
    {
        public PCInterface()
        {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.Videotester,
                    faultId: 5,
                    dataType: DiagnosticDataType.Text,
                    getWorkingData: VideotesterWorkingMessage,
                    getBrokenData: VideotesterBrokenMessage
                )
            };
        }
        private string VideotesterWorkingMessage()
        {
            string message = "Обрывов нет";
            return message;
        }
        private string VideotesterBrokenMessage()
        {
            string message = "Обрывы есть";
            return message;
        }
    }
}
