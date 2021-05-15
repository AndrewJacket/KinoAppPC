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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Custumers;
        DataTable Admins;




        public static string id_ { get; set; }
        public static string email_ { get; set; }
        public static string phone_ { get; set; }
        public static string login_ { get; set; }
        public static string password_ { get; set; }
        public static string role_ { get; set; }
        public static string name_ { get; set; }
        public static string dob_ { get; set; }
        public static string surname_ { get; set; }
        public static string patronymic_ { get; set; }




        public MainWindow()
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
                Error.Text = "Ошибка подключения БД";
            }
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            string sql2;
            Custumers = new DataTable();
            Admins = new DataTable();
            password_ = Password.Text.Trim();
            login_ = Login.Text.Trim();
            SqlConnection connection = null;
            Error.Text = "";
            if ((Login.Text == "") && (Password.Text == ""))
            {
                Error.Text = "Вы ничего не ввели. Повторите попытку.";
            }
            else
            if (Login.Text == "")
            {
                Error.Text = "Вы не ввели логин. Повторите попытку.";
            }
            else
            if (Password.Text == "")
            {
                Error.Text = "Вы не ввели пароль. Повторите попытку.";
            }
            else
            {
                //string role;
                sql = "SELECT customer_id,email,phonenumber,login,password,role,name,convert(varchar,date_of_birth,106) FROM Customers where ((login ='" + login_ + "') and (password ='" + password_ + "'))";
                sql2 = "SELECT admin_id,admin_email,admin_pn,admin_login,admin_password,admin_role,admin_name,convert(varchar,admin_dob,106) as admin_dob,admin_surname,admin_patronymic from Admins where ((admin_login ='" + login_ + "') and (admin_password ='" + password_ + "'))";
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    id_ = reader[0].ToString();
                    email_ = reader[1].ToString();
                    phone_ = reader[2].ToString();
                    login_ = reader[3].ToString();
                    password_ = reader[4].ToString();
                    role_ = reader[5].ToString();
                    name_ = reader[6].ToString();
                    dob_ = reader[7].ToString();
                    new MenuWindow().Show();
                    this.Close();
                    return;
                }
                reader.Close();
                SqlCommand command2 = new SqlCommand(sql2, connection);
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    id_ = reader[0].ToString();
                    email_ = reader[1].ToString();
                    phone_ = reader[2].ToString();
                    login_ = reader[3].ToString();
                    password_ = reader[4].ToString();
                    role_ = reader[5].ToString();
                    name_ = reader[6].ToString();
                    dob_ = reader[7].ToString();
                    surname_ = reader[8].ToString();
                    patronymic_ = reader[9].ToString();
                   // new MenuWindow().Show();
                    this.Close();
                    return;
                }
                reader2.Close();

                Error.Text = "Неверный логин или пароль";
                Login.Text = "";
                Password.Text = "";

                connection.Close();
            }
        }




        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }



    } 
}