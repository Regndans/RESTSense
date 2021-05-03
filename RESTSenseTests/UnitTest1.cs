using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PirSensor;
using RESTSense.Managers;

namespace RESTSenseTests
{
    [TestClass]
    public class UnitTest1
    {
        private PirSensorManager _manager;

        [TestInitialize]
        public void Init()
        { 
            _manager = new PirSensorManager();
        }

        [TestMethod]
        public void ManagerTest()
        {
            List<PirSensor.PirSensor> PirList = _manager.GetAll();
            Assert.AreEqual(0, PirList.Count);
        }
    }
}
