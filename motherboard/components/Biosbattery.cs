using System;

namespace Motherboard_Diagnostic.motherboard.components
{
    internal class Biosbattery : Component
    {
        private static Random Rnd = new();
        private float Voltage;
        public Biosbattery() {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.VoltmeterBase,
                    faultId: 9,
                    dataType: DiagnosticDataType.Text,
                    getWorkingData: VoltmeterWorkingMessage,
                    getBrokenData: VoltmeterBrokenMessage
                )
            };
            if (Diagnostic.HasFault(this.DiagnosticData[0].Fault))
            {
                this.Voltage = CalculationUtils.GetRandomFloat(0.8f, 1.7f);
            }
            else
            {
                this.Voltage = 3;
            }
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
