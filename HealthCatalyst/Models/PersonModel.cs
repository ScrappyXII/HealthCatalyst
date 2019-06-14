using System;
using System.ComponentModel.DataAnnotations;

namespace Manza.HealthCatalyst.Models
{
    // Definition of a Person model object:
    //      Unique Person ID     
    //      First and Last Name (= calculated Full Name)
    //      Street, City, State, Zip (= calculated Address)
    //      Birth Date (w/ calculated Age)
    //      URL to Person's Picture
    //      Interests
    public class Person
    {
        [Key]
        public int UID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public DateTime BirthDate { get; set; }

        public string PictureURL { get; set; }

        public string Interests { get; set; }


        // Full name is combined first name and last name
        public string FullName
        {
            get
            {
                var myName = FirstName + " " + LastName;

                return myName;
            }

        }

        // Address is combined street, city, state and zipcode
        public string Address
        {
            get
            {
                var myAddr = Street + ", " + City + ", " + State + " " + ZipCode;

                return myAddr;
            }
        }

        // calculate Age based on Birth date
        public string Age
        {
            get
            {
                DateTime n = DateTime.Now; // To avoid a race condition around midnight
                int age = n.Year - BirthDate.Year;

                if (n.Month < BirthDate.Month || (n.Month == BirthDate.Month && n.Day < BirthDate.Day))
                    age--;

                return age.ToString();
            }
        }
    }
}
