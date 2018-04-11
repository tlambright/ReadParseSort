using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ReadParseSort
{
    public class ReadParseSortRecords
    {

        #region Properties

        public string FilesDirectory { get; set; }

        private List<string> _fileList;
        public List<string> FileList
        {
            get
            {
                if (_fileList == null)
                {
                    _fileList = new List<string>();
                }

                return _fileList;
            }
            set
            {
                _fileList = value;
            }
        }

        private List<Person> _personList;
        public List<Person> PersonList
        {
            get
            {
                if (_personList == null)
                {
                    _personList = new List<Person>();
                }

                return _personList;
            }
            set
            {
                _personList = value;
            }
        }

        private List<string> _delimiterList;
        public List<string> DelimiterList
        {
            get
            {
                if (_delimiterList == null)
                {
                    _delimiterList = new List<string>();
                }

                return _delimiterList;
            }
            set
            {
                _delimiterList = value;
            }
        }
        #endregion

        public ReadParseSortRecords()
        {
            InitializeDefaultSupportedDelimiters();
        }

        private void InitializeDefaultSupportedDelimiters()
        {
            DelimiterList.Add(Constants.Delimiter_Pipe);
            DelimiterList.Add(Constants.Delimiter_Comma);
            DelimiterList.Add(Constants.Delimiter_Space);


            var fqp = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var supportingFilesDir = string.Format("{0}\\{1}", fqp, "SupportingFiles");

        }

        public void ReadData()
        {
            if (FileList != null && FileList.Count > 0)
            {
                foreach (var filePath in FileList)
                {

                    string stringData = File.ReadAllText(filePath);
                    string[] rowData = stringData.Split("\r\n\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    if (PersonList != null)
                    {
                        foreach (var personRecord in rowData)
                        {
                            var person = GetPersonFromTextInput(personRecord);

                            AddPersonToPersonList(person);
                        }
                    }
                }
            }
        }

        public void AddPersonToPersonList(Person person)
        {
            if (person != null)
            {
                if (!PersonList.Contains(person)) //Don't add duplicates
                {
                    PersonList.Add(person);
                }
            }
        }

        public Person GetPersonFromTextInput(string personRecord)
        {
            Person person = null;

            if (!string.IsNullOrEmpty(personRecord))
            {
                var delimiter = GetDelimiter(personRecord);

                if (IsValidDelimiter(delimiter)) //Confirm the delimiter is one of the supported delimiters
                {
                    string[] splitData = personRecord.Split(System.Convert.ToChar(delimiter));

                    if (IsValidRecord(splitData)) //Confirm the record had only 5 elements and the fifth element is a valid DateTime
                    {
                        person = new Person();
                        person.FirstName = splitData[Constants.SplitIndex_FirstName].Trim();
                        person.LastName = splitData[Constants.SplitIndex_LastName].Trim();
                        person.Gender = splitData[Constants.SplitIndex_Gender].Trim();
                        person.FavoriteColor = splitData[Constants.SplitIndex_FavoriteColor].Trim();
                        person.DateOfBirth = DateTime.Parse(splitData[Constants.SplitIndex_DateOfBirth].Trim());
                    }
                }
            }

            return person;
        }

        private bool IsValidRecord(string[] splitData)
        {
            var returnValue = false;

            // Confirm the record has at least 5 elements and the 5th element is a valid DateTime
            //Date of Birth may include time, space " " delimiter will break this out as additional elements
            //Ignoring any element after 5th
            if (splitData.Length >= 5)
            {
                //Confirm the 5th element if a valid DateTime
                DateTime checkDate;
                returnValue = DateTime.TryParse(splitData[Constants.SplitIndex_DateOfBirth], out checkDate);
            }

            return returnValue;
        }

        private bool IsValidDelimiter(string delimiter)
        {
            var returnValue = false;

            if (delimiter.Length == 1)
            { 
                returnValue = DelimiterList.Contains(delimiter);
            }

            return returnValue;
        }

        private string GetDelimiter(string dataRecord)
        {
            var returnValue = string.Empty;

            if (DelimiterList != null && DelimiterList.Count > 0)
            {
                foreach (var delimiter in DelimiterList)
                {
                    if (dataRecord.IndexOf(delimiter) > 0)
                    {
                        returnValue = delimiter;
                        break;
                    }
                }
            }

            return returnValue;

        }

        public void AddFilePathToFilePathList(string filePath)
        {
            if (filePath != null && filePath.Length > 0)
            {
                //Don't add it if it already exists
                if (!FileList.Contains(filePath))
                {
                    FileList.Add(filePath);
                }
            }
        }

        public void AddDelimiterToDelimiterList(string delimiter)
        {
            //Check the length, needs to be only one char
            if (delimiter != null && delimiter.Length == 1)
            {
                //Don't add it if it already exists
                if (!DelimiterList.Contains(delimiter))
                {
                    DelimiterList.Add(delimiter);
                }
            }
        }

        public List<Person> SortPersonsByGenderAscLastNameAsc()
        {
            //Persons sorted by Gender (F/M) ascending then by Last Name ascending.;
            var sortedListByGenderLastName = PersonList.OrderBy(p => p.Gender).ThenBy(p1 => p1.LastName).ToList();
            return sortedListByGenderLastName;
        }

        public List<Person> SortPersonsByDateOfBirthAsc()
        {
            //Persons sorted by Date of Birth ascending.
            var sortedListByDateOfBirthAsc = PersonList.OrderBy(p => p.DateOfBirth).ToList();
            return sortedListByDateOfBirthAsc;
        }

        public List<Person> SortPersonsByLastNameDesc()
        {
            //Persons sorted by Last Name descending.
            var sortedListByLastNameDesc = PersonList.OrderByDescending(p => p.LastName).ToList();
            return sortedListByLastNameDesc;
        }

        public List<Person> SortPersonsByLastName()
        {
            //Persons sorted by Last Name descending.
            var sortedListByLastName = PersonList.OrderBy(p => p.LastName).ToList();
            return sortedListByLastName;
        }

    }
}
