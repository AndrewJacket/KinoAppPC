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
    /// Логика взаимодействия для SelectTimePage.xaml
    /// </summary>
    public partial class SelectTimePage : Page
    {

        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable TimeSessions;

        public static string session_ { get; set; }

        public SelectTimePage()
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
            TimeSessions = new DataTable();
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    TimeSessions.Load(reader);
                }
            }
            return TimeSessions;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //хранимая процедура
            TimeSessions = ExecuteSql("SELECT CONVERT(varchar,time_of_session,108) as time_of_session, session_id FROM SelectTime WHERE ((movie_id='" + DateSessionPage.movie_id_ + "') and (date_of_session ='" + DateSessionPage.day_session_ + "'))");
            LViewTime.ItemsSource = TimeSessions.DefaultView;
        }


        private void LViewTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            session_ = TimeSessions.Rows[LViewTime.SelectedIndex]["session_id"].ToString().Trim();
            new SessionWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
