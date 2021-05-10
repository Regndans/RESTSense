using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTSense;
using RESTSense.Controllers;
using RESTSense.Managers;

namespace RESTSenseTests
{
    [TestClass]
    public class ManagerUnitTest
    {
        private PirSensorManager _manager;
        private readonly PirContext _context;
        private SensorController _sensController;

        public ManagerUnitTest()
        {
            DbContextOptionsBuilder<PirContext> options = new DbContextOptionsBuilder<PirContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new PirContext(options.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new PirSensorManager(_context);
            _sensController = new SensorController(_context);
        }

        // Tests for the Manager class

        #region ManagerMotion Tests

        [TestMethod]
        public void ManagerMotionGetAllTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfPi = motionList.Count();
            Assert.AreEqual(sizeOfPi, motionList.Count);

            //// Test for DeleteAll
            //_manager.DeleteAll(Secrets.ourKey);
            //PirList = _manager.GetAll();
            //Assert.AreEqual(0, PirList.Count);
        }

        [TestMethod]
        public void GetMotionByDateTest()
        {
            //Tester getbydate metoden med positivt gyldigt input. 
            List<MotionModel> motionList = _manager.GetAll();
            motionList = _manager.GetAll(07);
            Assert.AreEqual(1, motionList.Count);
            motionList = _manager.GetAll();

            //Tester GetByDate metoden med negativt gyldigt input.
            motionList = _manager.GetAll(01);
            Assert.AreNotEqual(1, motionList.Count);
            motionList = _manager.GetAll();

            //Tester GetByDate metoden med negativt ugyldigt input.
            motionList = _manager.GetAll(-1);
            Assert.AreNotEqual(1, motionList.Count);

            //Tester GetByDate metoden med negativt ugyldigt input (grænseværdi)
            motionList = _manager.GetAll(0);
            Assert.AreNotEqual(1, motionList.Count);
        }

        [TestMethod]
        //Positiv test af Add og Delete metoder, med gyldige input
        public void MangerMotionAddAndDeletePositiveTest()
        {
            //Henter liste
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfMotionList = motionList.Count();

            //Tilføjer ny motion til databasen
            MotionModel newMotion = new MotionModel();
            newMotion.SensorId = 1;
            newMotion.Status = "Nothing detected";
            newMotion.TimeOfDetection = DateTime.Now;
            _manager.AddFromSensor(newMotion);
            //Tester at den nye motion er tilføjet databasen, ved at tjekke størrelsen på array
            motionList = _manager.GetAll();
            Assert.AreEqual(sizeOfMotionList + 1, motionList.Count);

            //Benytter managerens deletemetode til at slette den nye motion fra databasen
            MotionModel deleteThis = motionList[motionList.Count - 1];
            sizeOfMotionList = motionList.Count();
            _manager.DeleteById(deleteThis.MotionId, Secrets.ourKey);
            //henter listen igen
            motionList = _manager.GetAll();
            //Tester at den nye motion er slettet fra databasen, ved at tjekke størrelsen på array
            Assert.AreEqual(sizeOfMotionList - 1, motionList.Count);
        }

        [TestMethod]
        public void ManagerMotionAddNegativeTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfMotionList = motionList.Count();

            MotionModel newMotion = null;
            Assert.ThrowsException<ArgumentNullException>(() => _manager.AddFromSensor(newMotion));
        }

        [TestMethod]
        //Negativ test af Add og Delete metoder med gyldige input
        public void ManagerMotionDeleteNegativeTest()
        {
            //Henter liste
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfMotionList = motionList.Count();
            _manager.DeleteById(-1, Secrets.ourKey);
            //henter listen igen
            motionList = _manager.GetAll();
            //Tester at der ikke er slettet noget fra databasen, ved at assert at metoden smider en exception.
            Assert.AreNotEqual(sizeOfMotionList-1, motionList.Count);
        }

        [TestMethod]
        public void ManagerMotionDeleteInvalidKeyTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfMotionList = motionList.Count;
            _manager.DeleteById(1, 1234);
            motionList = _manager.GetAll();

            Assert.AreNotEqual(sizeOfMotionList-1, motionList);
        }


        #endregion


        [TestMethod]
        public void ManagerSensorTest()
        {
            //Test GetAll metode
            List<SensorModel> allSensors = _manager.GetAllSensors();
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
            List<SensorModel> allSensors = _manager.GetAllSensors();
            SensorModel newSens = new SensorModel();
            newSens.Active = true;
            newSens.SensorName = "test";
            int sizeOfSens = allSensors.Count;
            _manager.AddSensor(newSens);
            allSensors = _manager.GetAllSensors();
            Assert.AreEqual(sizeOfSens + 1, allSensors.Count);

            //Test Delete metode
            _manager.DeleteSensor(newSens.SensorId, Secrets.ourKey);
            allSensors = _manager.GetAllSensors();
            Assert.AreEqual(sizeOfSens, allSensors.Count);
        }

        [TestMethod]
        public void ManagerSensorAddNegativeTest()
        {
            List<SensorModel> sensorList = _manager.GetAllSensors();
            int sizeOfSensorList = sensorList.Count();

            SensorModel newSensor = null;
            Assert.ThrowsException<ArgumentNullException>(() => _manager.AddSensor(newSensor));
        }

        [TestMethod]
        public void ManagerSensorDeleteNegativeTest()
        {
            //Henter liste
            List<SensorModel> sensorList = _manager.GetAllSensors();
            int sizeOfSensorList = sensorList.Count();
            _manager.DeleteSensor(-1, Secrets.ourKey);
            //henter listen igen
            sensorList = _manager.GetAllSensors();
            //Tester at der ikke er slettet noget fra databasen, ved at assert at metoden smider en exception.
            Assert.AreNotEqual(sizeOfSensorList-1, sensorList.Count);
        }

        [TestMethod]
        public void ManagerSensorUpdatePositiveTest()
        {
            //Test Update metode
            List<SensorModel> allSensors = _manager.GetAllSensors();
            SensorModel sensorById = _manager.GetById(2);
            string updateName = "Carport";

            SensorModel updatedSensor = new SensorModel();
            updatedSensor.Active = true;
            updatedSensor.SensorName = updateName;
            _manager.UpdateSensor(2, updatedSensor, Secrets.ourKey);
            sensorById = _manager.GetById(2);
            Assert.AreEqual(updateName, sensorById.SensorName);
        }

        [TestMethod]
        public void ManagerSensorDeleteInvalidKeyTest()
        {
            List<SensorModel> sensorList = _manager.GetAllSensors();
            int sizeOfMotionList = sensorList.Count;
            _manager.DeleteById(1, 1234);
            sensorList = _manager.GetAllSensors();

            Assert.AreNotEqual(sizeOfMotionList - 1, sensorList);
        }


    }
}
