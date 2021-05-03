using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense.Managers
{
    public class PirSensorManager
    {
        private readonly PirContext _context;

        public PirSensorManager(PirContext context)
        {
            _context = context;
        }
        public List<PirSensor.PirSensor> GetAll()
        {
            return _context.Pir.ToList();
        }
    }
}
