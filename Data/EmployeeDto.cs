using KoncarWebService.Models;

namespace KoncarWebService.Data
{
    public class EmployeeDto : IDTObject
    {

        /**
         *  Id of the employee.
         */
        public string Id { get; set; }

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
		public string Gender { get; set; }

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
		public string Department { get; set; }

        public EmployeeDto()
        {
        }

		/*
         *  Public constructor.
         */
		public EmployeeDto(string id,
                        string firstName, string lastName,
						string birthPlace, string currentPlace,
						string gender,
						string department,
						string oib)
		{
            Id = id;
			FirstName = firstName;
			LastName = lastName;
			BirthPlace = birthPlace;
			CurrentPlace = currentPlace;
			Gender = gender;
			Department = department;
			OIB = oib;
		}

        public IDataObject ToDataObject()
        {
            return new Employee(
                0,
                FirstName,
                LastName,
                BirthPlace,
                CurrentPlace,
                EmployeeGenderExtensions.GetEnumValue(Gender),
                DepartmentCodeExtensions.GetEnumValue(Department),
                OIB
            );
        }
    }
}
