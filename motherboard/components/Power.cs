using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Motherboard_Diagnostic
{
    class Power : Component
    {
        private static readonly Random Rnd = new();
        private static List<int> BrokenLines = new() {0, 0, 0};
        
        public Power()
        {
            this.DiagnosticData = new()
            {
                new ElementDiagnosticData(
                    instrument: Instruments.Ohmmeter,
                    faultId: 1,
                    getWorkingData: OhmmeterWorkingMessage,
                    getBrokenData: OhmmeterBrokenMessage
                )
            };
            if (Diagnostic.HasFault(this.DiagnosticData[0].Fault))
            {
                SetBrokenLines();
            }
        }
        private static void SetBrokenLines()
        {
            int countLines = Rnd.Next(3);
            while (BrokenLines.Sum() < countLines)
            {
                BrokenLines[Rnd.Next(0, 3)] = 1;
            }
        }
        private string OhmmeterWorkingMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            message += $"+5В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            message += $"+3,3В: {GetOhmmeterValue(Convert.ToBoolean(false))} Ом\n";
            return message;
        }
        private string OhmmeterBrokenMessage()
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
                false => GetRandomValue(Config.minGoodOms, Config.maxGoodOms),
                true => GetRandomValue(1, 2)
            };
        }
        private static float GetRandomValue(int min, int max)
        {
            return (float)(Rnd.NextDouble() * (max - min) + min);
        }
    }
}
