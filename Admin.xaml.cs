using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class Admin : Window
    {
        /*private void addItemDataGrid()
        {
            MySqlConnection MyConnect = new MySqlConnection("Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;");

            //string query = "SELECT \r\n    `user`.`User` AS 'User_name',\r\n    `user`.`Host` AS 'Host',\r\n    `user`.`authentication_string` AS 'Password (encrypted)',\r\n    `db`.`Select_priv` AS 'SELECT',\r\n    `db`.`Insert_priv` AS 'INSERT',\r\n    `db`.`Update_priv` AS 'UPDATE',\r\n    `db`.`Delete_priv` AS 'DELETE',\r\n    `db`.`Alter_priv` AS 'ALTER',\r\n    `db`.`Drop_priv` AS 'DROP',\r\n    `db`.`Show_view_priv` AS 'SHOW VIEW',\r\n    `db`.`Grant_priv` AS 'GRANT'\r\nFROM\r\n    `mysql`.`user` AS `user`\r\nJOIN\r\n    `mysql`.`db` AS `db` ON `user`.`User` = `db`.`User` AND `user`.`Host` = `db`.`Host`\r\nWHERE\r\n    `db`.`Db` = 'familyfinancesdb'\r\n\r\nUNION\r\n\r\nSELECT\r\n    `user`.`User` AS 'User_name',\r\n    `user`.`Host` AS 'Host',\r\n    `user`.`authentication_string` AS 'Password (encrypted)',\r\n    `user`.`Select_priv` AS 'SELECT',\r\n    `user`.`Insert_priv` AS 'INSERT',\r\n    `user`.`Update_priv` AS 'UPDATE',\r\n    `user`.`Delete_priv` AS 'DELETE',\r\n    `user`.`Alter_priv` AS 'ALTER',\r\n    `user`.`Drop_priv` AS 'DROP',\r\n    `user`.`Show_view_priv` AS 'SHOW VIEW',\r\n    `user`.`Grant_priv` AS 'GRANT'\r\nFROM\r\n    `mysql`.`user` AS `user`\r\nWHERE\r\n    `user`.`User` IN ('root', 'gaust');\r\n";
            //string query = "WITH db_privileges AS (\r\n  SELECT\r\n    `user`.`User` AS 'User_name',\r\n    `user`.`Host` AS 'Host',\r\n    `user`.`authentication_string` AS 'Password (encrypted)',\r\n    `db`.`Db` AS 'Db',\r\n    CONCAT_WS(', ',\r\n      IF(`db`.`Select_priv` = 'Y', 'SELECT', NULL),\r\n      IF(`db`.`Insert_priv` = 'Y', 'INSERT', NULL),\r\n      IF(`db`.`Update_priv` = 'Y', 'UPDATE', NULL),\r\n      IF(`db`.`Delete_priv` = 'Y', 'DELETE', NULL),\r\n      IF(`db`.`Create_priv` = 'Y', 'CREATE', NULL),\r\n      IF(`db`.`Drop_priv` = 'Y', 'DROP', NULL),\r\n      IF(`db`.`Grant_priv` = 'Y', 'GRANT', NULL),\r\n      IF(`db`.`Alter_priv` = 'Y', 'ALTER', NULL),\r\n      IF(`db`.`Show_view_priv` = 'Y', 'SHOW VIEW', NULL)\r\n    ) AS 'Privileges'\r\n  FROM\r\n    `mysql`.`user` AS `user`\r\n  LEFT JOIN\r\n    `mysql`.`db` AS `db`\r\n    ON `user`.`User` = `db`.`User` AND `user`.`Host` = `db`.`Host`\r\n  WHERE\r\n    `db`.`Db` = 'familyfinancesdb'\r\n),\r\ntable_column_privileges AS (\r\n  SELECT\r\n    `User_name`,\r\n    `Host`,\r\n    `Password (encrypted)`,\r\n    `Db`,\r\n    GROUP_CONCAT(DISTINCT `Privilege` ORDER BY `Privilege`) AS 'Privileges'\r\n  FROM\r\n  (\r\n    SELECT\r\n      `user`.`User` AS 'User_name',\r\n      `user`.`Host` AS 'Host',\r\n      `user`.`authentication_string` AS 'Password (encrypted)',\r\n      `table_privileges`.`TABLE_SCHEMA` AS 'Db',\r\n      `table_privileges`.`Privilege_type` AS 'Privilege'\r\n    FROM\r\n      `mysql`.`user` AS `user`\r\n    JOIN\r\n      `INFORMATION_SCHEMA`.`TABLE_PRIVILEGES` AS `table_privileges`\r\n      ON CONCAT('\\'', `user`.`User`, '\\'@\\'', `user`.`Host`, '\\'') = `table_privileges`.`GRANTEE`\r\n    WHERE\r\n      `table_privileges`.`TABLE_SCHEMA` = 'familyfinancesdb'\r\n    UNION\r\n    SELECT\r\n      `user`.`User`,\r\n      `user`.`Host`,\r\n      `user`.`authentication_string`,\r\n      `column_privileges`.`TABLE_SCHEMA` AS 'Db',\r\n      `column_privileges`.`Privilege_type` AS 'Privilege'\r\n    FROM\r\n      `mysql`.`user` AS `user`\r\n    JOIN\r\n      `INFORMATION_SCHEMA`.`COLUMN_PRIVILEGES` AS `column_privileges`\r\n      ON CONCAT('\\'', `user`.`User`, '\\'@\\'', `user`.`Host`, '\\'') = `column_privileges`.`GRANTEE`\r\n    WHERE\r\n      `column_privileges`.`TABLE_SCHEMA` = 'familyfinancesdb'\r\n  ) AS combined_privileges\r\n  GROUP BY\r\n    `User_name`, `Host`, `Password (encrypted)`, `Db`\r\n),\r\nroot_user AS (\r\n  SELECT\r\n    `user`.`User` AS 'User_name',\r\n    `user`.`Host` AS 'Host',\r\n    `user`.`authentication_string` AS 'Password (encrypted)',\r\n    'familyfinancesdb' AS 'Db',\r\n    CONCAT_WS(', ',IF(`user`.`Select_priv` = 'Y', 'SELECT', NULL),\r\n      IF(`user`.`Insert_priv` = 'Y', 'INSERT', NULL),\r\n      IF(`user`.`Update_priv` = 'Y', 'UPDATE', NULL),\r\n      IF(`user`.`Delete_priv` = 'Y', 'DELETE', NULL),\r\n      IF(`user`.`Create_priv` = 'Y', 'CREATE', NULL),\r\n      IF(`user`.`Drop_priv` = 'Y', 'DROP', NULL),\r\n      IF(`user`.`Grant_priv` = 'Y', 'GRANT', NULL),\r\n      IF(`user`.`Alter_priv` = 'Y', 'ALTER', NULL),\r\n      IF(`user`.`Show_view_priv` = 'Y', 'SHOW VIEW', NULL)\r\n    ) AS 'Privileges'\r\n  FROM\r\n    `mysql`.`user` AS `user`\r\n  WHERE\r\n    `user`.`User` = 'root'\r\n)\r\nSELECT * FROM db_privileges\r\nUNION\r\nSELECT * FROM table_column_privileges\r\nWHERE (User_name, Host) NOT IN (SELECT User_name, Host FROM db_privileges)\r\nUNION\r\nSELECT * FROM root_user\r\nORDER BY User_name, Host;";
            string query = "SELECT \r\n    ROW_NUMBER() OVER (ORDER BY `user`.`User`) AS 'Id',\r\n    `user`.`User` AS 'User_name',\r\n    `user`.`authentication_string` AS 'Password'\r\nFROM\r\n    `mysql`.`user` AS `user`\r\nWHERE\r\n    `user`.`User` NOT LIKE 'mysql%';";
            MySqlCommand cmd = new MySqlCommand(query, MyConnect);
            DataTable dt = new DataTable();
            MyConnect.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            MyConnect.Close();
            dataGridTables.ItemsSource = dt.DefaultView;
        }*/
        public Admin()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            this.Close();
            _login.ShowDialog();
        }

        private void buttonInsert_Click(object sender, RoutedEventArgs e)
        {
            InsertUsers _insertUsers = new InsertUsers();
            this.Close();
            _insertUsers.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection MyConnect = new MySqlConnection($"Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;");

            string query = "SELECT \r\n    ROW_NUMBER() OVER (ORDER BY `user`.`User`) AS 'Id',\r\n    `user`.`User` AS 'User_name',\r\n    `user`.`authentication_string` AS 'Password',\r\n    CASE\r\n        WHEN `user`.`User` LIKE 'root%' THEN 'Администратор'\r\n        WHEN `user`.`User` LIKE 'super%' THEN 'Супер Пользователь'\r\n        WHEN `user`.`User` LIKE 'user%' THEN 'Пользователь'\r\n        ELSE 'Гость'\r\n    END AS 'Role'\r\nFROM\r\n    `mysql`.`user` AS `user`\r\nWHERE\r\n    `user`.`User` NOT LIKE 'mysql%';";
            MySqlCommand cmd = new MySqlCommand(query, MyConnect);
            DataTable dt = new DataTable();
            MyConnect.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            MyConnect.Close();
            dataGridTables.ItemsSource = dt.DefaultView;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = (DataRowView)dataGridTables.SelectedItem;
            if (selectedRow != null)
            {
                if (MessageBox.Show(this, "Вы действительно хотите удалять ползователя?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Получение значения столбца user_name
                    string userNameToDelete = selectedRow["User_name"].ToString();

                    // Создание и выполнение SQL-запроса DELETE
                    string connectionString = "Server=localhost; Port=3306; Database=FamilyFinancesDb; Username=root; Password=password;";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = $"drop user `{userNameToDelete}`@`localhost`;";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        //deleteCommand.Parameters.AddWithValue("@user", userNameToDelete);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        Admin _admin = new Admin();
                        this.Close();
                        _admin.ShowDialog();
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Пожалуйста, выберите строку для удаления.", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /*private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataTable changes = ((DataView)dataGridTables.ItemsSource).Table.GetChanges();

            if (changes != null)
            {
                foreach (DataRow row in changes.Rows)
                {
                    string userName = row["User_name"].ToString();
                    string password = row["Password"].ToString();
                    for (int i = 0; i < changes.Columns.Count; i++)
                    {
                        DataColumn column = changes.Columns[i];
                        string columnName = column.ColumnName;
                        if (columnName != "Id") // добавьте эту строку
                        {
                            string newValue = row[columnName].ToString();

                            // Сохраните изменения в базе данных
                            UpdateDatabase(userName, password, columnName, newValue);
                        }
                    }
                }
            }
        }


        private void UpdateDatabase(string userName, string password, string columnName, string newValue)
        {
            // Замените настройки подключения к базе данных на свои
            string connectionString = "Server=localhost; Port=3306; Database=mysql; Username=root; Password=password;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Формируем запрос на обновление
                string query = $"UPDATE mysql.user SET {columnName} = '{newValue}' WHERE User = '{userName}'";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }*/

    }
}
