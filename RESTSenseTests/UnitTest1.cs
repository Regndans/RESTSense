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

        [TestMethod]
        public void ManagerTest()
        {
            List<PirSensorModel> PirList = _manager.GetAll();
            //Assert.AreEqual(1, PirList.Count);
            PirSensorModel newPir = new PirSensorModel();
            newPir.Name = "Kitchen2";
            newPir.Active = true;
            newPir.Status = "Nothing detected";
            newPir.TimeOfDetection = DateTime.Now;
            _manager.AddFromSensor(newPir);
            int sizeOfPi = PirList.Count();
            PirList = _manager.GetAll();
            Assert.AreEqual(sizeOfPi + 1, PirList.Count);
            PirSensorModel deleteThis = PirList[PirList.Count - 1];
            sizeOfPi = PirList.Count();
            _manager.DeleteById(deleteThis.Id, Secrets.ourKey);
            PirList = _manager.GetAll();
            Assert.AreEqual(sizeOfPi -1, PirList.Count);

            //// Test for DeleteAll
            //_manager.DeleteAll(Secrets.ourKey);
            //PirList = _manager.GetAll();
            //Assert.AreEqual(0, PirList.Count);
        }
    }
}
