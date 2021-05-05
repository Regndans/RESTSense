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

        /// <summary>
        /// Method to Get All from Pir-table, key in Secrets
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public List<PirSensorModel> GetAll(int key = 0)
        {
            return _context.Pir.ToList();
        }

        /// <summary>
        /// Method to Add to Pir-table
        /// </summary>
        /// <param name="newPir"></param>

        public void AddFromSensor(PirSensorModel newPir)
        {
            _context.Pir.Add(newPir);
            _context.SaveChanges();
        }

        /// <summary>
        /// Method to Delete specific row in Pir-table, key in Secrets
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>

        public PirSensorModel DeleteById(int id, int key)
        {
            if (key == Secrets.ourKey)
            {
                PirSensorModel pirToDelete = _context.Pir.Find(id);
                if (pirToDelete == null) return null;
                _context.Pir.Remove(pirToDelete);
                _context.SaveChanges();
                return pirToDelete;
            }
            else return null;
        }

        /// <summary>
        /// Method to Delete All in Pir-table, key in Secrets
        /// </summary>
        /// <param name="ourKey"></param>

        public void DeleteAll(int ourKey)
        {
            if (ourKey == Secrets.ourKey)
            {
                foreach (var VARIABLE in _context.Pir)
                {
                    _context.Pir.Remove(VARIABLE);
                }
                _context.SaveChanges();
            }
        }
    }
}
