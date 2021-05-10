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

        private readonly PirSensorManager _manager;

        public SensorController(PirContext context)
        {
            _manager = new PirSensorManager(context);
        }

        // GET: api/<SensorController>
        [HttpGet("/sens")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SensorModel>> GetSensors()
        {
            List<SensorModel> allSens = _manager.GetAllSensors();
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
                SensorModel ToPost = _manager.AddSensor(newSens);
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
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<SensorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
