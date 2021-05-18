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
    public class MotionController : ControllerBase
    {
        private readonly MotionManager _manager;

        public MotionController(PirContext context)
        {
            _manager = new MotionManager(context);
        }

        // GET: api/<MotionController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<MotionModel>> Get([FromQuery]int? date, [FromQuery] int? month, [FromQuery] int? year, [FromQuery] int? sensorId)
        {
            List<MotionModel> allPirs = _manager.GetAll(date, month, year, sensorId);
            if (allPirs.Count == 0) return NotFound("Nothing found");
            return Ok(allPirs);
        }

        // POST api/<MotionController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MotionModel> Post([FromBody] MotionModel value)
        {
            try
            {
                MotionModel ToPost = _manager.Add(value);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + ToPost.MotionId;
                return Created(uri, ToPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<MotionController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<MotionModel> Delete(int id, [FromQuery] int key = 0)
        {
            if (key == Secrets.ourKey)
            {
                MotionModel toDelete = _manager.DeleteById(id, key);
                if (toDelete == null) return NotFound("No such Id");
                return Ok(toDelete);
            }
            return Unauthorized("Wrong key, try again");
        }

        //DELETE api/<MotionController>
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
