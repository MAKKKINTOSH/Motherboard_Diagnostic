

using System;
using System.ComponentModel;
using System.Configuration;

namespace Motherboard_Diagnostic
{
    class ElementDiagnosticData
    {
        public Instruments Instrument { get; }
        public Fault Fault { get; }

        public Func<string> GetWorkingData { get; }
        public Func<string> GetBrokenData { get; }


        public ElementDiagnosticData(Instruments instrument, int faultId, Func<string> getWorkingData, Func<string> getBrokenData)
        {
            this.Instrument = instrument;
            this.Fault = DiagnosticHandbook.Faults.Find((x) => x.Id==faultId);
            this.GetWorkingData = getWorkingData;
            this.GetBrokenData = getBrokenData;
        }
    }
}
