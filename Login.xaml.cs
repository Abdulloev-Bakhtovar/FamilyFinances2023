using System;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class Login : Window
    {
        /*private void addItemComboBox()
        {
            MySqlConnection MyConnect = new MySqlConnection($"Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;");
            string Sql = "select user from mysql.user";
            MyConnect.Open();
            MySqlCommand cmd = new MySqlCommand(Sql, MyConnect);
            MySqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBoxRole.Items.Add(DR[0]);
            }
            MyConnect.Close();
        }*/
        public Login()
        {
            InitializeComponent();
        }
        private void buttonLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxRole.SelectedItem == null)
            {
                MessageBox.Show(this, "Пожалуйста выберите роль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(_txtLogin.Visibility == Visibility.Visible && string.IsNullOrEmpty(_txtLogin.Text))
            {
                MessageBox.Show(this, "Пожалуйста введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_txtPassword.Visibility == Visibility.Visible && string.IsNullOrEmpty(_txtPassword.Password))
            {
                MessageBox.Show(this, "Пожалуйста введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            DataBase _dataBase;
            if (comboBoxRole.Text == "Гость")
            {
                try
                {
                    _dataBase = new DataBase("gaust", null);
                    _dataBase.OpenConnection();
                    MessageBox.Show(this, "Connect Gaust", "Except", MessageBoxButton.OK, MessageBoxImage.Information);
                    Gaust _gaust = new Gaust();
                    this.Close();
                    _gaust.ShowDialog();
                    
                    //_dataBase.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Error on Connect", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else if(comboBoxRole.Text == "Администратор")
            {
                
                try
                {
                    _dataBase = new DataBase(_txtLogin.Text, _txtPassword.Password);
                    _dataBase.OpenConnection();
                    MessageBox.Show(this, "Connect Admin", "Except", MessageBoxButton.OK, MessageBoxImage.Information);
                    Admin _admin = new Admin();
                    _admin.Show();
                    this.Close();
                    _dataBase.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Логин или пароль введены некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (comboBoxRole.Text == "Супер Пользователь")
            {
                try
                {
                    _dataBase = new DataBase(_txtLogin.Text, _txtPassword.Password);
                    _dataBase.OpenConnection();
                    MessageBox.Show(this, "Connect Super User", "Except", MessageBoxButton.OK, MessageBoxImage.Information);
                    _dataBase.CloseConnection();
                    SuperUser _superUser = new SuperUser();
                    this.Close();
                    _superUser.ShowDialog();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Логин или пароль введены некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (comboBoxRole.Text == "Пользователь")
            {
                try
                {
                    _dataBase = new DataBase(_txtLogin.Text, _txtPassword.Password);
                    _dataBase.OpenConnection();
                    MessageBox.Show(this, "Connect User", "Except", MessageBoxButton.OK, MessageBoxImage.Information);
                    _dataBase.CloseConnection();
                    User _user = new User();
                    this.Close();
                    _user.ShowDialog();
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Логин или пароль введены некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(this, "Пользовател не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _txtLogin.Clear();
            _txtPassword.Clear();
            _txtLogin.Focus();
        }
      
        private void comboBoxRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                string selectedValue = (comboBox.SelectedItem as ComboBoxItem).Content.ToString();
                
                if (selectedValue == "Гость")
                {
                    labelLogin.Visibility = Visibility.Collapsed;
                    _txtLogin.Visibility = Visibility.Collapsed;
                    labelPassword.Visibility = Visibility.Collapsed;
                    _txtPassword.Visibility = Visibility.Collapsed;
                    
                }
                else
                {
                    labelLogin.Visibility = Visibility.Visible;
                    _txtLogin.Visibility = Visibility.Visible;
                    labelPassword.Visibility = Visibility.Visible;
                    _txtPassword.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
