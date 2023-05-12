using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class InsertUsers : Window
    {
        
        public InsertUsers()
        {
            InitializeComponent();
        }


        private void InsertUser_Click(object sender, RoutedEventArgs e)
        {
            DataBase _dataBase = new DataBase("root", "password");
            if(string.IsNullOrEmpty(comboBoxRole.Text) || string.IsNullOrEmpty(_txtLogin.Text) || string.IsNullOrEmpty(_txtPassword.Password))
            {
                MessageBox.Show(this, "Все полья обязательно!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _dataBase.OpenConnection();
            string query = $"create user `{_txtLogin.Text}`@`localhost` identified by \"{_txtPassword.Password}\";";
            string query2 = null;
            if (comboBoxRole.Text == "Администратор")
            {
                query2 = $"grant all privileges on familyfinancesdb.*  to `{_txtLogin.Text}`@`localhost`;";
            }
            else if (comboBoxRole.Text == "Супер Пользователь")
            {
                query2 = $"grant delete,update,insert,select, show view on familyfinancesdb.*  to `{_txtLogin.Text}`@`localhost`;";
            }
            else if(comboBoxRole.Text == "Пользователь")
            {
                query2 = $"grant insert,select, show view on familyfinancesdb.*  to `{_txtLogin.Text}`@`localhost`;";
            }

            try
            {
                // открыть соединение
                _dataBase.OpenConnection();

                // создать объект команды
                using (MySqlCommand command = new MySqlCommand())
                {
                    // установить соединение для команды
                    command.Connection = _dataBase.GetConnection();

                    // установить текст запроса
                    command.CommandText = query;

                    // выполнить запрос и получить количество затронутых строк
                    int rowsAffected = command.ExecuteNonQuery();
                }
                using (MySqlCommand command2 = new MySqlCommand())
                {
                    // установить соединение для команды
                    command2.Connection = _dataBase.GetConnection();

                    // установить текст запроса
                    command2.CommandText = query2;

                    // выполнить запрос и получить количество затронутых строк
                    int rowsAffected = command2.ExecuteNonQuery();
                    // вывести сообщение об успехе
                    MessageBox.Show(this, $"Успешно создан пользователь {_txtLogin.Text}@`localhost`", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Admin _admin = new Admin();
                this.Close();
                _admin.ShowDialog();
            }
            catch (Exception ex)
            {
                // вывести сообщение об ошибке
                MessageBox.Show(this, "Ошибка при создании пользователя: " + ex.Message,"Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                // закрыть соединение
                _dataBase.CloseConnection();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Admin _admin = new Admin();
            this.Close();
            _admin.ShowDialog();
        }
    }
}
