using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для DeletePage.xaml
    /// </summary>
    public partial class DeletePage : Page
    {

        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;
        public DeletePage()
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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            Client = new DataTable();
            SqlConnection connection = null;
            sql = "DELETE FROM Customers WHERE customer_id='" + MainWindow.id_ + "'; ";
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            int num = command.ExecuteNonQuery();
            new MainWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
            connection.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
        }
    }
}
