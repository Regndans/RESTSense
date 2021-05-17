using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense.Managers
{
    public class MotionManager
    {
        private readonly PirContext _context;

        public MotionManager(PirContext context)
        {
            _context = context;
        }
        #region motionMethods
        /// <summary>
        /// Method to Get All from Pir-table,
        /// </summary>
        /// <param name="date"></param>
        /// <param name="sensorId"></param>
        /// <returns></returns>

        public List<MotionModel> GetAll(int? date = null, int? sensorId = null)
        {
            if (date != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Day == date).ToList();
            }
            else if (sensorId != null)
            {
                return _context.MotionList.Where(model => model.SensorId == sensorId).ToList();
            }
            return _context.MotionList.ToList();
        }

        /// <summary>
        /// Method to Add to Pir-table
        /// </summary>
        /// <param name="newPir"></param>
        //TODO validate data
        public MotionModel Add(MotionModel newPir)
        {
            _context.MotionList.Add(newPir);
            _context.SaveChanges();
            return newPir;
        }

        /// <summary>
        /// Method to Delete specific row in Pir-table, key in Secrets
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>

        public MotionModel DeleteById(int id, int key)
        {
            if (key == Secrets.ourKey)
            {
                MotionModel pirToDelete = _context.MotionList.Find(id);
                if (pirToDelete == null) return null;
                _context.MotionList.Remove(pirToDelete);
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
                foreach (var VARIABLE in _context.MotionList)
                {
                    _context.MotionList.Remove(VARIABLE);
                }
                _context.SaveChanges();
            }
        }
        #endregion
    }
}
