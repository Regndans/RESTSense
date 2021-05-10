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
        #region motionMethods
        /// <summary>
        /// Method to Get All from Pir-table,
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

        public List<MotionModel> GetAll(int? date = null)
        {
            if (date != null)
            {
                return _context.MotionList.Where(model => model.TimeOfDetection.Day == date).ToList();
            }
            return _context.MotionList.ToList();
        }

        /// <summary>
        /// Method to Add to Pir-table
        /// </summary>
        /// <param name="newPir"></param>
        //TODO validate data
        public MotionModel AddFromSensor(MotionModel newPir)
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
        #region SensorMethods
        public List<SensorModel> GetAllSensors()
        {
           return _context.SensorList.ToList();
        }

        public SensorModel GetById(int id)
        {
            SensorModel sensorM = _context.SensorList.Find(id);
            return sensorM;
        }

        public SensorModel AddSensor(SensorModel newSens)
        {
            _context.SensorList.Add(newSens);
            _context.SaveChanges();
            return newSens;
        }

        public SensorModel DeleteSensor(int id, int ourKey)
        {
            if (ourKey == Secrets.ourKey)
            {
                SensorModel sensToDelete = _context.SensorList.Find(id);
                if (sensToDelete == null) return null;
                _context.SensorList.Remove(sensToDelete);
                _context.SaveChanges();
                return sensToDelete;
            }
            else return null;

        }

        public SensorModel UpdateSensor(int id, SensorModel updatedSensor, int ourKey)
        {
            SensorModel sensorToUpdate = _context.SensorList.Find(id);
            if (sensorToUpdate == null) return null;
            if (ourKey == Secrets.ourKey)
            {
                sensorToUpdate.Active = updatedSensor.Active;
                sensorToUpdate.SensorName = updatedSensor.SensorName;
                _context.SaveChanges();
            }
            return sensorToUpdate;
        }
        #endregion
    }
}
