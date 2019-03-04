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
    [Route("api/officebranch")]
    [EnableCors("AllowSpecificOrigin")]
    public class OfficeBranchRestcontroller : Controller
    {

        public readonly IOfficeBranchService _officeBranchService;

        public OfficeBranchRestcontroller(IOfficeBranchService context)
        {
            _officeBranchService = context;
        }



        // GET: api/<controller>/Employees
        [HttpPost("get/new")]
        public async Task<ResultGet<IEnumerable<OfficeBranchDto>>> GetOfficeBranchByNameAsync([FromBody] ResultSet resultSet)
        {
            return await (_officeBranchService.GetAllOfficeBranchByResultSetAsync(resultSet));
        }



        [HttpGet("get/count")]
        public async Task<ActionResult<PagesDto>> GetEmployeesCountAsync()
        {
            PagesDto pagesDto = new PagesDto
            {
                pagesObjects = await _officeBranchService.GetCount()
            };
            return Ok(pagesDto);
        }


        // GET: api/<controller>
        [HttpGet("get/{count}/{page}")]
        public IEnumerable<OfficeBranchDto> GetOfficeBranchAsync(int count,int page)
        {
            return  _officeBranchService.GetAllOfficeBranchAsync(count,page);
        }
        // GET: api/<controller>
        [HttpGet("all/{count}")]
        public IEnumerable<OfficeBranchDetailsDto> GetOfficeBranchPlusPositionsAsync(int count,int page)
        {
            return _officeBranchService.GetAllOfficeBranchPlusPositionsAsync(count,page);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfficeBranchDetailsDto>> GetAsync(int id)
        {
            return Ok(await _officeBranchService.GetOfficeBranchById(id));
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]OfficeBranchDtoCreateAllUpdate insert)
        {
            _officeBranchService.InsertOfficeBranch(insert);
        }

        //// POST api/<controller>
        //[HttpPost("All")]
        //public void PostAll([FromBody]OfficeBranchDtoCreateAllUpdate insert)
        //{
        //    _officeBranchService.InsertOfficeBranchAll(insert);
        //}

        // PUT api/<controller>
        [HttpPut("{id}")]
        public void PuttAll(int id, [FromBody]OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            UpdateInsert.OfficeBranchId = id;
            _officeBranchService.UpdateOfficeBranchDynamic(UpdateInsert);
        }

        // POST api/<controller>
        [HttpPost("all")]
        public void PostAll([FromBody]OfficeBranchDtoCreateAllUpdate UpdateInsert)
        {
            _officeBranchService.UpdateOfficeBranchDynamic(UpdateInsert);
        }

        

        // POST api/<controller>
        [HttpPost("all/full")]
        public void PostAllFull([FromBody]OfficeBranchDtoCreateAllUpdateFull UpdateInsert)
        {
            _officeBranchService.UpdateOfficeBranchDynamicFull(UpdateInsert);
        }
        //// PUT api/<controller>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]OfficeBranchDtoCreateUpdate update)
        //{

        //    _officeBranchService.UpdateOfficeBranch(update);
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _officeBranchService.DeleteOfficeBranch(id);
        }
    }
}
