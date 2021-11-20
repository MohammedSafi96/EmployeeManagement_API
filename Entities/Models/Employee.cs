using System;
using System.Collections.Generic;

#nullable disable

namespace Infrastructure
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int CountryId { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiesDate { get; set; }

        public virtual Country Country { get; set; }
        public virtual Department Department { get; set; }
    }
}
