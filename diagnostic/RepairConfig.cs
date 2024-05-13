using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Threading;
using System.Windows.Controls.Primitives;

namespace Motherboard_Diagnostic
{
    class ElementDiagnosticData
    {
        public Instruments Instrument { get; }
        public Fault Fault { get; }
        public DiagnosticDataType DataType { get; }
        public Func<string> GetWorkingData { get; }
        public Func<string> GetBrokenData { get; }
        public ElementDiagnosticData(
            Instruments instrument,
            int faultId,
            DiagnosticDataType dataType,
            Func<string> getWorkingData,
            Func<string> getBrokenData
        )
        {
            this.Instrument = instrument;
            this.Fault = DiagnosticHandbook.Faults.Find((x) => x.Id == faultId);
            this.DataType = dataType;
            this.GetWorkingData = getWorkingData;
            this.GetBrokenData = getBrokenData;
        }
    }
    class MultiElementDiagnosticData : ElementDiagnosticData
    {
        public List<Tuple<string, bool>>? MultiElements { get; } = null;
        public MultiElementDiagnosticData(
            Instruments instrument,
            int faultId,
            DiagnosticDataType dataType,
            string[] buttons,
            Func<string> getWorkingData,
            Func<string> getBrokenData
        ) : base(instrument, faultId, dataType, getWorkingData, getBrokenData)
        {
            this.MultiElements = new();
            Random rnd = new();
            int brokenIndex = rnd.Next(0, this.MultiElements.Count);
            bool isBroken;
            for (int i = 0; i < buttons.Length; i++)
            {
                isBroken = Convert.ToBoolean(rnd.Next(0, 2));
                if (i == brokenIndex)
                {
                    isBroken = true;
                }
                this.MultiElements.Add(new Tuple<string, bool>(buttons[i], isBroken));
            }
        }
    }
    enum DiagnosticDataType
    {
        Text,
        Chart
    }
}
