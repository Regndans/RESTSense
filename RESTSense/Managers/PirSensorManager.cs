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
        public List<PirSensorModel> GetAll(int key = 0)
        {
            return _context.Pir.ToList();
        }

        public void AddFromSensor(PirSensorModel newPir)
        {
            _context.Pir.Add(newPir);
            _context.SaveChanges();
        }

        public void DeleteById(int id, int key)
        {
            if (key == Secrets.ourKey)
            {
                PirSensorModel pirToDelete = _context.Pir.Find(id);
                if (pirToDelete == null) return;
                _context.Pir.Remove(pirToDelete);
                _context.SaveChanges();
            }
            else return;
        }
    }
}
