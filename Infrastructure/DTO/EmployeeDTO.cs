using Infrastructure.Enums;
using Infrastructure.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
   public class EmployeeDTO
    {
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [Range((int)VlidationNumbersConfig.One, Int32.MaxValue, ErrorMessageResourceName = "ValidationErrorMessage_OutOfRangeNumber", ErrorMessageResourceType = typeof(Resource))]
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        public int DepartmentId { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        public int CountryId { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [MaxLength((int)VlidationNumbersConfig.OneHundred, ErrorMessageResourceName = "ValidationErrorMessage_MaxString100", ErrorMessageResourceType = typeof(Resource))]
        public string FullName { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [Range((int)VlidationNumbersConfig.One, double.MaxValue, ErrorMessageResourceName = "ValidationErrorMessage_OutOfRangeNumber", ErrorMessageResourceType = typeof(Resource))]
        public decimal Salary { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [StringLength((int)VlidationNumbersConfig.Thirteen, MinimumLength = (int)VlidationNumbersConfig.Ten, ErrorMessageResourceName = "ValidationErrorMessage_InvalidPhoneNumber", ErrorMessageResourceType = typeof(Resource))]
        public string Phone { get; set; }

    }
    public class EmployeeAdditionDTO
    {
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        public int DepartmentId { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        public int CountryId { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [MaxLength((int)VlidationNumbersConfig.OneHundred, ErrorMessageResourceName = "ValidationErrorMessage_MaxString100", ErrorMessageResourceType = typeof(Resource))]
        public string FullName { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [Range((int)VlidationNumbersConfig.One, double.MaxValue, ErrorMessageResourceName = "ValidationErrorMessage_OutOfRangeNumber", ErrorMessageResourceType = typeof(Resource))]
        public decimal Salary { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [StringLength((int)VlidationNumbersConfig.Thirteen, MinimumLength = (int)VlidationNumbersConfig.Ten, ErrorMessageResourceName = "ValidationErrorMessage_InvalidPhoneNumber", ErrorMessageResourceType = typeof(Resource))]
        public string Phone { get; set; }
    }
}
