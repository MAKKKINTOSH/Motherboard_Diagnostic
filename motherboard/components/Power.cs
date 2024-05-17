using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Motherboard_Diagnostic
{
    class Power : Component
    {
        private static readonly Random Rnd = new();
        private static readonly List<int> BrokenLines = new() {0, 0, 0};
        private static float Voltage12, Voltage5, Voltage33;
        
        public Power()
        {
            this.DiagnosticData = new()
            {
                new MultiElementDiagnosticData(
                    instrument: Instruments.Ohmmeter,
                    faultId: 1,
                    dataType: DiagnosticDataType.Text,
                    buttons: new string[] {"8_Pin", "24_Pin"},
                    getWorkingData: OhmmeterWorkingMessage,
                    getBrokenData: OhmmeterBrokenMessage
                ),
                new MultiElementDiagnosticData(
                    instrument: Instruments.VoltmeterBase,
                    faultId: 6,
                    dataType: DiagnosticDataType.Text,
                    buttons: new string[] { "8_Pin", "24_Pin" },
                    getWorkingData: VoltmeterWorkingMessage,
                    getBrokenData: VoltmeterBrokenMessage
                )
            };
            if (Diagnostic.HasFault(this.DiagnosticData[0].Fault))
            {
                SetBrokenLines();
            }
            if (Diagnostic.HasFault(this.DiagnosticData[1].Fault))
            {
                Voltage12 = 12 - CalculationUtils.GetRandomFloat(6, 10);
                Voltage5 = 5 - CalculationUtils.GetRandomFloat(3, 4);
                Voltage33 = 3.3f - CalculationUtils.GetRandomFloat(2, 2.5f);

            }
        }
        private static void SetBrokenLines()
        {
            int countLines = Rnd.Next(1, 4);
            while (BrokenLines.Sum() < countLines)
            {
                BrokenLines[Rnd.Next(0, 3)] = 1;
            }
        }
        private static string OhmmeterWorkingMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            message += $"+5В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            message += $"+3,3В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            return message;
        }
        private static string OhmmeterBrokenMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetOhmmeterValue(Convert.ToBoolean(BrokenLines[0]))} Ом\n";
            message += $"+5В: {GetOhmmeterValue(Convert.ToBoolean(BrokenLines[1]))} Ом\n";
            message += $"+3,3В: {GetOhmmeterValue(Convert.ToBoolean(BrokenLines[2]))} Ом\n";
            return message;
        }

        private static float GetOhmmeterValue(bool isBroken)
        {
            return isBroken switch
            {
                false => CalculationUtils.GetRandomFloat(2000, 5000),
                true => CalculationUtils.GetRandomFloat(1, 2)
            };
        }
        private static string VoltmeterWorkingMessage()
        {
            string message = "Показатели вольтметра:\n";
            message += $"+12В: {GetVoltage(false)[0]} Ом\n";
            message += $"+5В: {GetVoltage(false)[1]} Ом\n";
            message += $"+3,3В: {GetVoltage(false)[2]} Ом\n";
            return message;
        }
        private static string VoltmeterBrokenMessage()
        {
            string message = "Показатели вольтметра:\n";
            message += $"+12В: {GetVoltage(true)[0]} Ом\n";
            message += $"+5В: {GetVoltage(true)[1]} Ом\n";
            message += $"+3,3В: {GetVoltage(true)[2]} Ом\n";
            return message;
        }

        private static float[] GetVoltage(bool isBroken)
        {
            return isBroken switch
            {
                true => new float[]
                {
                    Voltage12 + CalculationUtils.GetRandomFloat(-0.1f, 0.1f),
                    Voltage5 + CalculationUtils.GetRandomFloat(-0.1f, 0.1f),
                    Voltage33 + CalculationUtils.GetRandomFloat(-0.1f, 0.1f)
                },
                false => new float[]
                {
                    12 + CalculationUtils.GetRandomFloat(-0.1f, 0.1f),
                    5 + CalculationUtils.GetRandomFloat(-0.1f, 0.1f),
                    3.3f + CalculationUtils.GetRandomFloat(-0.1f, 0.1f)
                }
            };
        }
    }
}
