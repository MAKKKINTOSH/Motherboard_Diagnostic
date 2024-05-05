using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Motherboard_Diagnostic
{
    
    class Diagnosic
    {
        public List<Fault> Faults { get; set; } = null;
        public bool IsRunning { get; set; } = false;

        public Diagnosic()
        {
        }
        public void generateFaults(int quantity)
        {
            this.Faults = new List<Fault>();
            Random rnd = new Random();

            for (int i = 0; i < quantity; i++)
            {
                int fault_id = rnd.Next(1, 6);
                for (int j = 0; j < DiagnosticHandbook.Faults.Count; j++)
                {
                    Fault fault = DiagnosticHandbook.Faults[j];
                    if (fault.id == fault_id)
                    {
                        this.Faults.Add(fault);
                    }
                }
            }
        }
    }
}
