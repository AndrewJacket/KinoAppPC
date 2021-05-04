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
using System.Windows.Shapes;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Customer;

        public RegWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                //Error.Text = "подключено БД";
            }
            catch (SqlException)
            {
                Error.Text = "Ошибка подключения БД!!!";
            }
        }


        private void Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
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


        private void RegLogBtn_Click(object sender, RoutedEventArgs e)
        {
            string sql;
            string sql1;
            string sql2;
            string sql3;
            string sql4;

            Customer = new DataTable();
            SqlConnection connection = null;
            string idClient = null;
            //проверка на не пустоту поля
            Error.Text = "";
            if ((RegName.Text == "") && (RegDob.Text == "") && (RegPhone.Text == "") && (RegEmail.Text == "") && (RegLogin.Text == "") && (RegPassword.Text == ""))
            {
                Error.Text = "Вы ничего не ввели.";
            }
            else
            if (RegName.Text == "")
            {
                Error.Text = "Вы не ввели имя.";
            }
            else
            if (RegDob.Text == "")
            {
                Error.Text = "Вы не ввели дату рождения.";
            }
            else
            if (RegPhone.Text == "")
            {
                Error.Text = "Вы не ввели номер телефона.";
            }
            else
            if (RegEmail.Text == "")
            {
                Error.Text = "Вы не ввели почту.";
            }
            else
            if (RegLogin.Text == "")
            {
                Error.Text = "Вы не ввели логин.";
            }
            else
            if (RegPassword.Text == "")
            {
                Error.Text = "Вы не ввели пароль.";
            }
            else
            if (!string.IsNullOrEmpty(RegEmail.Text.Trim()))
            {
                const string namePattern = @"^[А-Яа-яЁё\s]+$";
                const string phonePattern = @"[0-9]{11,}";
                const string emailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                const string logpasPattern = @"^[A-Za-z0-9]{3,}$";

                var name = RegName.Text.Trim().ToLowerInvariant();
                var phone = RegPhone.Text.Trim().ToLowerInvariant();
                var email = RegEmail.Text.Trim().ToLowerInvariant();
                var login = RegLogin.Text.Trim().ToLowerInvariant();
                var password = RegPassword.Text.Trim().ToLowerInvariant();

                if (Regex.Match(name, namePattern).Success)
                {
                    if (Regex.Match(phone, phonePattern).Success)
                    {
                        if (Regex.Match(email, emailPattern).Success)
                        {
                            if (Regex.Match(login, logpasPattern).Success)
                            {
                                if (Regex.Match(password, logpasPattern).Success)
                                {
                                    MainWindow.name_ = RegName.Text.Trim();
                                    MainWindow.dob_ = RegDob.Text.Trim();
                                    MainWindow.phone_ = RegPhone.Text.Trim();
                                    MainWindow.email_ = RegEmail.Text.Trim();
                                    MainWindow.login_ = RegLogin.Text.Trim();
                                    MainWindow.password_ = RegPassword.Text.Trim();
                                    connection = new SqlConnection(connectionString);
                                    connection.Open();

                                    sql = "SELECT top(1) customer_id from Customers Order by customer_id desc;";
                                    SqlCommand command = new SqlCommand(sql, connection);
                                    SqlDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        idClient = reader[0].ToString();
                                        int id = int.Parse(idClient) + 1;
                                        idClient = id.ToString();
                                    }
                                    MainWindow.id_ = idClient;
                                    reader.Close();

                                    //connection = new SqlConnection(connectionString);
                                    sql4 = "INSERT INTO Customers (customer_id, name, date_of_birth, phonenumber, email, login, password, role) VALUES ('" + idClient + "','" + MainWindow.name_ + "','" + MainWindow.dob_ + "','" + MainWindow.phone_ + "','" + MainWindow.email_ + "','" + MainWindow.login_ + "','" + MainWindow.password_ + "',1);";
                                    sql1 = "Select * from Customers where login='" + MainWindow.login_ + "';";
                                    SqlCommand command1 = new SqlCommand(sql1, connection);
                                    SqlDataReader reader1 = command1.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        Error.Text = "Такой логин уже используется другим пользователем";
                                        return;
                                    }
                                    reader1.Close();

                                    sql2 = "Select * from Customers where email='" + MainWindow.email_ + "';";
                                    SqlCommand command2 = new SqlCommand(sql2, connection);
                                    SqlDataReader reader2 = command2.ExecuteReader();
                                    while (reader2.Read())
                                    {
                                        Error.Text = "Такая почта уже используется другим пользователем";
                                        return;
                                    }
                                    reader2.Close();

                                    sql3 = "Select * from Customers where phonenumber='" + MainWindow.phone_ + "';";
                                    SqlCommand command3 = new SqlCommand(sql3, connection);
                                    SqlDataReader reader3 = command3.ExecuteReader();
                                    while (reader3.Read())
                                    {
                                        Error.Text = "Такой номер телефона уже используется другим пользователем ";
                                        return;
                                    }
                                    reader3.Close();

                                    SuccessfulFrame.Content = new RegSuccessfulPage();
                                    if ((SuccessfulFrame.Content = new RegSuccessfulPage()) != null)
                                    {
                                        SqlCommand command4 = new SqlCommand(sql4, connection);
                                        int num = command4.ExecuteNonQuery();
                                        RegName.IsEnabled = false;
                                        RegDob.IsEnabled = false;
                                        RegPhone.IsEnabled = false;
                                        RegEmail.IsEnabled = false;
                                        RegLogin.IsEnabled = false;
                                        RegPassword.IsEnabled = false;
                                        RegLogBtn.IsEnabled = false;
                                        MainWindow.login_ = RegLogin.Text.Trim();
                                        MainWindow.password_ = RegPassword.Text.Trim();
                                        connection.Close();
                                    }
                                }
                                else
                                    Error.Text = "Неверно введен пароль. Пароль может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                            }
                            else
                                Error.Text = "Неверно введен логин. Логин может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                        }

                        else
                            Error.Text = "Неверно введена почта. Пример ввода почты: Ivanov@mail.ru";

                    }
                    else
                        Error.Text = "Неверно введен номер телефона.";
                }
                else
                    Error.Text = "Неверно введено имя пользователя. Используйте только буквы кириллицы.";
            }

        }
    }
}
