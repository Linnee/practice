using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Reflection;
namespace shop
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd-MM-yyyy";
        }
        public Window3()
        {
            InitializeComponent();
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            MySqlDataReader mysql_result;
            mysql_query.CommandText = "Select * from people";
            mysql_connection.Open();
            mysql_result = mysql_query.ExecuteReader();
            List<Item> items = new List<Item>();
            List<People> PeopleList = new List<People>();
            while (mysql_result.Read())
            {
               PeopleList.Add(new People { Name=mysql_result.GetString(0), Pasport = mysql_result.GetInt32(1), Address = mysql_result.GetString(2), Item_Id = mysql_result.GetInt32(3)});
                
            }

            mysql_connection.Close();
            mysql_query.CommandText = "Select * from items";
            mysql_connection.Open();
            mysql_result = mysql_query.ExecuteReader();
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "dd-MM-yyyy";
            format.DateSeparator = "-";
            while (mysql_result.Read())
            {
              
                items.Add(new Item { Item_Id = mysql_result.GetInt32(0), Price = mysql_result.GetInt32(1), DateDelivery = Convert.ToDateTime(mysql_result.GetDateTime(2), format), DateRevaluation = Convert.ToDateTime(mysql_result.GetDateTime(3), format), DateSale = Convert.ToDateTime(mysql_result.GetDateTime(4), format), Passport = mysql_result.GetInt32(5) });
            }
            PeopleGrid.ItemsSource = PeopleList;
            ItemGrid.ItemsSource = items;
        }

        private void PeopleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            MySqlDataReader mysql_result;
            var temp = SearchId.Text.Length > 0 ? SearchId.Text : "0";
            mysql_query.CommandText = "Select * from people WHERE `№_паспорта_сдатчика` = " + temp+";";
            mysql_connection.Open();
            mysql_result = mysql_query.ExecuteReader();
            List<People> PeopleList = new List<People>();
            while (mysql_result.Read())
            {
                PeopleList.Add(new People { Name = mysql_result.GetString(0), Pasport = mysql_result.GetInt32(1), Address = mysql_result.GetString(2), Item_Id = mysql_result.GetInt32(3) });

            }

            mysql_connection.Close();
            PeopleGrid.ItemsSource = PeopleList;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MySqlConnection mysql_connection = App.GetConnection();
            MySqlCommand mysql_query = mysql_connection.CreateCommand();
            MySqlDataReader mysql_result;
            var temp = SearchIdItem.Text.Length > 0 ? SearchIdItem.Text : "0";
            mysql_query.CommandText = "Select * from items WHERE `№_паспорта_сдатчика` = " + temp + ";";
            mysql_connection.Open();
            mysql_result = mysql_query.ExecuteReader();
            List<Item> items = new List<Item>();
            DateTimeFormatInfo format = new DateTimeFormatInfo();
            format.ShortDatePattern = "dd-MM-yyyy";
            format.DateSeparator = "-";
            while (mysql_result.Read())
            {
                items.Add(new Item { Item_Id = mysql_result.GetInt32(0), Price = mysql_result.GetInt32(1), DateDelivery = Convert.ToDateTime(mysql_result.GetDateTime(2), format), DateRevaluation = Convert.ToDateTime(mysql_result.GetDateTime(3), format), DateSale = Convert.ToDateTime(mysql_result.GetDateTime(4), format), Passport = mysql_result.GetInt32(5) });
            }

            mysql_connection.Close();
            ItemGrid.ItemsSource = items;
        }
    }
}
