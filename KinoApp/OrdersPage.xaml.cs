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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        static string connectionString;
        SqlDataAdapter adapter;
        DataTable Orders;
        static DataTable Table;

        public OrdersPage()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {

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
            Orders = ExecuteSql("SELECT movie_title, CONVERT(varchar,date_of_session,106) as date_of_session, time_of_session, name_hall, row, place, CONVERT(varchar,price,100) as price FROM OrdersCustomers WHERE (customer_id ='" + MainWindow.id_ + "')");
            LViewOrders.ItemsSource = Orders.DefaultView;
            LViewOrders.Items.Refresh();

        }
    }
}

