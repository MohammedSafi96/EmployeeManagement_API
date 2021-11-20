using EmployeeManagement_BLL;
using Infrastructure.DTO;
using Infrastructure.Enums;
using Infrastructure.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        #region Contructor & Properties
        private readonly DepartmentBLL BLL;
        public DepartmentController(DepartmentBLL _BLL)
        {
            BLL = _BLL;
        }
        #endregion
        #region GET
        [HttpGet]
        public IActionResult Get()
        {
            List<DepartmentDTO> collection = BLL.Get();
            if (collection.Count > 0)
                return Ok(collection);
            else
                return NotFound(Resource.NoDataFound);
        }
        [HttpGet("{id:int:min(1)}")]
        public IActionResult Get(int id)
        {
            DepartmentDTO model = BLL.Get(id);
            if (model is not null)
                return Ok(model);
            else
                return NotFound(Resource.NoDataFound);
        }
        #endregion
        #region POST
        [HttpPost]
        public IActionResult Create([FromBody] DepartmentAdditionDTO model)
        {
            if (BLL.Create(model))
                return Ok(Resource.DepartmentAdded);

            return BadRequest(Resource.InternalServerError);
        }
        [HttpPut]
        public IActionResult Edit([FromBody] DepartmentDTO model)
        {
            ResponseCode res = BLL.Edit(model);
            switch (res)
            {
                case ResponseCode.Success:
                    return Ok(Resource.DepartmentUpdated);
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
                    return Ok(Resource.DepartmentDeleted);
                case ResponseCode.Failed:
                    return BadRequest(Resource.InternalServerError);
                case ResponseCode.ObjectNotFound:
                    return Ok(Resource.ObjectNotFound);
                case ResponseCode.ObjectLinkedWithEmployee:
                    return Ok(Resource.ObjectLinkedWithEmployee);
                default:
                    return BadRequest(Resource.InternalServerError);
            }
        }
        #endregion
    }
}
