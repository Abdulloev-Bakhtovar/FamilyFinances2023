using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances
{
    public partial class InsertIncome : Window
    {
        public InsertIncome()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId_family.Text) || string.IsNullOrEmpty(txtId_category.Text) || string.IsNullOrEmpty(txt_Amiunt.Text))
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
                    query = $"insert into Income(Id_family,Id_income_category,_Amount)" +
                            $"values (\'{txtId_family.Text}\',\'{txtId_category.Text}\',\'{txt_Amiunt.Text}\');";
                }
                else
                {
                    query = $"insert into Income(Id_family,Id_income_category,_Date,_Amount)" +
                            $"values (\'{txtId_family.Text}\',\'{txtId_category.Text}\',\'{txt_Date.Text}\',\'{txt_Amiunt.Text}\');";
                }

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
