using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PirSensor;

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

        public void AddFromSensor(PirSensor.PirSensor newPir)
        {
            _context.Pir.Add(newPir);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            PirSensor.PirSensor pirToDelete = _context.Pir.Find(id);
            if (pirToDelete == null) return;
            _context.Pir.Remove(pirToDelete);
            _context.SaveChanges();
        }
    }
}
