using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motherboard_Diagnostic
{
    
    class Solution
    {
        public int id { get;}
        public string description { get;}

        public Solution(int id, string description)
        {
            this.id = id;
            this.description = description;
        }
    }
}
