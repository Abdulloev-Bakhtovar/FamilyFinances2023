using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FamilyFinances
{
    public partial class Querys : Window
    {
        private void addItemDataGrid(int _table)
        {

            DataBase _dataBase = new DataBase("superUser", "superUser");

            string query = null;
            if (_table == 0)
            {
                 query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, SUM(i._Amount) AS Total_Income, SUM(s._Amount) AS Total_Savings\r\nFROM Family f\r\nJOIN Income i ON f.Id_family = i.Id_family\r\nJOIN Savings s ON i.Id_income = s.Id_income\r\nGROUP BY f.Id_family;\r\n";
            }
            else if (_table == 1) 
            {
                 query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, ec.Category_name, SUM(e._Amount) AS Total_Expense\r\nFROM Family f\r\nJOIN Expense e ON f.Id_family = e.Id_family\r\nJOIN Expense_category ec ON e.Id_expense_category = ec.Id_category\r\nGROUP BY f.Id_family, ec.Category_name;\r\n";
            }
            else if(_table == 2)
            {
                query = $"SELECT ic.Category_name, SUM(i._Amount) AS Total_Income, SUM(s._Amount) AS Total_Savings\r\nFROM Income_category ic\r\nJOIN Income i ON ic.Id_income_category = i.Id_income_category\r\nJOIN Savings s ON i.Id_income = s.Id_income\r\nGROUP BY ic.Category_name;\r\n";
            }
            else if (_table == 3)
            {
                query = $"SELECT p.Product_name, pc.Category_name, SUM(po._Quantity) AS Total_Ordered, SUM(e._Amount) AS Total_Expense\r\nFROM Product p\r\nJOIN Product_category pc ON p.Id_category = pc.Id_category\r\nJOIN Product_order po ON p.Id_product = po.Id_product\r\nJOIN Expense e ON po.Id_expense = e.Id_expense\r\nGROUP BY p.Product_name, pc.Category_name;\r\n";
            }
            else if (_table == 4)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, pc.Category_name, SUM(e._Amount) AS Total_Expense\r\nFROM Family f\r\nJOIN Expense e ON f.Id_family = e.Id_family\r\nJOIN Product_order po ON e.Id_expense = po.Id_expense\r\nJOIN Product p ON po.Id_product = p.Id_product\r\nJOIN Product_category pc ON p.Id_category = pc.Id_category\r\nGROUP BY f.Id_family, pc.Category_name;\r\n";
            }
            else if (_table == 5)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, SUM(i._Amount) AS Total_Income, SUM(s._Amount) AS Total_Savings, SUM(e._Amount) AS Total_Expense, SUM(i._Amount) - SUM(e._Amount) AS Balance\r\nFROM Family f\r\nJOIN Income i ON f.Id_family = i.Id_family\r\nJOIN Savings s ON i.Id_income = s.Id_income\r\nJOIN Expense e ON f.Id_family = e.Id_family GROUP BY f.Id_family;\r\n";
            }
            else if (_table == 6)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, p.Product_name, pc.Category_name, SUM(po._Quantity) AS Total_Ordered FROM Family f JOIN Expense e ON f.Id_family = e.Id_family JOIN Product_order po ON e.Id_expense = po.Id_expense JOIN Product p ON po.Id_product = p.Id_product JOIN Product_category pc ON p.Id_category = pc.Id_category GROUP BY f.Id_family, p.Product_name, pc.Category_name;";
            }
            else if (_table == 7)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, ic.Category_name, SUM(i._Amount) AS Total_Income, SUM(s._Amount) AS Total_Savings, SUM(e._Amount) AS Total_Expense FROM Family f JOIN Income i ON f.Id_family = i.Id_family JOIN Income_category ic ON i.Id_income_category = ic.Id_income_category JOIN Savings s ON i.Id_income = s.Id_income JOIN Expense e ON f.Id_family = e.Id_family GROUP BY f.Id_family, ic.Category_name;";
            }
            else if (_table == 8)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, p.Product_name, pc.Category_name, p._Fresh, p.Expiration_date, p.Coloric_content, p._Variety, SUM(po._Quantity) AS Total_Ordered, SUM(e._Amount) AS Total_Expense FROM Family f JOIN Expense e ON f.Id_family = e.Id_family JOIN Product_order po ON e.Id_expense = po.Id_expense JOIN Product p ON po.Id_product = p.Id_product JOIN Product_category pc ON p.Id_category = pc.Id_category GROUP BY f.Id_family, p.Product_name, pc.Category_name;";
            }
            else if (_table == 9)
            {
                query = $"SELECT f.Id_family, f.Last_name, f.First_Name, f.Middle_name, f._Gender, f.Birth_day, f._Status, pc.Category_name, SUM(i._Amount) AS Total_Income, SUM(s._Amount) AS Total_Savings, SUM(e._Amount) AS Total_Expense FROM Family f JOIN Income i ON f.Id_family = i.Id_family JOIN Savings s ON i.Id_income = s.Id_income JOIN Expense e ON f.Id_family = e.Id_family JOIN Product_order po ON e.Id_expense = po.Id_expense JOIN Product p ON po.Id_product = p.Id_product JOIN Product_category pc ON p.Id_category = pc.Id_category GROUP BY f.Id_family, pc.Category_name;";
            }


            MySqlCommand cmd = new MySqlCommand(query, _dataBase.GetConnection());
            DataTable dt = new DataTable();
            _dataBase.OpenConnection();
            MySqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            _dataBase.CloseConnection();
            if (dataGridTables != null)
            {
                dataGridTables.ItemsSource = dt.DefaultView;
            }

        }

        private static int _table;
        public Querys()
        {
            InitializeComponent();
            addItemDataGrid(_table);
        }



        private void comboBoxTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                _table = comboBox.SelectedIndex;
                addItemDataGrid(_table);
            }
        }
    }
}
