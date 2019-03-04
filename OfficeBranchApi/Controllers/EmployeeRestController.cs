using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeBranchApi.Condext;
using OfficeBranchApi.DTO;
using OfficeBranchApi.models;
using OfficeBranchApi.Service;

namespace OfficeBranchApi.Controllers
{
    [Route("api/employee")]
    [EnableCors("AllowSpecificOrigin")]
    public class EmployeeRestController : ControllerBase
    {

        public readonly IEmployeeRestService _service;

        public EmployeeRestController(IEmployeeRestService context)
        {
            _service = context;
        }

       

        // GET: api/<controller>/Employees
        [HttpPost("get/new")]
        public async Task<ResultGet<IEnumerable<EmployeeDto>>> GetEmployeesByNameAsync([FromBody] ResultSet resultSet)
        {
            return await( _service.GetAllEmployeesByResultSetAsync(resultSet));
        }

        // GET: api/<controller>/Employees
        [HttpPost("get/noposi")]
        public async Task<ResultGet<IEnumerable<EmployeeDto>>> GetEmployeesByNameAsyncNoPosition([FromBody] ResultSet resultSet)
        {
            return await (_service.GetAllEmployeesByResultSetAsyncNoPosition(resultSet));
        }



        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult<EmployeeDetailDto> Get(int id)
        {
            return Ok( _service.GetEmployeeById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]EmployeeDtoCreateUpdate emp)
        {
            _service.UpdateEmployee(emp);
            //_service.InsertEmployee(emp);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromRoute]int id, [FromBody]EmployeeDtoCreateUpdate update)
        {
            update.EmployeeId = id;
            _service.UpdateEmployee(update);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteEmployee(id);
        }
    }
}
