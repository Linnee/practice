using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
namespace shop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        
        public static string DateFormat = "dd-MM-yyyy";
        public static MySqlConnection mysql_connection;
        public static MySqlConnection GetConnection()
        {
            return mysql_connection;
        }

        public static bool Connect(string hostSQL, string userSQL, string passwordSQL)
        {
            Window2 windowConnect = new Window2();
            string Connect = "Database=" + "комиссионный_магазин" + ";Datasource=" + hostSQL + ";User=" + userSQL + ";Password=" + passwordSQL;
            
          
            try
            {
                
                mysql_connection = new MySqlConnection(Connect);
                MySqlCommand mysql_query = mysql_connection.CreateCommand();
                mysql_query.CommandText = "" +
                    "CREATE TABLE IF NOT EXISTS manager (id int AUTO_INCREMENT PRIMARY KEY, login varchar(255), password varchar(255),birthday date);\n" +
                    "CREATE TABLE IF NOT EXISTS people ( ФИО_сдатчика varchar(255), №_паспорта_сдатчика int, Адрес_сдатчика varchar(255), Инвентарный_номер_сданной_вещи int);\n" +
                    "CREATE TABLE IF NOT EXISTS items ( Инвентарный_номер_сданной_вещи int AUTO_INCREMENT, Цена int, Дата_сдачи DATE,Дата_переоценки DATE, Дата_продажи DATE, №_паспорта_сдатчика int, PRIMARY KEY (Инвентарный_номер_сданной_вещи) );\n" +
                    "ALTER TABLE people ADD FOREIGN KEY (Инвентарный_номер_сданной_вещи) REFERENCES items (Инвентарный_номер_сданной_вещи);";
                mysql_connection.Open();
                MySqlDataReader mysql_result;
                mysql_result = mysql_query.ExecuteReader();

               
                mysql_connection.Close();
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Отсутствует подключение к базе данных", "БД", MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                { 
                    windowConnect.Show();
                }
                return false;
            }
           return true;
        }

        public App()
        {
            Connect("localhost", "root", "password");

        }
    }
}
