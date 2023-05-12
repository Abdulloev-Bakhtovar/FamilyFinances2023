using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances
{
    public partial class InsertSavings : Window
    {
        public InsertSavings()
        {
            InitializeComponent();
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId_income.Text) || string.IsNullOrEmpty(txt_Amiunt.Text))
            {
                MessageBox.Show(this, "Все поля с * обязательно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Замените настройки подключения к базе данных на свои
            string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query;

                if (string.IsNullOrEmpty(txt_Date.Text))
                {
                    query = $"insert into Savings(Id_income,_Amount)" +
                   $"values (\'{txtId_income.Text}\',\'{txt_Amiunt.Text}\');";
                }
                else
                {
                    query = $"insert into Savings(Id_income,_Date,_Amount)" +
                   $"values (\'{txtId_income.Text}\',\'{txt_Date.Text}\',\'{txt_Amiunt.Text}\');";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);


                cmd.ExecuteNonQuery();

                connection.Close();
            }
            // SuperUser superUser = new SuperUser();
            this.Close();
            //superUser.ShowDialog();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
