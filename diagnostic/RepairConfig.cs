using System;

namespace Motherboard_Diagnostic
{
    class ElementDiagnosticData
    {
        public Instruments Instrument { get; }
        public Fault Fault { get; }
        public DiagnosticDataType DataType { get; }
        public Func<string> GetWorkingData { get; }
        public Func<string> GetBrokenData { get; }


        public ElementDiagnosticData(Instruments instrument, int faultId, DiagnosticDataType dataType, Func<string> getWorkingData, Func<string> getBrokenData)
        {
            this.Instrument = instrument;
            this.DataType = dataType;
            this.Fault = DiagnosticHandbook.Faults.Find((x) => x.Id==faultId);
            this.GetWorkingData = getWorkingData;
            this.GetBrokenData = getBrokenData;
        }
    }
    enum DiagnosticDataType
    {
        Text,
        Chart
    }
}
