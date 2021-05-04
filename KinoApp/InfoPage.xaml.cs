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
    /// Логика взаимодействия для UserInfo.xaml
    /// </summary>
    public partial class InfoPage : Page
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;
        public InfoPage()
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Name.Text = "Информация отсутствует";
            DayOfB.Text = "Информация отсутствует";
            Phone.Text = "Информация отсутствует";
            Email.Text = "Информация отсутствует";
            Login.Text = "Информация отсутствует";
            Password.Text = "Информация отсутствует";
            string sql;
            Client = new DataTable();
            SqlConnection connection = null;
            sql = "SELECT name, CONVERT(varchar,date_of_birth,106) as date_of_birth, phonenumber, email, login, password FROM Customers WHERE customer_id='" + MainWindow.id_ + "';";
            connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Name.Text = reader[0].ToString();
                DayOfB.Text = reader[1].ToString();
                Phone.Text = reader[2].ToString();
                Email.Text = reader[3].ToString();
                Login.Text = reader[4].ToString();
                Password.Text = reader[5].ToString();
                return;
            }
            reader.Close();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new EditPage());

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DeleteFrame.Navigate(new DeletePage());
        }
    }
}