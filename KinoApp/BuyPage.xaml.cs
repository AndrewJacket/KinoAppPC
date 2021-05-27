using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для BuyPage.xaml
    /// </summary>
    public partial class BuyPage : Page
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Table;
        DataTable BuyPlaces;

        public BuyPage()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //подключено БД
            }
            catch (SqlException)
            {
                //Ошибка подключения БД!!
            }
        }
        static DataTable ExecuteSql(string sql)
        {
            Table = new DataTable();
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    Table.Load(reader);
                }
            }
            return Table;

        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //BuyPlaces = ExecuteSql("SELECT row, place, price FROM PlaceSession WHERE ((session_id ='" + SelectTimePage.session_ + "') AND (place_id ='" + PlacesPage.places_id_[((PlacesPage.CustomButton)sender).Num] + "'))");
            LViewPlacePrice.ItemsSource = BuyPlaces.DefaultView;
            LViewPlacePrice.Items.Refresh();
           
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
