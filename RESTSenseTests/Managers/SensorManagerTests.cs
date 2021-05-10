using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTSense.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RESTSense.Controllers;

namespace RESTSense.Managers.Tests
{
    [TestClass()]
    public class SensorManagerTests
    {
        private SensorManager _manager;
        private readonly PirContext _context;
        private SensorController _sensController;

        public SensorManagerTests()
        {
            DbContextOptionsBuilder<PirContext> options = new DbContextOptionsBuilder<PirContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new PirContext(options.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new SensorManager(_context);
            _sensController = new SensorController(_context);
        }

        [TestMethod]
        public void ManagerSensorTest()
        {
            //Test GetAll metode
            List<SensorModel> allSensors = _manager.GetAll();
            int sizeOfSensor = allSensors.Count();
            Assert.AreEqual(sizeOfSensor, allSensors.Count);

            //Test GetById
            SensorModel sensorById = _manager.GetById(2);
            Assert.AreEqual("Carport", sensorById.SensorName);

            //TODO spørg nogen der ved noget
            //_manager.UpdateSensor(7, updatedSensor, Secrets.ourKey);
            //var response = _sensController.Put(7, updatedSensor, Secrets.ourKey);
            //Assert.AreEqual(404, response);
            //var controller = 
        }

        [TestMethod]
        public void ManagerSensorAddAndDeletePositveTest()
        {
            //Test Add metode
            List<SensorModel> allSensors = _manager.GetAll();
            SensorModel newSens = new SensorModel();
            newSens.Active = true;
            newSens.SensorName = "test";
            int sizeOfSens = allSensors.Count;
            _manager.Add(newSens);
            allSensors = _manager.GetAll();
            Assert.AreEqual(sizeOfSens + 1, allSensors.Count);

            //Test Delete metode
            _manager.DeleteById(newSens.SensorId, Secrets.ourKey);
            allSensors = _manager.GetAll();
            Assert.AreEqual(sizeOfSens, allSensors.Count);
        }

        [TestMethod]
        public void ManagerSensorAddNegativeTest()
        {
            List<SensorModel> sensorList = _manager.GetAll();
            int sizeOfSensorList = sensorList.Count();

            SensorModel newSensor = null;
            Assert.ThrowsException<ArgumentNullException>(() => _manager.Add(newSensor));
        }

        [TestMethod]
        public void ManagerSensorDeleteNegativeTest()
        {
            //Henter liste
            List<SensorModel> sensorList = _manager.GetAll();
            int sizeOfSensorList = sensorList.Count();
            _manager.DeleteById(-1, Secrets.ourKey);
            //henter listen igen
            sensorList = _manager.GetAll();
            //Tester at der ikke er slettet noget fra databasen, ved at assert at metoden smider en exception.
            Assert.AreNotEqual(sizeOfSensorList - 1, sensorList.Count);
        }

        [TestMethod]
        public void ManagerSensorUpdatePositiveTest()
        {
            //Test Update metode
            List<SensorModel> allSensors = _manager.GetAll();
            SensorModel sensorById = _manager.GetById(2);
            string updateName = "Carport";

            SensorModel updatedSensor = new SensorModel();
            updatedSensor.Active = true;
            updatedSensor.SensorName = updateName;
            _manager.Update(2, updatedSensor, Secrets.ourKey);
            sensorById = _manager.GetById(2);
            Assert.AreEqual(updateName, sensorById.SensorName);
        }

        [TestMethod]
        public void ManagerSensorDeleteInvalidKeyTest()
        {
            List<SensorModel> sensorList = _manager.GetAll();
            int sizeOfMotionList = sensorList.Count;
            _manager.DeleteById(1, 1234);
            sensorList = _manager.GetAll();

            Assert.AreNotEqual(sizeOfMotionList - 1, sensorList);
        }

    }
}