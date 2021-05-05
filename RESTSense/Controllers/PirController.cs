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
        [HttpGet("{Date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IEnumerable<PirSensorModel> GetByDate(string date)
        {
            return _manager.GetByDate(date);
        }

        // POST api/<PirsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PirSensorModel> Post([FromBody] PirSensorModel value)
        {
            try
            {
                PirSensorModel ToPost = _manager.AddFromSensor(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + ToPost.Id;
                return Created(uri, ToPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
            
        }

        // PUT api/<PirsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<PirsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PirSensorModel> Delete(int id, [FromQuery] int key = 0)
        {
            PirSensorModel toDelete = _manager.DeleteById(id, key);
            if (toDelete == null) return NotFound("No such Id");
            return Ok(toDelete);
        }

        //DELETE api/<PirsController>
        [HttpDelete("DeleteAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteAll([FromQuery] int key = 0)
        {
            if (key == Secrets.ourKey)
            {
                _manager.DeleteAll(key);
                return Ok("Everything deleted");
            }

            return BadRequest("Wrong Key, try again");


        }
    }
}
