using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Motherboard_Diagnostic
{
    class Power : Component
    {
        private static readonly Random rnd = new Random();
        private static List<int> brokenLines = new() {0, 0, 0};
        
        public Power(int faultId) : base(faultId)
        {
            this.Responses = new Dictionary<string, List<Func<string>>>
            {
                { "ohmmeter", new List<Func<string>> {OhmmeterMessage, OhmmeterMessage} }
            };
            if (Diagnostic.HasFault(FaultId))
            {
                SetBrokenLines();
            }
        }
        private static void SetBrokenLines()
        {
            int countLines = rnd.Next(3);
            while (brokenLines.Sum() < countLines)
            {
                brokenLines[rnd.Next(0, 3)] = 1;
            }
        }
        private static string OhmmeterMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetOhmmeterValue(Convert.ToBoolean(brokenLines[0]))} Ом\n";
            message += $"+5В: {GetOhmmeterValue(Convert.ToBoolean(brokenLines[1]))} Ом\n";
            message += $"+3,3В: {GetOhmmeterValue(Convert.ToBoolean(brokenLines[2]))} Ом\n";
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
            return (float)(rnd.NextDouble() * (max - min) + min);
        }
    }
}
