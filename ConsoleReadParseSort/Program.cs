using ReadParseSort;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace ConsoleParseSortRecords1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var readParseSort = new ReadParseSortRecords();

                string[] files = Directory.GetFiles("../../../SupportingFiles/");

                foreach (var filePath in files)
                {
                    var filename = Path.GetFileName(filePath);
                    if (filename.StartsWith("Records"))
                    {
                        readParseSort.AddFileToFileList(filePath);
                    }
                }

                //readParseSort.AddDelimiterToDelimiterList(Constants.Delimiter_Pipe);
                //readParseSort.AddDelimiterToDelimiterList(Constants.Delimiter_Comma);
                //readParseSort.AddDelimiterToDelimiterList(Constants.Delimiter_Space);

                readParseSort.ReadData();

                //Sort the Data 
                //Get list sorted by Gender (F/M) ascending, Last Name ascending.
                var sortedListByGenderLastName = readParseSort.SortPersonsByGenderAscLastNameAsc();

                Console.WriteLine("* Persons sorted by Gender (F/M) ascending then by Last Name ascending. *");
                foreach (var person in sortedListByGenderLastName)
                {
                    var output = string.Format(Constants.Output_ConsoleFormat, person.LastName, person.FirstName, person.Gender, person.FavoriteColor, person.DateOfBirthFormatted);
                    Console.WriteLine(output);
                }

                Console.WriteLine();

                //Get list sorted by by Date of Birth ascending.
                var sortedListByDateOfBirthAsc = readParseSort.SortPersonsByDateOfBirthAsc();
                Console.WriteLine("* Persons sorted by Date of Birth ascending. *");

                foreach (var person in sortedListByDateOfBirthAsc)
                {
                    var output = string.Format(Constants.Output_ConsoleFormat, person.LastName, person.FirstName, person.Gender, person.FavoriteColor, person.DateOfBirthFormatted);
                    Console.WriteLine(output);
                }

                Console.WriteLine();
                
                //Get list sorted by Last Name descending.
                var sortedListByLastNameDesc = readParseSort.SortPersonsByLastNameDesc();

                Console.WriteLine("* Persons sorted by Last Name descending. *");

                foreach (var person in sortedListByLastNameDesc)
                {
                    var output = string.Format(Constants.Output_ConsoleFormat, person.LastName, person.FirstName, person.Gender, person.FavoriteColor, person.DateOfBirthFormatted);
                    Console.WriteLine(output);
                }

                Console.WriteLine();
                // Wait for user to acknowledge.
                Console.WriteLine("Press Enter to terminate...");
                Console.Read();

            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred. " + ex.Message);
                // Wait for user to acknowledge.
                Console.WriteLine("Press Enter to terminate...");
                Console.Read();
            }

        }

        private void ReadData(string[] args)
        {
            if (args != null && args.Length > 0)
            {


            }
        }
    }
}
