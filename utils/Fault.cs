using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Motherboard_Diagnostic
{
    class Fault
    {
        public int id { get; }
        public string name { get;  }
        public string description { get; }
        public Fault(int id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }
    }
}
