using System;

namespace Motherboard_Diagnostic
{
    static class CalculationUtils
    {
        static Random Rnd = new();
        public static float GetRandomFloat(float start, float end)
        {
            return (float)Rnd.NextDouble() * (end - start) + start;
        }
    }
}
