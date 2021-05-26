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
    /// Логика взаимодействия для PlacesPage.xaml
    /// </summary>
    public partial class PlacesPage : Page
    {
        public class CustomButton : Button
        {
            public string CustomProperty { get; set; }
        }

        static string connectionString;
        SqlDataAdapter adapter;

        static DataTable RowPlaceSessions;
        static DataTable RowSessions;
        static DataTable PlaceSessions;
        static DataTable PriceSessions;
        static DataTable Sessions;
        static DataTable BusyPlaces;


        public static string place_id_ { get; set; }
        public static string[] free_places;
        public static string[] busy_places;
        public CustomButton[] places_ = new CustomButton[100];

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

            RowPlaceSessions = ExecuteSql("SELECT row, place, session_id, place_id, price_id FROM PlaceSession WHERE (session_id ='" + SelectTimePage.session_ + "')");
            free_places = new string[RowPlaceSessions.Rows.Count];
            for (int i = 0; i < RowPlaceSessions.Rows.Count; i++)
            {
                free_places[i] = RowPlaceSessions.Rows[i]["place_id"].ToString().Trim();
            }

            RowPlaceSessions = ExecuteSql("SELECT row, place, session_id, place_id, price_id FROM PlaceSession WHERE (session_id ='" + SelectTimePage.session_ + "')");
            places_ = new CustomButton[RowPlaceSessions.Rows.Count];
            for (int i = 0; i < RowPlaceSessions.Rows.Count; i++)
            {
                places_[i] = new CustomButton();
                places_[i].Name =$"sid{i + 1}";
                places_[i].CustomProperty = $"{free_places[i]}";
                places_[i].Content = $"{RowPlaceSessions.Rows[i]["place"].ToString().Trim()}";
                places_[i].Height = 40;
                places_[i].Width = 70;
                places_[i].Margin = new Thickness(50, 0, 0, 35);
                //var bc = new BrushConverter();
                // (Brush)bc.ConvertFrom("#A43820");
                places_[i].Background = new LinearGradientBrush(Colors.LightBlue, Colors.SlateBlue, 90);
                places_[i].Click += PlaceBtn_Click;
                RowPlaces.Children.Add(places_[i]);
            }
           
           
            BusyPlaces = ExecuteSql("SELECT place_id, row, place FROM BusyPlaces WHERE (session_id ='" + SelectTimePage.session_ + "')");
            busy_places = new string[RowPlaceSessions.Rows.Count];
            if (BusyPlaces.Rows.Count != 0)
            {
                for (int i = 0; i < BusyPlaces.Rows.Count; i++)
                {
                    busy_places[i] = BusyPlaces.Rows[i]["place_id"].ToString().Trim();
                }
                for (int i = 0; i < RowPlaceSessions.Rows.Count; i++)
                {
                    for (int i2 = 0; i2 < BusyPlaces.Rows.Count; i2++)
                    {
                        if (free_places[i] == busy_places[i2])
                        {
                            places_[i].IsEnabled = false;
                        }
                    }
                }
            }

            
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
            RowSessions = ExecuteSql("SELECT row FROM Places  WHERE (hall_id ='" + SessionWindow.hall_id_ + "') GROUP BY row");
            LViewRow.ItemsSource = RowSessions.DefaultView;
            LViewRow.Items.Refresh();
            PriceSessions = ExecuteSql("SELECT CONVERT(varchar,price,100) as price FROM PlaceSession  WHERE (session_id ='" + SelectTimePage.session_ + "') GROUP BY price, row ORDER BY row");
            LViewPrice.ItemsSource = PriceSessions.DefaultView;
            LViewPrice.Items.Refresh();
            PlaceSessions = ExecuteSql("SELECT place FROM Places  WHERE (hall_id ='" + SessionWindow.hall_id_ + "') GROUP BY place ");
            LViewPlace.ItemsSource = PlaceSessions.DefaultView;
            LViewPlace.Items.Refresh();            

        }

        private void PlaceBtn_Click(object sender, RoutedEventArgs e)
        {
            place_id_ = ((CustomButton)sender).CustomProperty.ToString().Trim();
            ((CustomButton)sender).Background = new LinearGradientBrush(Colors.Red, Colors.DarkRed, 90);
            //MessageBox.Show(place_id_);



        }
    }
}
