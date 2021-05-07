using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTSense;
using RESTSense.Managers;

namespace RESTSenseTests
{
    [TestClass]
    public class UnitTest1
    {
        private PirSensorManager _manager;
        private readonly PirContext _context;

        public UnitTest1()
        {
            DbContextOptionsBuilder<PirContext> options = new DbContextOptionsBuilder<PirContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new PirContext(options.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new PirSensorManager(_context);
        }

        // Tests for the Manager class

        [TestMethod]
        public void ManagerMotionTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfPi = motionList.Count();
            Assert.AreEqual(sizeOfPi, motionList.Count);
            MotionModel newPir = new MotionModel();
            newPir.SensorId = 1;
            newPir.Status = "Nothing detected";
            newPir.TimeOfDetection = DateTime.Now;
            _manager.AddFromSensor(newPir);
            sizeOfPi = motionList.Count();
            motionList = _manager.GetAll();
            Assert.AreEqual(sizeOfPi + 1, motionList.Count);


            MotionModel deleteThis = motionList[motionList.Count - 1];
            sizeOfPi = motionList.Count();
            _manager.DeleteById(deleteThis.MotionId, Secrets.ourKey);
            motionList = _manager.GetAll();
            Assert.AreEqual(sizeOfPi - 1, motionList.Count);

            //Tester getbydate metoden
            motionList = _manager.GetAll(07);
            Assert.AreEqual(1, motionList.Count);
            motionList = _manager.GetAll();

            //// Test for DeleteAll
            //_manager.DeleteAll(Secrets.ourKey);
            //PirList = _manager.GetAll();
            //Assert.AreEqual(0, PirList.Count);
        }

        [TestMethod]
        public void ManagerSensorTest()
        {
            List<SensorModel> allSensors = _manager.GetAllSensors();
            int sizeOfSensor = allSensors.Count();
            Assert.AreEqual(sizeOfSensor, allSensors.Count);

            SensorModel newSens = new SensorModel();
            newSens.Active = true;
            newSens.SensorName = "test";
            int sizeOfSens = allSensors.Count;
            _manager.AddSensor(newSens);
            allSensors = _manager.GetAllSensors();
            Assert.AreEqual(sizeOfSens+1,allSensors.Count);

            _manager.DeleteSensor(newSens.SensorId,Secrets.ourKey);
            allSensors = _manager.GetAllSensors();
            Assert.AreEqual(sizeOfSens,allSensors.Count);

        }
    }
}
