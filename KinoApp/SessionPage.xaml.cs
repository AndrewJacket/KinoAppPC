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
    /// Логика взаимодействия для SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {

       static  string connectionString;
                SqlDataAdapter adapter;
        static  DataTable Sessions;


        public SessionPage()
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


        static  DataTable ExecuteSql(string sql)
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
            DataTable Sessions = ExecuteSql("SELECT poster, movie_title, CONVERT(varchar,length,108) as length, genre, rating,name_hall,CONVERT(varchar, date_of_session, 100) as date_of_session FROM MoviesSessions;");
            LViewSessions.ItemsSource = Sessions.DefaultView;
           
        }
        
    }
       


    
}
