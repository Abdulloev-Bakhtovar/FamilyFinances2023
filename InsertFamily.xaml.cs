using MySql.Data.MySqlClient;
using System.Windows;

namespace FamilyFinances.UserControls.InsertAllTables
{
    public partial class InsertFamily : Window
    {
        public InsertFamily()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
           if(string.IsNullOrEmpty(txtLast_name.Text) || string.IsNullOrEmpty(txtMiddle_name.Text)|| string.IsNullOrEmpty(txtFirst_name.Text) || string.IsNullOrEmpty(txt_Gender.Text)
                || string.IsNullOrEmpty(txtBirth_day.Text) || string.IsNullOrEmpty(cmb_Status.Text))
            {
                MessageBox.Show(this, "Все поля с * обязательно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            //string txtId1 = txtId.Text;
            string txtLast_name1 = txtLast_name.Text;
            string txtFirst_name1 = txtFirst_name.Text;
            string txtMiddle_name1 = txtMiddle_name.Text;
            string txt_Gender1 = txt_Gender.Text;
            string txtBirth_day1 = txtBirth_day.Text;
            string txt_Status1 = cmb_Status.Text;

            // Замените настройки подключения к базе данных на свои
            string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = $"INSERT INTO Family ( Last_name, First_name, Middle_name, _Gender, Birth_day, _Status) VALUES (@last_name, @first_name, @middle_name, @gender, @birth_day, @status);";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                // cmd.Parameters.AddWithValue("@id", txtId1);
                cmd.Parameters.AddWithValue("@last_name", txtLast_name1);
                cmd.Parameters.AddWithValue("@first_name", txtFirst_name1);
                cmd.Parameters.AddWithValue("@middle_name", txtMiddle_name1);
                cmd.Parameters.AddWithValue("@gender", txt_Gender1);
                cmd.Parameters.AddWithValue("@birth_day", txtBirth_day1);
                cmd.Parameters.AddWithValue("@status", txt_Status1);

                cmd.ExecuteNonQuery();

                connection.Close();
            }
            //SuperUser superUser = new SuperUser();
            this.Close();
           // superUser.ShowDialog(); 
        }

    }
}
