using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CountryRepository
{
   public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(EmployeeManagement_DBContext context) : base(context)
        {
        }
    }
}
