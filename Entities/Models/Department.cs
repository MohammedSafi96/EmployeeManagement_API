using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiesDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
