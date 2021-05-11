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
    public class SensorController : ControllerBase
    {

        private readonly SensorManager _manager;

        public SensorController(PirContext context)
        {
            _manager = new SensorManager(context);
        }

        // GET: api/<SensorController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SensorModel>> GetSensors()
        {
            List<SensorModel> allSens = _manager.GetAll();
            if (allSens.Count == 0) return NotFound("Nothing found");
            return Ok(allSens);
        }

        // GET api/<SensorController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SensorModel> GetById(int id)
        {
            SensorModel sensorM = _manager.GetById(id);
            if (sensorM == null) return NotFound("Sensor not found.");
            return Ok(sensorM);
        }


        // POST api/<SensorController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SensorModel> Post([FromBody] SensorModel newSens)
        {
            try
            {
                SensorModel ToPost = _manager.Add(newSens);
                string uri = Url.RouteUrl(RouteData.Values) + "/" + ToPost.SensorId;
                return Created(uri, ToPost);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SensorController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<SensorModel> Put(int id, [FromBody] SensorModel updateSens, [FromQuery] int key)
        {
            if (key == Secrets.ourKey)
            {
                SensorModel sensorModelToUpdate = _manager.GetById(id);
                if (sensorModelToUpdate == null) NotFound("No Sensor found");
                SensorModel updatedSensor = _manager.Update(id, updateSens, key);
                return Ok(updatedSensor);
            }
            return Unauthorized("Wrong key, try again");
        }

        // DELETE api/<SensorController>/5
        [HttpDelete("{id}")]
        public ActionResult<SensorModel> Delete(int id, [FromQuery] int key = 0)
        {
            if (key == Secrets.ourKey)
            {
                SensorModel toDelete = _manager.DeleteById(id, key);
                if (toDelete == null) return NotFound("No such Id");
                return Ok(toDelete);
            }
            return Unauthorized("Wrong key, try again");
        }
    }
}
