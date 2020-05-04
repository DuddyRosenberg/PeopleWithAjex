using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PeopleAjex.Data
{
    public class PeopleDB
    {
        private string _connectionString;
        public PeopleDB(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetPeople()
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM People";
                connection.Open();
                var reader = command.ExecuteReader();
                var people = new List<Person>();
                while (reader.Read())
                {
                    people.Add(new Person
                    {
                        ID = (int)reader["ID"],
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Age = (int)reader["Age"],
                    });
                }
                return people;
            }
        }
        public int AddPerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO People(FirstName,LastName,Age)VALUES(@firstName,@lastName,@age)";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                return (int)command.ExecuteNonQuery();
            }
        }
        public Person GetPerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM People WHERE ID=@id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                return new Person
                {
                    ID = id,
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                };
            }
        }
        public int EditPerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "UPDATE People SET FirstName=@firstName,LastName=@lastName,Age=@age WHERE ID=@id";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.Parameters.AddWithValue("@id", person.ID);
                connection.Open();
                return (int)command.ExecuteNonQuery();
            }
        }
        public int DeletePerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM People WHERE ID=@id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                return (int)command.ExecuteNonQuery();
            }
        }
    }
}
