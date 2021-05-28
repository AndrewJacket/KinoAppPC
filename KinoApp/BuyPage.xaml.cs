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
        DataTable FullTable;

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
            string addplace = "";
            string mainplace = "";
            mainplace = $"SELECT row, place, price FROM PlaceSession WHERE (session_id ={SelectTimePage.session_}) AND (";
            for (int i = 0; i < PlacesPage.places_id_.Length; i++)
            {
                if (PlacesPage.places_id_[i] != null)
                {
                    addplace = $" (place_id = {PlacesPage.places_id_[i]}) OR";
                    mainplace = String.Concat(mainplace, addplace);
                }
            }
            mainplace = mainplace.Remove(mainplace.Length - 2);
            mainplace = String.Concat(mainplace, ")");
            BuyPlaces = ExecuteSql(mainplace);
            LViewPlacePrice.ItemsSource = BuyPlaces.DefaultView;


            mainplace = $"SELECT SUM(price) AS FullPrice FROM PlaceSession WHERE (session_id ={SelectTimePage.session_}) AND (";
            for (int i = 0; i < PlacesPage.places_id_.Length; i++)
            {
                if (PlacesPage.places_id_[i] != null)
                {
                    addplace = $" (place_id = {PlacesPage.places_id_[i]}) OR";
                    mainplace = String.Concat(mainplace, addplace);
                }
            }
            mainplace = mainplace.Remove(mainplace.Length - 2);
            mainplace = String.Concat(mainplace, ")");
            fullprice_.Text = "null";
            FullTable = new DataTable();
            SqlConnection connection = null;
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(mainplace, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                fullprice_.Text = reader[0].ToString();
            }
            reader.Close();
        }


        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            string addplace = "";
            string mainplace = "";
            string idOrder = "700000";
            mainplace = $"INSERT INTO Orders(order_id, session_id, place_id, customer_id, date_of_sale) VALUES ";
            for (int i = 0; i < PlacesPage.places_id_.Length; i++)
            {
                if (PlacesPage.places_id_[i] != null)
                {
                    string sql;
                    SqlConnection connection1 = null;
                    sql = "SELECT top(1) order_id from Orders Order by order_id desc;";
                    connection1 = new SqlConnection(connectionString);
                    SqlCommand command1 = new SqlCommand(sql, connection1);
                    connection1.Open();
                    SqlDataReader reader = command1.ExecuteReader();
                    int id = int.Parse(idOrder) + 1;
                    idOrder = id.ToString();
                    while (reader.Read())
                    {

                        idOrder = reader[0].ToString();
                        int id2 = int.Parse(idOrder) + 1;
                        idOrder = id2.ToString();
                    }
                    reader.Close();
                    addplace = $"({idOrder}, {SelectTimePage.session_}, {PlacesPage.places_id_[i]}, {MainWindow.id_}, '{DateTime.Now}'),";
                    mainplace = String.Concat(mainplace, addplace);
                }
            }
            mainplace = mainplace.Remove(mainplace.Length - 1);
            SqlConnection connection = null;
            connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(mainplace, connection);
            int num = command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Покупка прошла успешно.");
            new MenuWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }
    }
}
