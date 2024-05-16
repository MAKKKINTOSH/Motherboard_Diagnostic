using System;

namespace Motherboard_Diagnostic
{
    class USB : Component
    {
        protected static readonly Random Rnd = new();
        private static float GoodVoltage, BadVoltageDPlus, BadVoltageDMinus;
        public USB()
        {
            this.DiagnosticData = new()
            {
                new MultiElementDiagnosticData(
                    instrument: Instruments.VoltmeterVoltageDown,
                    faultId: 2,
                    dataType: DiagnosticDataType.Text,
                    buttons: new string[] {"USB2(1)", "USB2(2)", "USB2(3)", "USB3(1)", "USB3(2)"},
                    getWorkingData: GetGoodVoltage,
                    getBrokenData: GetBadVoltage
                )
            };

            BadVoltageDPlus = Rnd.Next(0, 13000) * (float)0.0001;
            BadVoltageDMinus = BadVoltageDPlus > 0.6 ? 
                BadVoltageDPlus - (float)(Rnd.Next(200, 400) * 0.001) : 
                BadVoltageDPlus + (float)(Rnd.Next(200, 400) * 0.001);
            
            GoodVoltage = Rnd.Next(450, 700) * (float)0.001;
        }
        private static float GetVoltage(float voltage)
        {
            return Rnd.Next((int)(voltage * 1000) - 10, (int)(voltage * 1000) + 10) * (float)0.001;

        }
        private static string GetGoodVoltage()
        {
            return VoltmeterMessage(GoodVoltage, GoodVoltage);
        }
        private static string GetBadVoltage()
        {
            return VoltmeterMessage(BadVoltageDPlus, BadVoltageDMinus);
        }
        private static string VoltmeterMessage(float voltageDPlus, float voltageDMinus)
        {
            string message = "Падение напряжения:\n";
            message += $"D+: {GetVoltage(voltageDPlus):N5} мВ\n";
            message += $"D-: {GetVoltage(voltageDMinus):N5} мВ";
            return message;
        }
    }
}
