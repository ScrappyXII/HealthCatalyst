using NUnit.Framework;
using System;
using Manza.HealthCatalyst.Models;

namespace HealthCatalystTests
{
    [TestFixture]
    public class PersonModelTests
    {
        [Test]
        public void PersonAgeTest()
        {
            var person = new Person();

            person.BirthDate = new DateTime(1970, 10, 15);

            Assert.AreEqual("48", person.Age);
        }

        [Test]
        public void PersonFullNameTest()
        {
            var person = new Person();

            person.FirstName = "Marc";
            person.LastName = "Manza";

            Assert.AreEqual("Marc Manza", person.FullName);
        }

        [Test]
        public void PersonAddressTest()
        {
            var person = new Person();

            person.Street = "100 Some Street";
            person.City = "Some City";
            person.State = "NY";
            person.ZipCode = "12345";

            Assert.AreEqual("100 Some Street, Some City, NY 12345", person.Address);
        }
    }
}