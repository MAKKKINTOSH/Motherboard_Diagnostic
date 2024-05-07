using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Motherboard_Diagnostic
{
    
    class Diagnostic
    {
        public static HashSet<Fault> Faults { get; set; } = null;
        public static List<Solution> Solutions { get; set; } = null;
        public static bool IsRunning { get; set; }

        public static void Init()
        {
            GenerateFaults(Config.faultsQuantity);
            GenerateSolutions();
            IsRunning = false;
        }
        private static void GenerateFaults(int quantity)
        {
            Faults = new();
            Random rnd = new();
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
        private static void GenerateSolutions()
        {
            Solutions = new();
            foreach (var sol in DiagnosticHandbook.Solutions)
            {
                Solutions.Add(sol);
            }
        }
        public static bool HasFault(int faultId)
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
