<<<<<<< HEAD:ReadParseSort.UnitTests/ReadParseSortRecordsTests.cs
﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadParseSort;

namespace ReadParseSort.UnitTests
{
    [TestClass]
    public class ReadParseSortRecordsTests
    {
        [TestMethod]
        public void ReadParseSort_AddDelimiterToDelimiterList_Succeeds()
        {

            var readParseSort = new ReadParseSortRecords();
            var delimiter = "|";
            readParseSort.AddDelimiterToDelimiterList(delimiter);

            Assert.IsTrue(readParseSort.DelimiterList.Contains(delimiter));
        }

        [TestMethod]
        public void ReadParseSort_AddFilePathToFilePathList_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();
            var path = @"C:\Program Files\test.txt";
            readParseSort.AddFilePathToFilePathList(path);

            Assert.IsTrue(readParseSort.FileList.Contains(path));
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterPipe_ObjectCompare_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = "01/01/1990";

            var person = new Person();
            person.LastName = lastName;
            person.FirstName = firstName;
            person.Gender = gender;
            person.FavoriteColor = favoriteColor;

            DateTime checkDOB;
            if (DateTime.TryParse(dateOfBirth, out checkDOB))
            {
                person.DateOfBirth = checkDOB;
            }

            //Add the delimiter to the delimiter list
            //readParseSort.AddDelimiterToDelimiterList("|");

            //The order matters.  Use the same delimiter as added to delimiter list
            var personTextInput = string.Format("{0} | {1} | {2} | {3} | {4}", lastName, firstName, gender, favoriteColor, dateOfBirth);

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(person.LastName, personFromText.LastName);
            Assert.AreEqual(person.FirstName, personFromText.FirstName);
            Assert.AreEqual(person.Gender, personFromText.Gender);
            Assert.AreEqual(person.FavoriteColor, personFromText.FavoriteColor);
            Assert.AreEqual(person.DateOfBirth, personFromText.DateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterPipe_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var dateOfBirthFormatted = "1/1/1990";

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0} | {1} | {2} | {3} | {4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString(Constants.Output_DateFormat));

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
            Assert.AreEqual(personFromText.DateOfBirthFormatted, dateOfBirthFormatted);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterComma_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0},{1},{2},{3},{4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString());

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterSpace_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0} {1} {2} {3} {4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString());

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_AddPersonToPersonList_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personATextInput = string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName);

            var personFromText = readParseSort.GetPersonFromTextInput(personATextInput);
            readParseSort.AddPersonToPersonList(personFromText);

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(1));


            //Assert.AreEqual(personFromText.LastName, lastName);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByGenderAscLastNameAsc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";
            var personBLastName = "BlastName";
            var personCLastName = "ClastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            //Add to the list in order different from expected order
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/2000", personCLastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/1990", personBLastName));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByGenderAscLastNameAsc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].Gender, "F");
            Assert.AreEqual(sortedList[0].LastName, personBLastName);
            Assert.AreEqual(sortedList[1].Gender, "M");
            Assert.AreEqual(sortedList[1].LastName, personALastName);
            Assert.AreEqual(sortedList[1].Gender, "M");
            Assert.AreEqual(sortedList[2].LastName, personCLastName);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByDateOfBirthAsc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";
            var personBLastName = "BlastName";
            var personCLastName = "ClastName";

            var personADateOfBirth = "1/1/1990";
            var personBDateOfBirth = "1/1/2000";
            var personCDateOfBirth = "1/1/1980";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            //Add to the list in order different from expected order
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | {1}", personBLastName, personBDateOfBirth));
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | {1}", personCLastName, personCDateOfBirth));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | {1}", personALastName, personADateOfBirth));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByDateOfBirthAsc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].DateOfBirthFormatted, personCDateOfBirth);
            Assert.AreEqual(sortedList[1].DateOfBirthFormatted, personADateOfBirth);
            Assert.AreEqual(sortedList[2].DateOfBirthFormatted, personBDateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByLastNameDesc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personCLastName = "ClastName";
            var personBLastName = "BlastName";
            var personALastName = "AlastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1990", personALastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/2000", personBLastName));
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/1980", personCLastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByLastNameDesc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].LastName, personCLastName);
            Assert.AreEqual(sortedList[1].LastName, personBLastName);
            Assert.AreEqual(sortedList[2].LastName, personALastName);
        }


        [TestMethod]
        public void ReadParseSort_SortPersonsByLastName_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personCLastName = "ClastName";
            var personBLastName = "BlastName";
            var personALastName = "AlastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/2000", personCLastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/1990", personBLastName));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByLastName();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].LastName, personALastName);
            Assert.AreEqual(sortedList[1].LastName, personBLastName);
            Assert.AreEqual(sortedList[2].LastName, personCLastName);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadParseSort;

namespace ReadParseSort.UnitTests
{
    [TestClass]
    public class ReadParseSortRecordsTests
    {
        [TestMethod]
        public void ReadParseSort_AddDelimiterToDelimiterList_Succeeds()
        {

            var readParseSort = new ReadParseSortRecords();
            var delimiter = "|";
            readParseSort.AddDelimiterToDelimiterList(delimiter);

            Assert.IsTrue(readParseSort.DelimiterList.Contains(delimiter));
        }

        [TestMethod]
        public void ReadParseSort_AddFilePathToFilePathList_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();
            var path = @"C:\Program Files\test.txt";
            readParseSort.AddFilePathToFilePathList(path);

            Assert.IsTrue(readParseSort.FileList.Contains(path));
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterPipe_ObjectCompare_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = "01/01/1990";

            var person = new Person();
            person.LastName = lastName;
            person.FirstName = firstName;
            person.Gender = gender;
            person.FavoriteColor = favoriteColor;

            DateTime checkDOB;
            if (DateTime.TryParse(dateOfBirth, out checkDOB))
            {
                person.DateOfBirth = checkDOB;
            }

            //Add the delimiter to the delimiter list
            //readParseSort.AddDelimiterToDelimiterList("|");

            //The order matters.  Use the same delimiter as added to delimiter list
            var personTextInput = string.Format("{0} | {1} | {2} | {3} | {4}", lastName, firstName, gender, favoriteColor, dateOfBirth);

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(person.LastName, personFromText.LastName);
            Assert.AreEqual(person.FirstName, personFromText.FirstName);
            Assert.AreEqual(person.Gender, personFromText.Gender);
            Assert.AreEqual(person.FavoriteColor, personFromText.FavoriteColor);
            Assert.AreEqual(person.DateOfBirth, personFromText.DateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterPipe_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);
            var dateOfBirthFormatted = "1/1/1990";

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0} | {1} | {2} | {3} | {4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString(Constants.Output_DateFormat));

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
            Assert.AreEqual(personFromText.DateOfBirthFormatted, dateOfBirthFormatted);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterComma_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0},{1},{2},{3},{4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString());

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_GetPersonFromTextInput_DelimiterSpace_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var lastName = "LastName";
            var firstName = "FirstName";
            var gender = "M";
            var favoriteColor = "Black";
            var dateOfBirth = new DateTime(1990, 1, 1);

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personTextInput = string.Format("{0} {1} {2} {3} {4}", lastName, firstName, gender, favoriteColor, dateOfBirth.ToString());

            var personFromText = readParseSort.GetPersonFromTextInput(personTextInput);

            Assert.AreEqual(personFromText.LastName, lastName);
            Assert.AreEqual(personFromText.FirstName, firstName);
            Assert.AreEqual(personFromText.Gender, gender);
            Assert.AreEqual(personFromText.FavoriteColor, favoriteColor);
            Assert.AreEqual(personFromText.DateOfBirth, dateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_AddPersonToPersonList_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            var personATextInput = string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName);

            var personFromText = readParseSort.GetPersonFromTextInput(personATextInput);
            readParseSort.AddPersonToPersonList(personFromText);

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(1));


            //Assert.AreEqual(personFromText.LastName, lastName);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByGenderAscLastNameAsc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";
            var personBLastName = "BlastName";
            var personCLastName = "ClastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            //Add to the list in order different from expected order
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/2000", personCLastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/1990", personBLastName));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByGenderAscLastNameAsc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].Gender, "F");
            Assert.AreEqual(sortedList[0].LastName, personBLastName);
            Assert.AreEqual(sortedList[1].Gender, "M");
            Assert.AreEqual(sortedList[1].LastName, personALastName);
            Assert.AreEqual(sortedList[1].Gender, "M");
            Assert.AreEqual(sortedList[2].LastName, personCLastName);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByDateOfBirthAsc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personALastName = "AlastName";
            var personBLastName = "BlastName";
            var personCLastName = "ClastName";

            var personADateOfBirth = "1/1/1990";
            var personBDateOfBirth = "1/1/2000";
            var personCDateOfBirth = "1/1/1980";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            //Add to the list in order different from expected order
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | {1}", personBLastName, personBDateOfBirth));
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | {1}", personCLastName, personCDateOfBirth));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | {1}", personALastName, personADateOfBirth));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByDateOfBirthAsc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].DateOfBirthFormatted, personCDateOfBirth);
            Assert.AreEqual(sortedList[1].DateOfBirthFormatted, personADateOfBirth);
            Assert.AreEqual(sortedList[2].DateOfBirthFormatted, personBDateOfBirth);
        }

        [TestMethod]
        public void ReadParseSort_SortPersonsByLastNameDesc_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personCLastName = "ClastName";
            var personBLastName = "BlastName";
            var personALastName = "AlastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1990", personALastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/2000", personBLastName));
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/1980", personCLastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByLastNameDesc();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].LastName, personCLastName);
            Assert.AreEqual(sortedList[1].LastName, personBLastName);
            Assert.AreEqual(sortedList[2].LastName, personALastName);
        }


        [TestMethod]
        public void ReadParseSort_SortPersonsByLastName_Succeeds()
        {
            var readParseSort = new ReadParseSortRecords();

            var personCLastName = "ClastName";
            var personBLastName = "BlastName";
            var personALastName = "AlastName";

            var personInputList = new List<string>();

            //The order matters.  Use one the default support delimiters:  "|", ",", " "
            personInputList.Add(string.Format("{0} | CfirstName | M | Red | 01/01/2000", personCLastName));
            personInputList.Add(string.Format("{0} | BfirstName | F | Blue | 01/01/1990", personBLastName));
            personInputList.Add(string.Format("{0} | AfirstName | M | Green | 01/01/1980", personALastName));

            foreach (var personTextInput in personInputList)
            {
                var personFromInput = readParseSort.GetPersonFromTextInput(personTextInput);
                readParseSort.AddPersonToPersonList(personFromInput);
            }

            var sortedList = readParseSort.SortPersonsByLastName();

            Assert.IsTrue(readParseSort.PersonList.Count.Equals(3));
            Assert.AreEqual(sortedList[0].LastName, personALastName);
            Assert.AreEqual(sortedList[1].LastName, personBLastName);
            Assert.AreEqual(sortedList[2].LastName, personCLastName);
        }
    }
}
>>>>>>> eedc1e8858a696aa770a4f69e6603007de75c08b:ReadParseSort.UnitTests/ReadParseSortRecordsTests.cs
