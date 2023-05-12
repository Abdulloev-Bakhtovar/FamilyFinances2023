using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FamilyFinances
{
    /*internal*/ class DataBase
    {
        MySqlConnection MyConnection;
        public DataBase(string _Log, string _Pass) 
        {
            MyConnection = new MySqlConnection($"Server=localhost; Port=3306; Database=FamilyFinancesDb; Username={_Log}; Password={_Pass};");
        }

        public void OpenConnection()
        {
            if(MyConnection.State == System.Data.ConnectionState.Closed)
                MyConnection.Open();
        }
        public void CloseConnection()
        {
            if(MyConnection.State == System.Data.ConnectionState.Open)
                MyConnection.Close();
        }

        public MySqlConnection GetConnection()
        {
            return MyConnection;
        }
    }
}
