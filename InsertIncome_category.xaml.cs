using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances
{
    public partial class InsertIncome_category : Window
    {
        public InsertIncome_category()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName_category.Text))
            {
                MessageBox.Show(this, "Все поля с * обязательно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Замените настройки подключения к базе данных на свои
            string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"insert into Income_category(Category_name)" +
                               $"values (\'{txtName_category.Text}\');";

                MySqlCommand cmd = new MySqlCommand(query, connection);


                cmd.ExecuteNonQuery();

                connection.Close();
            }
            // SuperUser superUser = new SuperUser();
            this.Close();
            //superUser.ShowDialog();

        }
    }
}
