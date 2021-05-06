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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PirSensorModel>> Get([FromQuery]int? date)
        {
            List<PirSensorModel> allPirs = _manager.GetAll(date);
            if (allPirs.Count == 0) return NotFound("Nothing from that date");
            return Ok(allPirs);
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

        // DELETE api/<PirsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<PirSensorModel> Delete(int id, [FromQuery] int key = 0)
        {
            if (key == Secrets.ourKey)
            {
                PirSensorModel toDelete = _manager.DeleteById(id, key);
                if (toDelete == null) return NotFound("No such Id");
                return Ok(toDelete);
            }
            return BadRequest("Wrong key, try again");
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
