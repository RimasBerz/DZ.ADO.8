using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.EfContext
{
    public class Manager
    { 
        public Guid Id { get; set; }
        public String Surname { get; set; } = null!;
        public String Name { get; set; } = null!;
        public String SecName { get; set; } = null!;
        public Guid Id_main_dep { get; set; }
        public Guid? Id_sec_dep { get; set; }
        public Guid? Id_chief { get; set; }

        public String ToShortString()
        {
            return $"{Id.ToString()[..4]}...  {Surname} {Name[0]}. {SecName[0]}.";
        }
    
    }
}
