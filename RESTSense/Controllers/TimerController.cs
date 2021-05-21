using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RESTSense.Managers;
using RESTSense.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTSense.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimerController : ControllerBase
    {
        private readonly TimerManager _manager;

        public TimerController(PirContext context)
        {
            _manager = new TimerManager(context);
        }
        /// <summary>
        /// Method to get timer
        /// </summary>
        /// <returns></returns>
        // GET: api/<TimerController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<TimerModel>> Get()
        {
            List<TimerModel> list = _manager.GetAll();
            if (list.Count == 0) return NotFound("Nothing found");
            return Ok(list);


        }
        /// <summary>
        /// Method to put timer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/<TimerController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TimerModel> Put(int id, [FromBody] TimerModel value)
        {
           TimerModel timer = _manager.UpdateTimer(id,value);
           if (timer == null) return NotFound("No data found on id");
           return Ok(timer);
        }

    }
}
