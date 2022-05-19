using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
namespace shop
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
            
            
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            MySqlDataReader mysql_result;
            if (Password.Password == PasswordRetry.Password)
            {
                bool exist = false;
                mysql_query.CommandText = "Select * from manager WHERE login='" + Login.Text + "'AND password='" + Password.Password + "' LIMIT 1;";
                mysql_connection.Open();
                mysql_result = mysql_query.ExecuteReader();
                
                while (mysql_result.Read())
                {
                    if (Login.Text == mysql_result.GetString(1) && Password.Password == mysql_result.GetString(2))
                    {
                        exist=true;
                    }
                }
                
                mysql_connection.Close();
                if (!exist)
                {
                    mysql_query.CommandText = "INSERT INTO `manager`(`id`, `login`, `password`) VALUES ('','" + Login.Text + "','" + Password.Password + "');";
                    mysql_connection.Open();
                    mysql_query.ExecuteReader();
                    mysql_connection.Close();
                }
            }

           
        }
  

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
