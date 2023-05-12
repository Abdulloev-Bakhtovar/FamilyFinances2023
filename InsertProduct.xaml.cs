using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances
{
    public partial class InsertProduct : Window
    {
        public InsertProduct()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId_category.Text) || string.IsNullOrEmpty(txtproduct_name.Text) || string.IsNullOrEmpty(txtColoric_content.Text) 
                || string.IsNullOrEmpty(txt_Fresh.Text) || string.IsNullOrEmpty(cmb_Veariety.Text))
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

                /*if (string.IsNullOrEmpty(txt_Date.Text))
                {
                    query = $"insert into Income(Id_family,Id_income_category,_Amount)" +
                            $"values (\'{txtId_family.Text}\',\'{txtId_category.Text}\',\'{txt_Amiunt.Text}\');";
                }
                else
                {*/
                query = $"INSERT INTO Product (Id_category, Product_name, _Fresh, Coloric_content, _Variety)" +
                        $"values (\'{txtId_category.Text}\',\'{txtproduct_name.Text}\',\'{txt_Fresh.Text}\',\'{txtColoric_content.Text}\','{cmb_Veariety.Text}');";
                

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
