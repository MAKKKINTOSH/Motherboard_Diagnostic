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
        public static bool PCIsLaunch = false;

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
                    if (fault.Id == fault_id)
                    {
                        Faults.Add(fault);
                    }
                }
            }
        }
        private static void GenerateSolutions()
        {
            Solutions = new();
            foreach (var fault in DiagnosticHandbook.Faults)
            {
                Solutions.Add(fault.Solution);
            }
        }
        public static bool HasFault(Fault fault)
        {
            foreach (var item in Faults)
            {
                if (item.Id == fault.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
