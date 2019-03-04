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
    [Route("api/equipments")]
    [EnableCors("AllowSpecificOrigin")]
    public class EquipmentsRestController : Controller
    {

        public readonly IEquipmentsRestService _equipmentsRestService;

        public EquipmentsRestController(IEquipmentsRestService equipmentsRestService)
        {
            _equipmentsRestService = equipmentsRestService;
        }




        // GET: api/<controller>/Employees
        [HttpPost("get/new")]
        public async Task<ResultGet<IEnumerable<EquipmentDetailDto>>> GetEquipmentByNameAsync([FromBody] ResultSet resultSet)
        {
            return await (_equipmentsRestService.GetAllEquipmentsByResultSetAsync(resultSet));
        }

        //// GET: api/<controller>/Employees
        //[HttpPost("get/new")]
        //public async Task<ResultGet<IEnumerable<EquipmentDto>>> GetEquipmentByNameAsync([FromBody] ResultSet resultSet)
        //{
        //    return await (_equipmentsRestService.GetAllEquipmentsByResultSetAsync(resultSet));
        //}




        [HttpGet("get/count")]
        public async Task<ActionResult<PagesDto>> GetEquipmentCountAsync()
        {
            PagesDto pagesDto = new PagesDto
            {
                pagesObjects = await _equipmentsRestService.GetCount()
            };
            return Ok(pagesDto);
        }

        // GET: api/<controller>
        [HttpGet("get/{count}/{page}")]
        public IEnumerable<EquipmentDto> GetEquipment(int count,int page)
        {
            return _equipmentsRestService.GetAllEquipments(count,page);
        }

        // GET: api/<controller>
        [HttpGet("all/{count}/{page}")]
        public IEnumerable<EquipmentDetailDto> GetEquipmentPlusType(int count, int page)
        {
            return _equipmentsRestService.GetAllEquipmentsPlusType(count, page);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDetailDto>> Get(int id)
        {
            return Ok(await _equipmentsRestService.GetEquipmentById(id));
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]EquipmentDtoCreateUpdate insert)
        {
            _equipmentsRestService.UpdateEquipment(insert);
            //_equipmentsRestService.InsertEquipment(insert);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]EquipmentDtoCreateUpdate update)
        {
            update.EquipmentId = id;
            _equipmentsRestService.UpdateEquipment(update);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _equipmentsRestService.DeleteEquipment(id);
        }
    }
}
