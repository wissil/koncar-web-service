﻿using System;
namespace KoncarWebService.App_Layers
{
    public class EmployeeDto : IDTObject
    {

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
		public int Gender { get; set; }

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

		/*
         *  Public constructor.
         */
		public EmployeeDto(string firstName, string lastName,
						string birthPlace, string currentPlace,
						int gender,
						string department,
						string oib)
		{
			FirstName = firstName;
			LastName = lastName;
			BirthPlace = birthPlace;
			CurrentPlace = currentPlace;
			Gender = gender;
			Department = department;
			OIB = oib;
		}
    }
}
