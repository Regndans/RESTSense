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
    public class MotionManagerTests
    {
        private MotionManager _manager;
        private readonly PirContext _context;

        public MotionManagerTests()
        {
            DbContextOptionsBuilder<PirContext> options = new DbContextOptionsBuilder<PirContext>();
            options.UseSqlServer(Secrets.ConnectionString);
            _context = new PirContext(options.Options);
        }

        [TestInitialize]
        public void Init()
        {
            _manager = new MotionManager(_context);
        }

        #region Positive tests

        [TestMethod]
        public void ManagerMotionGetAllTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfList = motionList.Count();
            Assert.AreEqual(sizeOfList, motionList.Count);

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
            _manager.Add(newMotion);
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

        #endregion

        #region Negative tests


        [TestMethod]
        public void ManagerMotionAddNegativeTest()
        {
            List<MotionModel> motionList = _manager.GetAll();
            int sizeOfMotionList = motionList.Count();

            MotionModel newMotion = null;
            Assert.ThrowsException<ArgumentNullException>(() => _manager.Add(newMotion));
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
    }
}
