using System;
using System.Collections.Generic;
using System.Linq;

namespace Motherboard_Diagnostic
{
    
    class Diagnostic
    {
        public static HashSet<Fault> Faults { get; set; } = null;
        public static List<Solution> Solutions { get; set; } = null;
        public static int[] FaultsIdsArray = DiagnosticHandbook.Faults.Select(x => x.Id).ToArray();
        public static bool IsRunning { get; set; }
        public static bool PCIsLaunch = false;
        public static bool CanRepair = false;

        public static void Init()
        {
            GenerateFaults(Config.faultsQuantity);
            GenerateSolutions();
            IsRunning = false;
            CanRepair = false;
            PCIsLaunch = false;
        }
        private static void GenerateFaults(int quantity)
        {
            Faults = new();
            Random rnd = new();
            while (Faults.Count != quantity)
            {
                int faultArrayIndex = rnd.Next(0, FaultsIdsArray.Length);
                Faults.Add(DiagnosticHandbook.Faults.Find(x => x.Id == FaultsIdsArray[faultArrayIndex]));
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
        public static void AddFaultByID(int faultId)    // Только для удобства разработки, в проде не юзается
        {
            Faults.Add(DiagnosticHandbook.Faults.Find(x => x.Id == faultId));
        }
    }
}
