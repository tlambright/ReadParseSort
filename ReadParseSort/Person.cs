using System;
using System.Collections.Generic;
using System.Text;

namespace ReadParseSort
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string FavoriteColor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthFormatted
        {
            get
            {
                return DateOfBirth.ToString(Constants.Output_DateFormat);
            }
        }
    }
}
