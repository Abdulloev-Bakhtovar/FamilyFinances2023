using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class Views : Window
    {
        public Views()
        {
            InitializeComponent();
            addItemComboBox();
        }
        private void addItemComboBox()
        {
            DataBase _dataBase = new DataBase("root", "password");

            string Sql = "SELECT TABLE_NAME\r\nFROM INFORMATION_SCHEMA.VIEWS\r\nWHERE TABLE_SCHEMA = 'familyfinancesdb';";
            _dataBase.OpenConnection();
            MySqlCommand cmd = new MySqlCommand(Sql, _dataBase.GetConnection());
            MySqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxTable.Items.Add(DR[0]);
            }
            _dataBase.CloseConnection();
        }

        private void addItemDataGrid(string _table)
        {
            DataBase _dataBase = new DataBase("root", "password");

            string query = $"select * from {_table};";
            MySqlCommand cmd = new MySqlCommand(query, _dataBase.GetConnection());
            DataTable dt = new DataTable();
            _dataBase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            _dataBase.CloseConnection();
            dataGridTables.ItemsSource = dt.DefaultView;
        }

        private string _table;

        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                _table = comboBox.SelectedItem.ToString();
                addItemDataGrid(_table);
            }
        }
    }
}
