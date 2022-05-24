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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
namespace shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Window4 window4 = new Window4();
            window4.Show();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            Regex validatePasswordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$");
            MySqlDataReader mysql_result;
            if (Login.Text.Length > 6 && Password.Password.Length > 6)
            {

                if (validateEmailRegex.IsMatch(Login.Text))
                {
                    if (validatePasswordRegex.IsMatch(Password.Password))
                    {
                      
                            bool exist = false;
                            mysql_query.CommandText = "Select * from manager WHERE login='" + Login.Text + "'AND password='" + Password.Password + "' LIMIT 1;";
                            mysql_connection.Open();
                            mysql_result = mysql_query.ExecuteReader();

                            while (mysql_result.Read())
                            {
                                if (Login.Text == mysql_result.GetString(1) && Password.Password == mysql_result.GetString(2))
                                {
                                MessageBoxResult result = MessageBox.Show("Успешно", "Авторизован", MessageBoxButton.OK);
                                if (result == MessageBoxResult.OK)
                                {
                                    mysql_connection.Close();
                                    Window3 window3 = new Window3();
                                    window3.Show();
                                    exist = true;
                                    this.Close();
                                    break;
                                }
                            }
                            }

                            mysql_connection.Close();
                            if (exist==false)
                            {
                            MessageBox.Show("Пользователя не существует или данные ведены не корректно");
                            }
                        
                        
                    }
                    else { MessageBox.Show("Пароль должен содержать минимум 6 символов, миниммум одну заглавную букву, миниммум одну строчную букву и цифры.\n"); }
                }
                else
                {
                    MessageBox.Show("Введите email корректно ");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля\nМинимум 6 символов");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();

        }
    }
}
