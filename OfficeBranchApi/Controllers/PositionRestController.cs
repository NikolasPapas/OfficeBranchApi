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
    [Route("api/position")]
    [EnableCors("AllowSpecificOrigin")]
    public class PositionRestController : Controller
    {

        public readonly IPositionService _positionService;

        public PositionRestController(IPositionService context)
        {
            _positionService = context;
        }


        // GET: api/<controller>/Employees
        [HttpPost("get/new")]
        public async Task<ResultGet<IEnumerable<PositionDetailsDto>>> GetPositionsByNameAsync([FromBody] ResultSet resultSet)
        {
            return await (_positionService.GetAllPositionsByResultSetAsync(resultSet));
        }



        [HttpGet("get/count")]
        public async Task<ActionResult<PagesDto>> GetEmployeesCountAsync()
        {
            PagesDto pagesDto = new PagesDto
            {
                pagesObjects = await _positionService.GetCount()
            };
            return Ok(pagesDto);
        }


        // GET: api/<controller>
        [HttpGet("get/{count}/{page}")]
        public IEnumerable<PositionDto> GetPosition(int count,int page)
        {
            return  _positionService.GetAllPositionAsync(count,page);
        }

        // GET: api/<controller>
        [HttpGet("all/{count}/{page}")]
        public IEnumerable<PositionDetailsDto> GetPositionPlusEmployees(int count,int page)
        {
            return  _positionService.GetAllPositionPlusAll(count,page);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public  ActionResult<PositionDetailsDto> GetAsync(int id)
        {
            return  _positionService.GetPositionById(id);
        }


        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]PositionDetailsDtoCreateUpdate insert)
        {
            _positionService.UpdatePosition(insert);
            // _positionService.InsertPosition(insert);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody]PositionDetailsDtoCreateUpdate update)
        {
            update.PositionId = id;
            _positionService.UpdatePosition(update);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _positionService.DeletePosition(id);
        }
    }
}
