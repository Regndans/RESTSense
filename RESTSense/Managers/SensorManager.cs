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
        public List<SensorModel> GetAll()
        {
            return _context.SensorList.ToList();
        }

        public SensorModel GetById(int id)
        {
            SensorModel sensorM = _context.SensorList.Find(id);
            return sensorM;
        }

        public SensorModel Add(SensorModel newSens)
        {
            _context.SensorList.Add(newSens);
            _context.SaveChanges();
            return newSens;
        }

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
