<<<<<<< HEAD
﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadParseSort;
using RestReadParseSort.Controllers;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Results;
using System.Reflection;
using System.Collections.Generic;

namespace RestReadParseSort.UnitTests
{
    [TestClass]
    public class RecordsControllerTests
    {
        [TestMethod]
        public void REST_Records_Get_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedByGenderAscLastNameAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.gender();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].Gender == "F");
            Assert.IsTrue(persons[0].LastName == "Keaton");
            Assert.IsTrue(persons[1].Gender == "F");
            Assert.IsTrue(persons[1].LastName == "McBride");
            Assert.IsTrue(persons[2].Gender == "M");
            Assert.IsTrue(persons[2].LastName == "Lincoln");
            Assert.IsTrue(persons[3].Gender == "M");
            Assert.IsTrue(persons[3].LastName == "Pacino");
            Assert.IsTrue(persons[4].Gender == "M");
            Assert.IsTrue(persons[4].LastName == "Rodgers");
            Assert.IsTrue(persons[5].Gender == "M");
            Assert.IsTrue(persons[5].LastName == "Trubisky");
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedBirthdateAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.birthdate();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].DateOfBirthFormatted == "4/25/1940");
            Assert.IsTrue(persons[1].DateOfBirthFormatted == "1/5/1946");
            Assert.IsTrue(persons[2].DateOfBirthFormatted == "5/23/1965");
            Assert.IsTrue(persons[3].DateOfBirthFormatted == "9/14/1973");
            Assert.IsTrue(persons[4].DateOfBirthFormatted == "12/2/1983");
            Assert.IsTrue(persons[5].DateOfBirthFormatted == "8/20/1994");
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedByLastNameAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.name();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].LastName == "Keaton");
            Assert.IsTrue(persons[1].LastName == "Lincoln");
            Assert.IsTrue(persons[2].LastName == "McBride");
            Assert.IsTrue(persons[3].LastName == "Pacino");
            Assert.IsTrue(persons[4].LastName == "Rodgers");
            Assert.IsTrue(persons[5].LastName == "Trubisky");
        }
    }
}
=======
﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadParseSort;
using RestReadParseSort.Controllers;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Http.Results;
using System.Reflection;
using System.Collections.Generic;

namespace RestReadParseSort.UnitTests
{
    [TestClass]
    public class RecordsControllerTests
    {
        [TestMethod]
        public void REST_Records_Get_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedByGenderAscLastNameAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.gender();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].Gender == "F");
            Assert.IsTrue(persons[0].LastName == "Keaton");
            Assert.IsTrue(persons[1].Gender == "F");
            Assert.IsTrue(persons[1].LastName == "McBride");
            Assert.IsTrue(persons[2].Gender == "M");
            Assert.IsTrue(persons[2].LastName == "Lincoln");
            Assert.IsTrue(persons[3].Gender == "M");
            Assert.IsTrue(persons[3].LastName == "Pacino");
            Assert.IsTrue(persons[4].Gender == "M");
            Assert.IsTrue(persons[4].LastName == "Rodgers");
            Assert.IsTrue(persons[5].Gender == "M");
            Assert.IsTrue(persons[5].LastName == "Trubisky");
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedBirthdateAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.birthdate();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].DateOfBirthFormatted == "4/25/1940");
            Assert.IsTrue(persons[1].DateOfBirthFormatted == "1/5/1946");
            Assert.IsTrue(persons[2].DateOfBirthFormatted == "5/23/1965");
            Assert.IsTrue(persons[3].DateOfBirthFormatted == "9/14/1973");
            Assert.IsTrue(persons[4].DateOfBirthFormatted == "12/2/1983");
            Assert.IsTrue(persons[5].DateOfBirthFormatted == "8/20/1994");
        }

        [TestMethod]
        public void REST_Records_Get_PersonsSortedByLastNameAsc_Succeeds()
        {
            // Arrange
            var controller = new RecordsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.name();

            // Assert
            List<Person> persons;
            response.TryGetContentValue<List<Person>>(out persons);

            // Did we get a list out
            Assert.IsTrue(response.TryGetContentValue<List<Person>>(out persons));
            // Does the list count equal 6
            Assert.IsTrue(persons.Count == 6);
            Assert.IsTrue(persons[0].LastName == "Keaton");
            Assert.IsTrue(persons[1].LastName == "Lincoln");
            Assert.IsTrue(persons[2].LastName == "McBride");
            Assert.IsTrue(persons[3].LastName == "Pacino");
            Assert.IsTrue(persons[4].LastName == "Rodgers");
            Assert.IsTrue(persons[5].LastName == "Trubisky");
        }
    }
}
>>>>>>> eedc1e8858a696aa770a4f69e6603007de75c08b
