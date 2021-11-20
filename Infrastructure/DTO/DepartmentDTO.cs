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
   public class DepartmentDTO
   {
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [Range((int)VlidationNumbersConfig.One, Int32.MaxValue, ErrorMessageResourceName = "ValidationErrorMessage_OutOfRangeNumber", ErrorMessageResourceType = typeof(Resource))]
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [MaxLength((int)VlidationNumbersConfig.Fifteen, ErrorMessageResourceName = "ValidationErrorMessage_MaxString50", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }
   }
    public class DepartmentAdditionDTO
    {
        [Required(ErrorMessageResourceName = "ValidationErrorMessage_RequiredValue", ErrorMessageResourceType = typeof(Resource))]
        [MaxLength((int)VlidationNumbersConfig.Fifteen,ErrorMessageResourceName = "ValidationErrorMessage_MaxString50", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }
    }
}
