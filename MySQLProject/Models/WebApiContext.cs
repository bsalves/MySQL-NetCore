using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace MySQLProject.Models
{
    public class WebApiContext
    {

        public string ConnectionString { get; set; }

        public WebApiContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<User> GetUsers()
        {
            List<User> list = new List<User>();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("select * from users", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new User()
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return list;
        }

        public User GetUser(int id)
        {
            User user = new User();

            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;

                command.CommandText = "Select id, name From users where id = @id";
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    user.id = Convert.ToInt32(reader["id"]);
                    user.name = reader["name"].ToString();
                }
            }
            return user;
        }

        public bool DeleteUser(int id)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;

                command.CommandText = "delete from users where id = @id";
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                try 
                {
                    var reader = command.ExecuteReader();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool CreateUser(User user)
        {
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand();
                command.Connection = connection;

                command.CommandText = "insert into users (name) value (@name)";
                command.Parameters.Add("@name", MySqlDbType.String).Value = user.name;

                try
                {
                    var reader = command.ExecuteReader();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
