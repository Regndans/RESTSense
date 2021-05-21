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
        /// Method to Get All from Motion-table,
        /// </summary>
        /// <param name="date">Date of month to filter by</param>
        /// <param name="month">Month to filter by</param>
        /// <param name="year">Year to filter by</param>
        /// <param name="sensorId">Specific sensor to filter by</param>
        /// <returns></returns>
        public List<MotionModel> GetAll(int? date = null, int? month = null, int? year = null, int? sensorId = null)
        {
            if (date != null && month != null && year != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Day == date && 
                                                          model.TimeOfDetection.Month == month && model.TimeOfDetection.Year == year).ToList();
            }
            else if (date != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Day == date).ToList();
            }
            else if (month != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Month == month).ToList();
            }
            else if (year != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Year == year).ToList();
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
        /// <param name="value"></param>
        //TODO validate data
        public MotionModel Add(MotionModel value)
        {
            _context.MotionList.Add(value);
            _context.SaveChanges();
            return value;
        }

        /// <summary>
        /// Method to Delete specific row in motion-table, key in Secrets
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        public MotionModel DeleteById(int id, int key)
        {
            if (key == Secrets.ourKey)
            {
                MotionModel motionToDelete = _context.MotionList.Find(id);
                if (motionToDelete == null) return null;
                _context.MotionList.Remove(motionToDelete);
                _context.SaveChanges();
                return motionToDelete;
            }
            else return null;
        }

        /// <summary>
        /// Method to Delete All in motion-table, key in Secrets
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
