using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public enum ActionBy
    {
        Anonymous_User
    }
   public enum VlidationNumbersConfig
    {    One = 1,
        OneHundred = 100,
        Fifteen = 50,
        Thirteen =13,
        Ten = 10
    }
    public enum ResponseCode
    {
        Success = 1,
        Failed = 2,
        ObjectNotFound = 3,
        ObjectLinkedWithEmployee = 4,
        SelectedCountryIDNotExist = 5,
        SelectedDepartmentIDNotExist = 6,

    }
}
