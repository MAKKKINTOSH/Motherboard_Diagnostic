using System;
using System.Threading;

namespace Motherboard_Diagnostic.motherboard.components
{
    internal class Capacitor : Component
    {
        public Capacitor()
        {
            this.DiagnosticData = new()
            {
                new MultiElementDiagnosticData(
                    instrument: Instruments.Calling,
                    faultId: 10,
                    dataType: DiagnosticDataType.Text,
                    buttons: new string[]{"C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "C11", "C12"},
                    getWorkingData: GetWorkingData,
                    getBrokenData: GetBrokenData
                )
            };
        }
        public static string GetWorkingData()
        {
            return "Прислушайся";
        }
        public static string GetBrokenData()
        {
            Thread beepThread = new(() => Console.Beep(5100, 1100));
            beepThread.Start();
            return "Прислушайся";
        }
    }
}

