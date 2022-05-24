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
using System.Text.RegularExpressions;
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
        int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year - 1) +
                (((endDate.Month > startDate.Month) ||
                ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            Regex validatePasswordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            MySqlDataReader mysql_result;
            int ageInYears = -1;
            try
            {
                ageInYears = GetDifferenceInYears(DateBirthday.SelectedDate.Value, DateTime.Today);

            }
            catch (Exception)
            {
                MessageBox.Show("Выберите дату рождения");
                
            }
            if (Login.Text.Length > 6 && Password.Password.Length > 6 && ageInYears > -1 && PasswordRetry.Password.Length > 6) {
               
                if (validateEmailRegex.IsMatch(Login.Text)) {
                    if (validatePasswordRegex.IsMatch(Password.Password)){
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
                                    MessageBox.Show("Пользователь существует");
                                    exist = true;
                                }
                            }

                            mysql_connection.Close();
                            if (!exist)
                            {
                                if (ageInYears > 18)
                                {
                                    mysql_query.CommandText = "INSERT INTO `manager`(`id`, `login`, `password`,`birthday`) VALUES (NULL,'" + Login.Text + "','" + Password.Password + "','"+DateBirthday.SelectedDate.Value.ToString("yyyy-MM-dd") + "');";
                                mysql_connection.Open();
                                mysql_query.ExecuteReader();
                                mysql_connection.Close();
                                
                                    MessageBoxResult result = MessageBox.Show("Успешно", "Авторизован", MessageBoxButton.OK);
                                    if (result == MessageBoxResult.OK)
                                    {
                                        Window3 window3 = new Window3();
                                        window3.Show();
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Вам нет 18 лет");
                                }
                                

                            }
                        } else
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                    } else { MessageBox.Show("Пароль должен содержать минимум 6 символов, миниммум одну заглавную букву, миниммум одну строчную букву и цифры.\n"); }
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
  

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
