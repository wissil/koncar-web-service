using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Configuration;

using KoncarWebService.Data;
using KoncarWebService.Models;

namespace KoncarWebService.App_Persistence
{
    public class EmployeePersistence
    {

        readonly string connectionString = ConfigurationManager.ConnectionStrings["sqlConnString"].ConnectionString;

        readonly MySqlConnection conn;

        static EmployeePersistence persistence;

        public EmployeePersistence()
        {
            try 
            {
                // establish connection to the database
                conn = new MySqlConnection(connectionString);
                conn.Open();
            }
            catch (MySqlException e)
            {
                // log the error
                throw new Exception(
                    String.Format("Error establishing connection to the database: '{0}'.", 
                                  e.Message));
            }
        }

        /**
         * Returns the singleton object of persistence object.
         * 
         */
        public static EmployeePersistence getEmployeePersistence()
        {
            if (persistence == null) {
                persistence = new EmployeePersistence();
            }

            return persistence;
        }

        /**
         * Parses the EmployeeDto object from the SQL reader cache.
         * 
         */
        EmployeeDto ParseFromReaderCache(MySqlDataReader dataReader)
        {
            // id
            int id = 0;
            try { id = dataReader.GetInt32(0); }
            catch (SqlNullValueException) {/* log the error */}

            // first name
            string firstName = "N/A";
            try { firstName = dataReader.GetString(1); }
            catch (SqlNullValueException) {/* log the error */}

            // last name
            string lastName = "N/A";
            try { lastName = dataReader.GetString(2); }
			catch (SqlNullValueException) {/* log the error */}

            // birth place
            string birthPlace = "N/A";
            try { birthPlace = dataReader.GetString(3); }
			catch (SqlNullValueException) {/* log the error */}

            // current place
            string currentPlace = "N/A";
            try { currentPlace = dataReader.GetString(4); }
			catch (SqlNullValueException) {/* log the error */}

            // gender
            EmployeeGender gender = EmployeeGender.UNDEFINED;
            try { gender = EmployeeGenderExtensions.GetEnumValue(dataReader.GetInt32(5)); }
			catch (SqlNullValueException) {/* log the error */}

            // department
            DepartmentCode departmentCode = DepartmentCode.UNKNOWN;
            try { departmentCode = DepartmentCodeExtensions.GetEnumValue(dataReader.GetString(6)); }
			catch (SqlNullValueException) {/* log the error */}

            // oib
            string OIB = "N/A";
			try { OIB = dataReader.GetString(7); }
			catch (SqlNullValueException) {/* log the error */}

            return (EmployeeDto) 
                new Employee(id, firstName, lastName, birthPlace, currentPlace, gender, departmentCode, OIB).ToDto();
        }

        public long SaveEmployee(EmployeeDto employee)
        {
            string sqlString =
                "INSERT INTO Employee (FirstName, LastName, BirthPlace, CurrentPlace, Gender, Department, OIB) " +
                "VALUES (@FirstName, @LastName, @BirthPlace, @CurrentPlace, @Gender, @Department, @OIB)";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);

            Employee e = (Employee) employee.ToDataObject();

			cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
			cmd.Parameters.AddWithValue("@LastName", e.LastName);
			cmd.Parameters.AddWithValue("@BirthPlace", e.BirthPlace);
			cmd.Parameters.AddWithValue("@CurrentPlace", e.CurrentPlace);
			cmd.Parameters.AddWithValue("@Gender", e.Gender);
            cmd.Parameters.AddWithValue("@Department", e.Department.GetStringValue());
			cmd.Parameters.AddWithValue("@OIB", e.OIB);

            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }


        /**
         * Returns the employee with a given Id from the database if such is present,
         * otherwise this method returns null.
         * 
         */
        public EmployeeDto GetEmployee(long id)
        {
            string sqlString = "SELECT * FROM Employee WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);

            cmd.Parameters.AddWithValue("@ID", id);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return ParseFromReaderCache(reader);
                }

                return null;
            }
        }


        /**
         * Deletes the Employee with a given Id from the database.
         * 
         * Returns 1 if operation was successful.
         * 
         */
        public int DeleteEmployee(long id) 
        {
            string sqlString = "DELETE FROM Employee WHERE ID = @ID";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);

            cmd.Parameters.AddWithValue("@ID", id);

            // number of rows affected
            return cmd.ExecuteNonQuery();
        }

        /**
         * Updates the employee with the given Id with the given parameteres.
         * 
         */
        public void UpdateEmployee(long id, EmployeeDto parameteres)
        {
            string sqlString =
                "UPDATE Employee " +
                "SET FirstName = @FirstName, LastName = @LastName, " +
                "CurrentPlace = @CurrentPlace, BirthPlace = @BirthPlace, " +
                "Gender = @Gender, Department = @Department, OIB = @OIB " +
                "WHERE Id = @Id";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);

            cmd.Parameters.AddWithValue("@FirstName", parameteres.FirstName);
            cmd.Parameters.AddWithValue("@LastName", parameteres.LastName);
            cmd.Parameters.AddWithValue("@CurrentPlace", parameteres.CurrentPlace);
            cmd.Parameters.AddWithValue("@BirthPlace", parameteres.BirthPlace);
            cmd.Parameters.AddWithValue("@Gender", parameteres.Gender);
            cmd.Parameters.AddWithValue("@Department", parameteres.Department);
            cmd.Parameters.AddWithValue("@OIB", parameteres.OIB);
            cmd.Parameters.AddWithValue("@Id", parameteres.Id);

            cmd.ExecuteNonQuery();
        }

        /**
         * Returns the list of all employees currently stored in the database.
         * 
         */
        public List<EmployeeDto> GetEmployeeList()
        {
            string sqlString = "SELECT * FROM Employee";

            MySqlCommand cmd = new MySqlCommand(sqlString, conn);

            List<EmployeeDto> employees = new List<EmployeeDto>();

            // loads the whole table into the memory
            // for large table: optimize the sql query to do the filtering
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    employees.Add(ParseFromReaderCache(reader));
                }
            }

            return employees;
        }
    }
}
