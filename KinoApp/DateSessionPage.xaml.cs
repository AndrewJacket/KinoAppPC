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
    /// Логика взаимодействия для DateSessionPage.xaml
    /// </summary>
    public partial class DateSessionPage : Page
    {
        static string connectionString;
        SqlDataAdapter adapter;
        static DataTable Sessions;
        static DataTable DeleteSessions;
        public static string movie_id_ { get; set; }
        public static string day_session_ { get; set; }
        public DateSessionPage()
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

            string sql;
            DeleteSessions = new DataTable();
            SqlConnection connection1 = null;
            sql = "DELETE FROM Sessions WHERE date_of_session <'" + SessionPage.DateToday + "'; ";
            connection1 = new SqlConnection(connectionString);
            SqlCommand command1 = new SqlCommand(sql, connection1);
            connection1.Open();
            int num = command1.ExecuteNonQuery();
            connection1.Close();
        }


        static DataTable ExecuteSql(string sql)
        {
            Sessions = new DataTable();
            SqlConnection connection = null;

            connection = new SqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    Sessions.Load(reader);
                }
            }
            return Sessions;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //хранимая процедура
            Sessions = ExecuteSql("SELECT poster, movie_title, CONVERT(varchar,length,108) as length, genre, rating, CONVERT(varchar,date_of_session,106) as date_of_session, movie_id  FROM MoviesSessions WHERE (date_of_session='" + SessionPage.Date + "');");
            LViewSessions.ItemsSource = Sessions.DefaultView;
        }

        private void LViewSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            movie_id_ = Sessions.Rows[LViewSessions.SelectedIndex]["movie_id"].ToString().Trim();
            day_session_ = Sessions.Rows[LViewSessions.SelectedIndex]["date_of_session"].ToString().Trim();
            SelectTimeFrame.Navigate(new SelectTimePage());


        }
    }
}