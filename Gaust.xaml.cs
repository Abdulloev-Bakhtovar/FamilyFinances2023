using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows;
using System.Windows.Controls;

/*namespace FamilyFinances
{
    public partial class Gaust : Window
    {
        DataBase _dataBase = new DataBase("gaust",null);
        private void addItemComboBox()
        {
            //_dataBase.GetConnection();
            string Sql = "show tables;";
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
            string query = $"select * from {_table};";
            MySqlCommand cmd = new MySqlCommand(query, _dataBase.GetConnection());
            DataTable dt = new DataTable();

            _dataBase.OpenConnection();
            // Используем DataAdapter для заполнения DataTable
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            _dataBase.CloseConnection();

            dataGridTables.ItemsSource = dt.DefaultView;
        }
        string _table;
        public Gaust()
        {
            InitializeComponent();
            addItemComboBox();
            //comboBoxTable_SelectionChanged();
            //addItemDataGrid(_table);
        }

        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                //_table = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
                _table = comboBox.SelectedItem.ToString();
                addItemDataGrid(_table);
            }
        }
    }
}
*/

namespace FamilyFinances
{
    public partial class Gaust : Window
    {
        private void addItemComboBox()
        {
            DataBase _dataBase = new DataBase("gaust", null);

            string Sql = "show tables;";
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
            DataBase _dataBase = new DataBase("gaust", null);

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

        public Gaust()
        {
            InitializeComponent();
            addItemComboBox();
            //addItemDataGrid(_table);
        }

        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                _table = comboBox.SelectedItem.ToString();
                addItemDataGrid(_table);
            }
        }

        private void buttonAuthorInfo_Click(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            this.Close();
            _login.ShowDialog();
        }
    }
}
