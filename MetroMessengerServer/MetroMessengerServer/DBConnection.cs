using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;

namespace MetroMessengerServer
{
    class DBConnection
    {
        private MySqlConnection connection;
        private MySqlCommand command;

        public DBConnection(string server,string databaseName, string username, string password)
        {
            connection = new MySqlConnection();
           
            connection.ConnectionString = "server="+server+";"
            + "database="+databaseName+";"
            + "uid="+username+";"
            + "password="+password+";";

            command = connection.CreateCommand();
        }

        public void addUser(string username, string password)
        {
            connection.Open();
            command.CommandText = "insert into person (name, password)"
            + " values "
            + "('" + username + "', '" + password + "')";

            MySqlDataReader result = command.ExecuteReader();
            result.Close();
            connection.Close();
        }

        public ArrayList returnAllUsers()
        {
            ArrayList resultSet = new ArrayList();
            
            connection.Open();
            command.CommandText = "select * from person";
            MySqlDataReader result = command.ExecuteReader();

            while (result.Read())
            {
                object[] values = new object[result.FieldCount];
                result.GetValues(values);
                resultSet.Add(values);
            }

            result.Close();
            connection.Close();

            return resultSet;
        }
    }
}
