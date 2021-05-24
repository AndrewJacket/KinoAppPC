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
        static DataTable PriceSessions;
        static DataTable Sessions;
        static DataTable BusyPlaces;


      //  public static int max_place { get; set; }
     //   public static int max_row { get; set; }

        public static int[] free_places;
        public static int[] busy_places = new int[1000];
        public Button[] places_;
        private int count_place;
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
/*
            RowSessions = ExecuteSql("SELECT maxrow FROM MaxRow WHERE (hall_id ='" + SessionWindow.hall_id_ + "')");
            max_row = int.Parse(RowSessions.Rows[0]["maxrow"].ToString().Trim());
            PlaceSessions = ExecuteSql("SELECT maxplace  FROM MaxPlace WHERE (hall_id ='" + SessionWindow.hall_id_ + "')");
            max_place = int.Parse(PlaceSessions.Rows[0]["maxplace"].ToString().Trim());
 */     
            BusyPlaces = ExecuteSql("SELECT count_place FROM CountPlaces WHERE (session_id ='" + SelectTimePage.session_ + "')");
            count_place = int.Parse(BusyPlaces.Rows[0]["count_place"].ToString().Trim());


            for (int i = 0; i < count_place; i++)
            {
                //if (i = max_place)
                var tempbut = new Button() { Name = $"sid{i + 1}", Content = $"{RowPlaceSessions.Rows[i]["place"].ToString().Trim()}", Height = 40, Width = 70, Margin = new Thickness(50, 0, 0, 35) };

                RowPlaces.Children.Add(tempbut);
                //tempbut.Size = new System.Drawing.Size(50, 50);
                //places_[i].Location = new System.Drawing.Point(_x, _y);
                //places_[i].Click += new EventHandler(Form1_Click);//вставить строку сюда
                //this.Controls.Add(places_[i]);
            }

            /*
                        for (int i = 0; i == count_place; i++)
                        {
                           // places_[i] = RowPlaceSessions.Rows[i + 1]["place"].ToString().Trim();
                        }
            
            //LViewRowPlace.SelectedIndex[1].IsEnabled = false;

            for (int i = 0; i < count_place; i++)
            {
                for (int i2 = 0; i2 < count_place; i2++)
                {
                    if (free_places[i] == busy_places[i2])
                    {
                        // LViewRowPlace.Items[free_places[i]].;
                        // LViewRowPlace.IsHitTestVisible  = false;
                        //  LViewRowPlace.
                    }
                }
            }
            
            for (int i = 0; i < maxsid; i++)
            {
                var tempbut = new Button() { Height = 30, Width = 30, Name = $"sid{i + 1}", Margin=new Thickness(10)};

                RowPlaces.Children.Add(tempbut);
                //tempbut.Size = new System.Drawing.Size(50, 50);
                //places_[i].Location = new System.Drawing.Point(_x, _y);
                //places_[i].Click += new EventHandler(Form1_Click);//вставить строку сюда
                //this.Controls.Add(places_[i]);
            }
            */
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

            // RowPlaceSessions = ExecuteSql("SELECT row, place, session_id, place_id, price_id FROM PlaceSession WHERE (session_id ='" + SelectTimePage.session_ + "')");
            //LViewRowPlace.ItemsSource = RowPlaceSessions.DefaultView;

           

            RowPlaceSessions = ExecuteSql("SELECT row, place, session_id, place_id, price_id FROM PlaceSession WHERE (session_id ='" + SelectTimePage.session_ + "')");
            free_places = new int[RowPlaceSessions.Rows.Count];


            for (int i = 0; i < count_place; i++)
            {
                free_places[i] = int.Parse(RowPlaceSessions.Rows[i]["place_id"].ToString().Trim());
            }
            BusyPlaces = ExecuteSql("SELECT place_id, row, place FROM BusyPlaces WHERE (session_id ='" + SelectTimePage.session_ + "')");
            if (BusyPlaces.Rows.Count != 0)
            {
                for (int i = 0; i < count_place; i++)
                {
                    busy_places[i] = int.Parse(BusyPlaces.Rows[i]["place_id"].ToString().Trim());
                }
            }


        }

        
    }
}
