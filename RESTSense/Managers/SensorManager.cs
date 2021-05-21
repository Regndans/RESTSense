using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTSense.Managers
{
    public class SensorManager
    {
        private readonly PirContext _context;

        public SensorManager(PirContext context)
        {
            _context = context;
        }

        #region SensorMethods
        /// <summary>
        /// Method to get all from Sensor-table
        /// </summary>
        /// <returns></returns>
        public List<SensorModel> GetAll()
        {
            return _context.SensorList.ToList();
        }
        /// <summary>
        /// Method to get specific sensor from Sensor-table
        /// </summary>
        /// <param name="id">SensorId to find</param>
        /// <returns>Specific sensor</returns>
        public SensorModel GetById(int id)
        {
            SensorModel sensorM = _context.SensorList.Find(id);
            return sensorM;
        }
        /// <summary>
        /// Method to add sensor to Sensor-table
        /// </summary>
        /// <param name="newSens">Object to add</param>
        /// <returns>Added object</returns>
        public SensorModel Add(SensorModel newSens)
        {
            _context.SensorList.Add(newSens);
            _context.SaveChanges();
            return newSens;
        }
        /// <summary>
        /// Method to delete specific sensor from sensor-table
        /// </summary>
        /// <param name="id">SensorId to find and delete by</param>
        /// <param name="ourKey"></param>
        /// <returns>Deleted object</returns>
        public SensorModel DeleteById(int id, int ourKey)
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
        /// <summary>
        /// Method to update specific sensor in sensor-table
        /// </summary>
        /// <param name="id">SensorId to find and update</param>
        /// <param name="updatedSensor">Object to put in place of old</param>
        /// <param name="ourKey"></param>
        /// <returns>Updated object</returns>
        public SensorModel Update(int id, SensorModel updatedSensor, int ourKey)
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
