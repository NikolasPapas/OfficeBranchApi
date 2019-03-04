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
using OfficeBranchApi.Models;
using OfficeBranchApi.Service;


namespace OfficeBranchApi.Controllers
{
    [Route("api/PositionToEquipment")]
    [EnableCors("AllowSpecificOrigin")]
    public class PositionToEquipmentController
    {
        public readonly IPositionToEquipmentService _service;

        public PositionToEquipmentController(IPositionToEquipmentService context)
        {
            _service = context;
        }

        [HttpGet("get/count")]
        public async Task<ActionResult<PagesDto>> GetEmployeesCountAsync()
        {
            PagesDto pagesDto = new PagesDto
            {
                pagesObjects = await _service.GetCount()
            };
            return Ok(pagesDto);
        }

        private ActionResult<PagesDto> Ok(PagesDto pagesDto)
        {
            throw new NotImplementedException();
        }


        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<PositionToEquipment> GetPositionToEquipment()
        {
            return _service.GetAllPositionToEquipment();
        }

        // GET: api/<controller>
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionToEquipment>> GetPositionToEquipment(int id)
        {
            return await _service.GetPositionToEquipment(id);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]PositionToEquipment insert)
        {
            _service.UpdatePositionToEquipment(insert);
            // _service.InsertPositionToEquipment(insert);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put([FromRoute]int id, [FromBody]PositionToEquipment update)
        {
            _service.UpdatePositionToEquipment(update);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeletePositionToEquipment(id);
        }


    }
}
