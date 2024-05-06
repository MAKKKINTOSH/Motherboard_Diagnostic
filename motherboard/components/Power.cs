using System;
using System.Collections.Generic;

namespace Motherboard_Diagnostic
{
    class Power : Component
    {
        private static readonly Random rnd = new Random();
        public Power(int faultId) : base(faultId)
        {
            this.Responses = new Dictionary<string, List<Func<string>>>
            {
                { "ohmmeter", new List<Func<string>> {OhmmeterWorkingMessage, OhmmeterBrokenMessage} }
            };
        }

        private string OhmmeterWorkingMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetRandomValue(Config.minGoodOms, Config.maxGoodOms)} Ом\n";
            message += $"+5В: {GetRandomValue(Config.minGoodOms, Config.maxGoodOms)} Ом\n";
            message += $"+3,3В: {GetRandomValue(Config.minGoodOms, Config.maxGoodOms)} Ом\n";
            return message;
        }private string OhmmeterBrokenMessage()
        {
            string message = "Показатели омметра:\n";
            message += $"+12В: {GetRandomValue(1, 2)} Ом\n";
            message += $"+5В: {GetRandomValue(1, 2)} Ом\n";
            message += $"+3,3В: {GetRandomValue(1, 2)} Ом\n";
            return message;
        }
        private static float GetRandomValue(int min, int max)
        {
            return (float)(rnd.NextDouble() * (max - min) + min);
        }
    }
}
