using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTSense;
using RESTSense.Managers;
using RESTSense.Model;

namespace RESTSenseTests.Managers
{
    [TestClass]
    public class TimerManagerTest
    {
        private TimerManager _manager;
        private readonly PirContext _context;
        public TimerManagerTest()
        {
            DbContextOptionsBuilder<PirContext> options = new DbContextOptionsBuilder<PirContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new PirContext(options.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new TimerManager(_context);
        }

        /// <summary>
        /// Tests getall
        /// </summary>
        [TestMethod]
        public void ManagerTimerGetAllTest()
        {
            List<TimerModel> TimerList = _manager.GetAll();
            int sizeOfList = TimerList.Count();
            Console.WriteLine(sizeOfList);
            Assert.AreEqual(sizeOfList, TimerList.Count);

        }

        /// <summary>
        /// Tests put method
        /// </summary>
        [TestMethod]
        public void ManagerTimerPutTest()
        {
            List<TimerModel> TimerList = _manager.GetAll();
            TimerModel newTime = new TimerModel();
            newTime = TimerList[0];
            newTime.ActiveEnd = DateTime.Now;

            _manager.UpdateTimer(1, newTime);
            TimerList = _manager.GetAll();
            Assert.AreEqual(newTime.ActiveEnd,TimerList[0].ActiveEnd);

        }
    }
}
