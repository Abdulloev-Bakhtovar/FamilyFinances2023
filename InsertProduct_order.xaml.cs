using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances
{
    public partial class InsertProduct_order : Window
    {
        public InsertProduct_order()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId_expense.Text) || string.IsNullOrEmpty(txtId_product.Text) || string.IsNullOrEmpty(txt_Quantity.Text))
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
                    query = $"insert into Product_order(Id_expense,Id_product,_Quantity))" +
                            $"values (\'{txtId_expense.Text}\',\'{txtId_product.Text}\',\'{txt_Quantity.Text}\');";
                }
                else
                {
                    query = $"insert into Product_order(Id_expense,Id_product,Order_date,_Quantity)" +
                            $"values (\'{txtId_expense.Text}\',\'{txtId_product.Text}\',\'{txt_Date.Text}\',\'{txt_Quantity.Text}\');";
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
