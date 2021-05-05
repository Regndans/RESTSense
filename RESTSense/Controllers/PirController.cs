using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RESTSense.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTSense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PirController : ControllerBase
    {
        private readonly PirSensorManager _manager;

        public PirController(PirContext context)
        {
            _manager = new PirSensorManager(context);
        }

        // GET: api/<PirsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<PirSensorModel> Get()
        {
            return _manager.GetAll();
        }

        // GET api/<PirsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PirsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public void Post([FromBody] PirSensorModel value)
        {
            _manager.AddFromSensor(value);
        }

        // PUT api/<PirsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PirsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Delete(int id, [FromQuery] int key = 0)
        {
            _manager.DeleteById(id, key);
        }
    }
}
