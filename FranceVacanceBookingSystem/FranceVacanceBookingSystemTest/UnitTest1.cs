using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FranceVacanceBookingSystem.Model;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace FranceVacanceBookingSystemTest
{
    [TestClass]
    public class KundeTest
    {
        private Kunde _kunde;

        [TestInitialize]
        public void Before()
        {
            _kunde = new Kunde("TestVej", "test@test.dk", "TestKunde", "12345678", "TestUser", "TestPassword");
        }

        [TestMethod]
        public void CheckNameTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _kunde.Name = null);
            Assert.ThrowsException<ArgumentException>(() => _kunde.Name = " ");
            _kunde.Name = "Test";
            Assert.AreEqual("Test", _kunde.Name);
        }

        [TestMethod]
        public void CheckAdressTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _kunde.Adress = null);
            Assert.ThrowsException<ArgumentException>(() => _kunde.Adress = " ");
            _kunde.Adress = "Testvejen 10";
            Assert.AreEqual("Testvejen 10", _kunde.Adress);
        }

        [TestMethod]
        public void CheckTlfTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _kunde.Tlf = null);
            Assert.ThrowsException<ArgumentException>(() => _kunde.Tlf = " ");
            Assert.ThrowsException<ArgumentException>(() => _kunde.Tlf = "123");
            Assert.ThrowsException<ArgumentException>(() => _kunde.Tlf = "123456789");
            _kunde.Tlf = "12345678";
            Assert.AreEqual("12345678", _kunde.Tlf);
        }

        [TestMethod]
        public void CheckEmailTest()
        {
            Assert.ThrowsException<ArgumentException>(() => _kunde.Email = null);
            Assert.ThrowsException<ArgumentException>(() => _kunde.Email = " ");
            _kunde.Email = "Test@test.dk";
            Assert.AreEqual("Test@test.dk", _kunde.Email);
        }

    }
}
