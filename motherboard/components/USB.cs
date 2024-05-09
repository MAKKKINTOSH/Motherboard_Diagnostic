using System;

namespace Motherboard_Diagnostic
{
    class USB : Component
    {
        protected static readonly Random Rnd = new();
        private static bool IsBroken = false;
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
        private static void SetVoltage()
        {
            VoltageDPlus = Rnd.Next((int)(VoltageDPlus * 1000) - 10, (int)(VoltageDPlus * 1000) + 10) * (float)0.001;
            VoltageDMinus = Rnd.Next((int)(VoltageDMinus * 1000) - 10, (int)(VoltageDMinus * 1000) + 10) * (float)0.001;

        }
        private static string VoltmeterMessage()
        {
            SetVoltage();
            string message = "Падение напряжения:\n";
            message += $"D+: {VoltageDPlus:N5} мВ\n";
            message += $"D-: {VoltageDMinus:N5} мВ";
            return message;
        }
    }
}
