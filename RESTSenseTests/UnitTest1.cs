using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PirSensor;
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
            List<PirSensor.PirSensor> PirList = _manager.GetAll();
            Assert.AreEqual(1, PirList.Count);

        }
    }
}
