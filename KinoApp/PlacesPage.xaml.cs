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
using System.Drawing;
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для PlacesPage.xaml
    /// </summary>
    public partial class PlacesPage : Page
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable RowPlaceSessions;
        static DataTable RowSessions;
        static DataTable PlaceSessions;

        public static string row_ { get; set; }
        public PlacesPage()
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
            PlaceSessions = new DataTable();
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    PlaceSessions.Load(reader);
                }
            }
            return PlaceSessions;

        }

       

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {  
            RowSessions = ExecuteSql("SELECT row FROM Places  WHERE (hall_id ='" + SessionWindow.hall_id_ + "') GROUP BY row");
            LViewRow.ItemsSource = RowSessions.DefaultView;
            PlaceSessions = ExecuteSql("SELECT place FROM Places  WHERE (hall_id ='" + SessionWindow.hall_id_ + "') GROUP BY place ");
            LViewPlace.ItemsSource = PlaceSessions.DefaultView;
            RowPlaceSessions = ExecuteSql("SELECT row, place, CONVERT(varchar,price,100) as price, session_id, place_id, price_id FROM PlaceSession WHERE (session_id ='" + SelectTimePage.session_ + "')");
            LViewRowPlace.ItemsSource = RowPlaceSessions.DefaultView;
        }

    }
}
