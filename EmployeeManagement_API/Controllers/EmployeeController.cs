using EmployeeManagement_BLL;
using Infrastructure.DTO;
using Infrastructure.Enums;
using Infrastructure.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Contructor & Properties
        private readonly EmployeeBLL BLL;
        public EmployeeController(EmployeeBLL _BLL)
        {
            BLL = _BLL;
        }
        #endregion
        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            List<EmployeeDTO> collection = BLL.Get();
            if (collection.Count > 0)
                return Ok(collection);
            else
                return NotFound(Resource.NoDataFound);
        }
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            EmployeeDTO model = BLL.Get(id);
            if (model is not null)
                return Ok(model);
            else
                return NotFound(Resource.NoDataFound);
        }
        #endregion
        #region POST
        [HttpPost]
        public IActionResult Create([FromBody] EmployeeAdditionDTO model)
        {
            ResponseCode res = BLL.Create(model);
            switch (res)
            {
                case ResponseCode.Success:
                    return Ok(Resource.EmployeeAdded);
                case ResponseCode.Failed:
                    return BadRequest(Resource.InternalServerError);
                case ResponseCode.SelectedCountryIDNotExist:
                    return Ok(Resource.SelectedCountryIDNotExist);
                case ResponseCode.SelectedDepartmentIDNotExist:
                    return Ok(Resource.SelectedDepartmentIDNotExist);
                default:
                    return BadRequest(Resource.InternalServerError);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] EmployeeDTO model)
        {
            ResponseCode res = BLL.Edit(model);
            switch (res)
            {
                case ResponseCode.Success:
                    return Ok(Resource.EmployeeUpdated);
                case ResponseCode.Failed:
                    return BadRequest(Resource.InternalServerError);
                case ResponseCode.ObjectNotFound:
                    return Ok(Resource.ObjectNotFound);
                default:
                    return BadRequest(Resource.InternalServerError);
            }
        }
        [HttpDelete("{id:int:min(1)}")]
        public IActionResult Delete(int id)
        {
            ResponseCode res = BLL.Delete(id);
            switch (res)
            {
                case ResponseCode.Success:
                    return Ok(Resource.EmployeeDeleted);
                case ResponseCode.Failed:
                    return BadRequest(Resource.InternalServerError);
                case ResponseCode.ObjectNotFound:
                    return Ok(Resource.ObjectNotFound);
                default:
                    return BadRequest(Resource.InternalServerError);
            }
        }
        #endregion
    }
}
