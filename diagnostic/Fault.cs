using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Motherboard_Diagnostic
{
    class Fault
    {
        public int Id { get; }
        public string Name { get;  }
        public string Description { get; }
        public Solution Solution { get; }
        public Fault(int id, string name, string description, Solution solution)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Solution = solution;
        }
    }
}
