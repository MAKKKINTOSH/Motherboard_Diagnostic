using System;

namespace Motherboard_Diagnostic
{
    class USB : Component
    {
        protected static readonly Random Rnd = new();
        private static bool IsBroken;
        private static float VoltageDPlus, VoltageDMinus;
        public USB()
        {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.VoltmeterVoltageDown,
                    faultId: 2,
                    getWorkingData: VoltmeterMessage,
                    getBrokenData: VoltmeterMessage
                )
            };
            if(Diagnostic.HasFault(this.DiagnosticData[0].Fault))
            {
                IsBroken = true;
            }
            else
            {
                IsBroken = false;
            }
            if (IsBroken)
            {
                VoltageDPlus = Rnd.Next(0, 13000) * (float)0.0001;
                if (VoltageDPlus < 0.7)
                {
                    VoltageDMinus = Rnd.Next((int)(VoltageDPlus * 10000) + 200, 13000) * (float)0.0001;
                }
                else
                {
                    VoltageDMinus = Rnd.Next(0, (int)(VoltageDPlus * 10000) - 200) * (float)0.0001;
                }
            }
            else
            {
                VoltageDPlus = Rnd.Next(450, 700) * (float)0.001;
                VoltageDMinus = Rnd.Next((int)(VoltageDPlus * 1000) - 10, (int)(VoltageDPlus * 1000) + 10) * (float)0.001;
            }
        }
        private static float GetVoltage(float voltage)
        {
            return Rnd.Next((int)(voltage * 1000) - 10, (int)(voltage * 1000) + 10) * (float)0.001;

        }
        private static string VoltmeterMessage()
        {
            string message = "Падение напряжения:\n";
            message += $"D+: {GetVoltage(VoltageDPlus):N5} мВ\n";
            message += $"D-: {GetVoltage(VoltageDMinus):N5} мВ";
            return message;
        }
    }
}
