using System;

namespace Motherboard_Diagnostic.motherboard.components
{
    internal class Biosbattery : Component
    {
        private static Random Rnd = new();
        public int BrokenPictureType = Rnd.Next(0, 3);
        private float Voltage;
        public Biosbattery() {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.Oscilloscope,
                    faultId: 4,
                    dataType: DiagnosticDataType.Chart,
                    getWorkingData: OscilloscopeWorkingChart,
                    getBrokenData: OscilloscopeBrokenChart
                ),
                new ElementDiagnosticData(
                    instrument: Instruments.VoltmeterBase,
                    faultId: 9,
                    dataType: DiagnosticDataType.Text,
                    getWorkingData: VoltmeterWorkingMessage,
                    getBrokenData: VoltmeterBrokenMessage
                )
            };
            if (Diagnostic.HasFault(this.DiagnosticData[1].Fault))
            {
                this.Voltage = CalculationUtils.GetRandomFloat(0.8f, 1.7f);
            }
            else
            {
                this.Voltage = 3;
            }
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
        private string VoltmeterWorkingMessage()
        {
            return $"Напряжение: {this.GetVoltage()} В";
        }
        private string VoltmeterBrokenMessage()
        {
            int orient = Rnd.Next(0, 2) == 1 ? -1 : 1;
            return $"Напряжение: {this.GetVoltage()} В";
        }

        private float GetVoltage()
        {
            return this.Voltage + CalculationUtils.GetRandomFloat(-0.05f, 0.05f);
        }
    }
}
