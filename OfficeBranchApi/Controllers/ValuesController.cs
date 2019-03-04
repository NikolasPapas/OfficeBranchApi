using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OfficeBranchApi.Controllers
{
    [Route("api/bad")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {

            String heroes1 = "heroes: {{ id: 11, name: 'Mr. Nice', description: 'Nice, gender: 1 },{ id: 12, name: 'Narco', description: 'Narrr', gender: 2 },{ id: 13, name: 'Bombasto', description: 'Bomb', gender: 1 },{ id: 14, name: 'Celeritas', description: 'Celery', gender: 2 },{ id: 15, name: 'Magneta', description: 'Magentifirsjg', gender: 1 },{ id: 16, name: 'RubberMan', description: 'Rubben', gender: 2 },{ id: 17, name: 'Dynama', description: 'Dynamite', gender: 1 },{ id: 18, name: 'Dr IQ', description: 'IQ', gender: 2 },{ id: 19, name: 'Magma', description: 'Lava', gender: 1 },{ id: 20, name: 'Tornado', description: 'Wind', gender: 2 }}";
            String gameHeroes = "gameHeroes: {id: 1,name: 'game1',company: 'company1',heroes:{ { id: 11, name: 'Mr. Nice', description: 'Nice;, gender: 1 },{ id: 12, name: 'Narco', description: 'Narrr', gender: 2 },{ id: 13, name: 'Bombasto', description: 'Bomb', gender: 1 }}},{id: 2,name: 'game2',company: 'company2',heroes: {{ id: 15, name: 'Magneta', description: 'Magentifirsjg', gender: 1 },{ id: 16, name: 'RubberMan', description: 'Rubben', gender: 2 },{ id: 17, name: 'Dynama', description: 'Dynamite', gender: 1 },{ id: 18, name: 'Dr IQ', description: 'IQ', gender: 2 }}},{id: 3,name: 'game3',company: 'company3',heroes:{ { id: 17, name: 'Dynama', description: 'Dynamite', gender: 1 },{ id: 18, name: 'Dr IQ', description: 'IQ', gender: 2 },{ id: 19, name: 'Magma', description: 'Lava', gender: 1 },{ id: 20, name: 'Tornado', description: 'Wind', gender: 2 }}},{id: 4,name: 'game3',company: 'company3',heroes: {{ id: 11, name: 'Mr. Nice', description: 'Nice', gender: 1 },{ id: 12, name: 'Narco', description: 'Narrr', gender: 2 },{ id: 13, name: 'Bombasto', description: 'Bomb', gender: 1 },{ id: 14, name: 'Celeritas', description: 'Celery', gender: 2 },{ id: 15, name: 'Magneta', description: 'Magentifirsjg', gender: 1 },{ id: 16, name: 'RubberMan', description: 'Rubben', gender: 2 },{ id: 17, name: 'Dynama', description: 'Dynamite', gender: 1 },{ id: 18, name: 'Dr IQ', description: 'IQ', gender: 2 },{ id: 19, name: 'Magma', description: 'Lava', gender: 1 },{ id: 20, name: 'Tornado', description: 'Wind', gender: 2 }}},{id: 5,name: 'game4',company: 'company4',heroes: {{ id: 19, name: 'Magma', description: 'Lava', gender: 1 },{ id: 20, name: 'Tornado', description: 'Wind', gender: 2 }}}";
            // return "[{" + heroes1 + "},{" + gameHeroes + "}]";
            return "[{" + heroes1 + "}]";
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
