using NUnit.Framework;
using System;
using System.Linq;
using Manza.HealthCatalyst.Controllers;
using Manza.HealthCatalyst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace HealthCatalystTests
{
    [TestFixture]
    public class PeopleControllerTests
    {
        // ensure DB is created and seeded for each test
        private void SeedDB(PeopleContext context)
        {
            context.Database.EnsureCreated(); 
            context.People.Add(new Person 
            {   
                FirstName = "Marc", 
                LastName = "Manza", 
                Street = "62 Doges Prom", 
                City = "Lindenhurst",
                State = "NY",
                ZipCode = "11757",
                BirthDate = new DateTime(1970, 9, 7), 
                Interests = "Engineering Leadership", 
                PictureURL = "https://media.licdn.com/dms/image/C4E03AQFa-Lk29DNhFA/profile-displayphoto-shrink_200_200/0?e=1565827200&v=beta&t=RKxyvbHC3EHPHHDPOvVXXxxMqeDC_OAyu9I6uj0wCYU"
            });
            context.People.Add(new Person
            {
                FirstName = "Bill",
                LastName = "Gates",
                Street = "1835 73rd Ave",
                City = "Medina",
                State = "WA",
                ZipCode = "98039",
                BirthDate = new DateTime(1955, 10, 28),
                Interests = "Gates Foundation",
                PictureURL = "https://media.licdn.com/dms/image/C5603AQHv9IK9Ts0dFA/profile-displayphoto-shrink_800_800/0?e=1565827200&v=beta&t=iJRDz2qOLKK_d5CGEofMZEXKP3SUc7fD3KKeJd7UhrE"
            });
            context.People.Add(new Person
            {
                FirstName = "Larry",
                LastName = "Ellision",
                Street = "500 Oracle Parkway",
                City = "Redwood City",
                State = "CA",
                ZipCode = "94065",
                BirthDate = new DateTime(1944, 8, 17),
                Interests = "Oracle Cloud",
                PictureURL = "https://media.licdn.com/dms/image/C5603AQFEHCGuvO7Zpw/profile-displayphoto-shrink_800_800/0?e=1565827200&v=beta&t=c9yq_cjTDDe08fZmrjwsHO_n8c41CBAe94klzvRJHJw"
            });
            context.People.Add(new Person
            {
                FirstName = "Jeff",
                LastName = "Bezos",
                Street = "1200 12trh Avenue",
                City = "Seattle",
                State = "WA",
                ZipCode = "98144",
                BirthDate = new DateTime(1964, 1, 12),
                Interests = "AWS Cloud",
                PictureURL = "https://www.bing.com/th?id=AMMS_1ffda714849b53877bd52b1954d04f4c&w=209&h=209&c=7&o=5&dpr=2&pid=1.7"
            });

            context.SaveChanges();
        }

        [Test, RequiresThread]
        public void GetAllPeopleTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // SeedDB and run test
                using (var context = new PeopleContext())
                {
                    SeedDB(context);

                    var service = new PeopleController(context);
                    var result = service.GetPeople();
                    Assert.GreaterOrEqual(result.Count(), 4);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test, RequiresThread]
        public void SearchPeopleTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // SeedDB and run test
                using (var context = new PeopleContext())
                {
                    SeedDB(context);

                    var service = new PeopleController(context);

                    // test searching for People that contain a specific character. 
                    // we should get a result since at least one seeded person matches
                    var result = service.GetPeopleByName("M");
                    Assert.IsNotNull(result);

                    // check that we got a result with a 200 response 
                    var status = result.Result as OkObjectResult;
                    Assert.AreEqual(200, status.StatusCode);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Test, RequiresThread]
        public void SearchPeopleNoMatchTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<PeopleContext>()
                    .UseSqlite(connection)
                    .Options;

                // SeedDB and run test
                using (var context = new PeopleContext())
                {
                    SeedDB(context);

                    var service = new PeopleController(context);

                    // test searching for people using a query that should return no matches
                    var result = service.GetPeopleByName("!");
                    Assert.IsNotNull(result);

                    // check that we got a valid Not Found response which means no matching results
                    var status = result.Result as NotFoundObjectResult;
                    Assert.AreEqual(404, status.StatusCode);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}