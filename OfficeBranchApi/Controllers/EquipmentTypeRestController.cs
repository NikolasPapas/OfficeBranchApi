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
    [Route("api/equipmentstype")]
    [EnableCors("AllowSpecificOrigin")]
    public class EquipmentTypeRestController : Controller
    {

        public readonly IEquipmentTypeService _equipmentTypeService;

        public EquipmentTypeRestController(IEquipmentTypeService context)
        {
            _equipmentTypeService = context;
        }



        // GET: api/<controller>/Employees
        [HttpPost("get/new")]
        public async Task<ResultGet<IEnumerable<EquipmentTypeDto>>> GetEquipmentTypeByNameAsync([FromBody] ResultSet resultSet)
        {
            return await (_equipmentTypeService.GetAllEquipmentTypeByResultSetAsync(resultSet));
        }








        [HttpGet("get/count")]
        public async Task<ActionResult<PagesDto>> GetEmployeesCountAsync()
        {
            PagesDto pagesDto = new PagesDto
            {
                pagesObjects = await _equipmentTypeService.GetCount()
            };
            return Ok(pagesDto);
        }


        // GET: api/<controller>
        [HttpGet("get/{count}/{page}")]
        public IEnumerable<EquipmentTypeDto> GetEquipmentType(int count,int page)
        {
            return  _equipmentTypeService.GetAllEquipmentTypeAsync(count,page);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentTypeDto>> GetAsync(int id)
        {
           
            return Ok(await _equipmentTypeService.GetEquipmentTypeById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]EquipmentTypeDtoCreateUpdate insert)
        {
            _equipmentTypeService.UpdateEquipmentType(insert);
            //_equipmentTypeService.InsertEquipmentType(insert);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]EquipmentTypeDtoCreateUpdate Update)
        {
            Update.EquipmentTypeId = id;
            _equipmentTypeService.UpdateEquipmentType(Update);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _equipmentTypeService.DeleteEquipmentType(id);
        }
    }
}
