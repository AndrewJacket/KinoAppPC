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
    /// Логика взаимодействия для SessionWindow.xaml
    /// </summary>
    public partial class SessionWindow : Window
    {
        static string connectionString;
        SqlDataAdapter adapter;
        DataTable NameMovie;

        public static string hall_id_ { get; set; }

        public SessionWindow()
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            movie.Text = "Информация отсутствует";
            rating.Text = "Информация отсутствует";
            hall.Text = "Информация отсутствует";
            date.Text = "Информация отсутствует";
            time.Text = "Информация отсутствует";
            string sql;
            NameMovie = new DataTable();
            SqlConnection connection = null;
            sql = "SELECT movie_title, rating, name_hall, CONVERT(varchar,date_of_session,106) as date_of_session, time_of_session, hall_id FROM SelectMovie WHERE(session_id = '" + SelectTimePage.session_ + "')";
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                movie.Text = reader[0].ToString();
                rating.Text = reader[1].ToString();
                hall.Text = reader[2].ToString();
                date.Text = reader[3].ToString();
                time.Text = reader[4].ToString();
                hall_id_ = reader[5].ToString();
                SelectPLaceFrame.Navigate(new PlacesPage());
                return;
            }
            reader.Close();
            

        }

        private void Close_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MenuWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }
    }
}
