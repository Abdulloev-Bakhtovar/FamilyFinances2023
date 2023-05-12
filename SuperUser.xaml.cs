using FamilyFinances.UserControls.InsertAllTables;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class SuperUser : Window
    {
        private void addItemComboBox()
        {
            DataBase _dataBase = new DataBase("superUser", "superUser");

            string Sql = "SELECT table_name  FROM information_schema.tables \r\nWHERE table_type = 'BASE TABLE' \r\nAND table_schema = 'familyfinancesdb';";
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
            DataBase _dataBase = new DataBase("superUser", "superUser");

            string query = $"select * from {_table};";
            MySqlCommand cmd = new MySqlCommand(query, _dataBase.GetConnection());
            DataTable dt = new DataTable();
            _dataBase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            _dataBase.CloseConnection();
            dataGridTables.ItemsSource = dt.DefaultView;
        }

        private static string _table;
        public SuperUser()
        {
            InitializeComponent();
            addItemComboBox();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            this.Close();
            _login.ShowDialog();
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

        // Update
        private void dataGridTables_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Получить измененное значение и столбец
            string newValue = ((TextBox)e.EditingElement).Text;
            string columnName = e.Column.Header.ToString();

            // Проверить значение на корректность и сохранить его в базе данных
            if (columnName == "Birth_day")
            {
                if (!DateTime.TryParse(newValue, out DateTime birthDay))
                {
                    MessageBox.Show("Пожалуйста, введите корректное значение для дату (yyyy-mm-dd).");
                    e.Cancel = true; // Отменить изменение, если значение некорректно
                    return;
                }
            }

            // Найти строку в DataTable, которую следует обновить
            DataRowView rowView = (DataRowView)e.Row.Item;
            DataRow row = rowView.Row;

            // Обновить базу данных
            UpdateDatabase(row, columnName, newValue);
        }
        private void UpdateDatabase(DataRow row, string columnName, string newValue)
        {
            // Замените настройки подключения к базе данных на свои
            string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = null;
                if (comboBoxTable.Text == "family")
                {
                    int primaryKey = Convert.ToInt32(row["Id_family"]);


                    query = $"UPDATE family SET {columnName} = '{newValue}' WHERE Id_family = {primaryKey}";
                }
                else if (comboBoxTable.Text == "income")
                {
                    int primaryKey = Convert.ToInt32(row["Id_income"]);


                    query = $"UPDATE Income SET {columnName} = '{newValue}' WHERE Id_income = {primaryKey}";
                }
                else if(comboBoxTable.Text == "income_category")
                {
                    int primaryKey = Convert.ToInt32(row["Id_income_category"]);


                    query = $"UPDATE income_category SET {columnName} = '{newValue}' WHERE Id_income_category = {primaryKey}";
                }
                else if (comboBoxTable.Text == "savings")
                {
                    int primaryKey = Convert.ToInt32(row["Id_savings"]);


                    query = $"UPDATE savings SET {columnName} = '{newValue}' WHERE Id_savings = {primaryKey}";
                }
                else if (comboBoxTable.Text == "expense")
                {
                    int primaryKey = Convert.ToInt32(row["Id_expense"]);


                    query = $"UPDATE expense SET {columnName} = '{newValue}' WHERE Id_expense = {primaryKey}";
                }
                else if (comboBoxTable.Text == "expense_category")
                {
                    int primaryKey = Convert.ToInt32(row["Id_category"]);


                    query = $"UPDATE Expense_category SET {columnName} = '{newValue}' WHERE Id_category = {primaryKey}";
                }
                else if (comboBoxTable.Text == "product_order")
                {
                    int primaryKey = Convert.ToInt32(row["Id_order"]);


                    query = $"UPDATE Product_order SET {columnName} = '{newValue}' WHERE Id_order = {primaryKey}";
                }
                else if (comboBoxTable.Text == "product")
                {
                    int primaryKey = Convert.ToInt32(row["Id_product"]);


                    query = $"UPDATE product SET {columnName} = '{newValue}' WHERE Id_product = {primaryKey}";
                }
                else if (comboBoxTable.Text == "product_category")
                {
                    int primaryKey = Convert.ToInt32(row["Id_category"]);


                    query = $"UPDATE product_category SET {columnName} = '{newValue}' WHERE Id_category = {primaryKey}";
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (DataRowView)dataGridTables.SelectedItem;
            if (selectedRow != null)
            {
                if (MessageBox.Show(this, "Вы действительно хотите удалять этот запись?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (comboBoxTable.Text == "family")
                    {
                        string IdToDelete = selectedRow["Id_family"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from Family where Id_family = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "income")
                    {
                        string IdToDelete = selectedRow["Id_income"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from income where Id_income = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                           
                        }
                    }
                    else if (comboBoxTable.Text == "income_category")
                    {
                        string IdToDelete = selectedRow["Id_income_category"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from income_category where Id_income_category = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                        }
                    }
                    else if (comboBoxTable.Text == "savings")
                    {
                        string IdToDelete = selectedRow["Id_savings"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from savings where Id_savings = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "expense")
                    {
                        string IdToDelete = selectedRow["Id_expense"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from expense where Id_expense = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "expense_category")
                    {
                        string IdToDelete = selectedRow["Id_category"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from expense_category where Id_category = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "product_order")
                    {
                        string IdToDelete = selectedRow["Id_order"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from Product_order where Id_order = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "product")
                    {
                        string IdToDelete = selectedRow["Id_product"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from product where Id_product = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                    else if (comboBoxTable.Text == "product_category")
                    {
                       
                        string IdToDelete = selectedRow["Id_category"].ToString();

                        // Создание и выполнение SQL-запроса DELETE
                        string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            string deleteQuery = $"delete from product_category where Id_category = {IdToDelete};";
                            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                            //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Пожалуйста, выберите строку для удаления.", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxTable.Text == "family")
            {
                InsertFamily _insertFamily = new InsertFamily();
                _insertFamily.Show();
            }
            else if (comboBoxTable.Text == "income")
            {
                InsertIncome insertIncome = new InsertIncome();
                insertIncome.Show();
            }
            else if (comboBoxTable.Text == "income_category")
            {
                InsertIncome_category insertIncome_Category = new InsertIncome_category();
                insertIncome_Category.Show();
            }
            else if (comboBoxTable.Text == "savings")
            {
                InsertSavings insertSavings = new InsertSavings();
                insertSavings.Show();  
            }
            else if (comboBoxTable.Text == "expense")
            {
                InsertExpense insertExpense = new InsertExpense();
                insertExpense.Show();
            }
            else if (comboBoxTable.Text == "expense_category")
            {
                InsertExpense_category insertExpense_Category = new InsertExpense_category();
                insertExpense_Category.Show();
            }
            else if (comboBoxTable.Text == "product_order")
            {
                InsertProduct_order insertProduct_Order = new InsertProduct_order();
                insertProduct_Order.Show();
            }
            else if (comboBoxTable.Text == "product")
            {
                InsertProduct insertProduct = new InsertProduct();
                insertProduct.Show();
            }
            else if (comboBoxTable.Text == "product_category")
            {
                Insertproduct_category insertproduct_Category = new Insertproduct_category();
                insertproduct_Category.Show();
            }
        }

        private void buttonQuery_Click(object sender, RoutedEventArgs e)
        {
            Querys querys = new Querys();
            //this.Close();
            querys.ShowDialog();
        }

        private void buttonViews_Click(object sender, RoutedEventArgs e)
        {
            buttonViews.Visibility = Visibility.Collapsed;
            Views views = new Views();
            views.ShowDialog();
        }
    }
}
