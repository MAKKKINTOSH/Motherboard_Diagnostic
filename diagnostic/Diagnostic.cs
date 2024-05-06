using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Motherboard_Diagnostic
{
    
    class Diagnostic
    {
        public static HashSet<Fault> Faults { get; set; } = null;
        public static bool IsRunning { get; set; } = false;

        public Diagnostic()
        {
        }
        public static void generateFaults(int quantity)
        {
            Faults = new HashSet<Fault>();
            Random rnd = new Random();
            while (Faults.Count != quantity)
            {
                int fault_id = rnd.Next(1, 6);
                for (int j = 0; j < DiagnosticHandbook.Faults.Count; j++)
                {
                    Fault fault = DiagnosticHandbook.Faults[j];
                    if (fault.id == fault_id)
                    {
                        Faults.Add(fault);
                    }
                }
            }
        }
        public static bool hasFault(int faultId)
        {
            foreach (var item in Faults)
            {
                if (item.id == faultId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
