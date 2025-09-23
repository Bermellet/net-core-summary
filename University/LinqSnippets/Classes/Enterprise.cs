using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets.Classes
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Employee[] Employees { get; set; } = [];
    }
}
