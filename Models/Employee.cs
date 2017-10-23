using System;
using KoncarWebService.Data;

namespace KoncarWebService.Models
{
    public class Employee : IDataObject
    {
        /**
         * Number of integers in an OIB.
         */
        const int OIB_LENGTH = 11;

        /**
         * OIB not available.
         */
        const string OIB_NA = "N/A";

        /**
         * Id of the employee.
         */
        public int Id { get; set; }

        /**
         * First name of the employee.
         */
        public string FirstName { get; set; }

        /*
         * Last name of the employee.
         */
        public string LastName { get; set; }

        /**
         * Place of birth of the employee.
         */
        public string BirthPlace { get; set; }

        /**
         * Gender of the employee.
         */
        public EmployeeGender Gender { get; set; }

        /**
         * OIB of the employee.
         */
        public string OIB { get; set; }


		/**
         *  Current place of the employee.
         */
		public string CurrentPlace { get; set; }


        /**
         *  Department code of the employee.
         */
        public DepartmentCode Department { get; set; }

        /*
         *  Default constructor.
         */
        public Employee() {}


        /*
         *  Public constructor.
         */
        public Employee(int id,
                        string firstName, string lastName, 
                        string birthPlace, string currentPlace,
                        EmployeeGender gender,
                        DepartmentCode department,
                        string oib)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthPlace = birthPlace;
            CurrentPlace = currentPlace;
            Gender = gender;
            Department = department;
            OIB = ValidateOib(oib);
        }



        static string ValidateOib(string oib) 
        {
            if (oib.Equals(OIB_NA))
            {
                return oib;
            }

            if (oib.Length != OIB_LENGTH) 
            {
                throw new ArgumentException("Oib should contain exactly 11 digits.");
            }

            foreach (char symbol in oib) 
            {
                if (!Char.IsDigit(symbol)) 
                {
                    throw new ArgumentException("Oib should not contain a non-digit symbol: " + symbol + ".");
                }
            }

            return oib;
        }

        public override string ToString()
        {
            return string.Format("[Employee: FirstName={0}, LastName={1}, BirthPlace={2}, Gender={3}, OIB={4}, CurrentPlace={5}, Department={6}]", 
                                 FirstName, LastName, BirthPlace, Gender, OIB, CurrentPlace, Department);
        }


        public IDTObject ToDto()
        {
            return new EmployeeDto(
                Id.ToString(),
                FirstName,
                LastName,
                BirthPlace,
                CurrentPlace,
                EmployeeGenderExtensions.GetStringValue(Gender),
                DepartmentCodeExtensions.GetStringValue(Department),
                OIB
            );
        }
    }
}
