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
using System.Text.RegularExpressions;

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable Client;
        public EditPage()
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
                //Error.Text = "Ошибка подключения БД!!!";
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Error.Text = "";
            string sql;
            string phone_ = Phone.Text;
            if ((Name.Text.Trim() == "") && (DayOfB.Text.Trim() == "") && (Phone.Text.Trim() == "") && (Email.Text.Trim() == "") && (Login.Text.Trim() == "") && (Password.Text.Trim() == ""))
            {
                Error.Text = "Вы ничего не ввели.";
            }
            else
            if (Name.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели имя.";
            }
            else
            if (DayOfB.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели дату рождения.";
            }
            else
            if (Phone.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели номер телефона.";
            }
            else
            if (Email.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели свой email.";
            }
            else
            if (Login.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели логин.";
            }
            else
            if (Password.Text.Trim() == "")
            {
                Error.Text = "Вы не ввели пароль.";
            }
            else
            if ((Name.Text.Trim() != "") && (DayOfB.Text.Trim() != "") && (Phone.Text.Trim() != "") && (Email.Text.Trim() != "") && (Login.Text.Trim() != "") && (Password.Text.Trim() != ""))
            {
                const string namePattern = @"^[А-Яа-яЁё\s]+$";
                const string phonePattern = @"[0-9]{11,}";
                const string emailPattern = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                const string logpasPattern = @"^[A-Za-z0-9]{3,}$";

                var newname = Name.Text.Trim().ToLowerInvariant();
                var newphone = Phone.Text.Trim().ToLowerInvariant();
                var newemail = Email.Text.Trim().ToLowerInvariant();
                var newlogin = Login.Text.Trim().ToLowerInvariant();
                var newpassword = Password.Text.Trim().ToLowerInvariant();

                if (Regex.Match(newname, namePattern).Success)
                {
                    if (Regex.Match(newphone, phonePattern).Success)
                    {
                        if (Regex.Match(newemail, emailPattern).Success)
                        {
                            if (Regex.Match(newlogin, logpasPattern).Success)
                            {
                                if (Regex.Match(newpassword, logpasPattern).Success)
                                {
                                    Client = new DataTable();
                                    SqlConnection connection = null;
                                    sql = "UPDATE Customers SET name='" + Name.Text.Trim() + "', date_of_birth='" + DayOfB.Text.Trim() + "',phonenumber='" + Phone.Text.Trim() + "', email='" + Email.Text.Trim() + "', login='" + Login.Text.Trim() + "', password='" + Password.Text.Trim() + "' WHERE customer_id='" + MainWindow.id_.Trim() + "';";
                                    connection = new SqlConnection(connectionString);
                                    SqlCommand command = new SqlCommand(sql, connection);
                                    connection.Open();
                                    int num = command.ExecuteNonQuery();
                                    MainWindow.name_ = Name.Text.Trim();
                                    MainWindow.dob_ = DayOfB.Text.Trim();
                                    MainWindow.phone_ = Phone.Text.Trim();
                                    MainWindow.email_ = Email.Text.Trim();
                                    MainWindow.login_ = Login.Text.Trim();
                                    MainWindow.password_ = Password.Text.Trim();
                                    this.NavigationService.Navigate(new InfoPage());
                                    connection.Close();

                                }
                                else
                                    Error.Text = "Неверно изменён пароль. Пароль может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                            }
                            else
                                Error.Text = "Неверно изменён логин. Логин может содержать в себе только цифры латинские буквы, а так же быть не менее 3-х символов.";

                        }

                        else
                            Error.Text = "Неверно изменена почта. Пример ввода почты: Ivanov@mail.ru";

                    }
                    else
                        Error.Text = "Неверно изменён номер телефона.";
                }
                else
                    Error.Text = "Неверно изменено имя пользователя. Используйте только буквы кириллицы.";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (MainWindow.name_ != "")
            {
                Name.Text = MainWindow.name_;
            }
            if (MainWindow.dob_ != "")
            {
                DayOfB.Text = MainWindow.dob_;
            }
            if (MainWindow.phone_ != "")
            {

                Phone.Text = MainWindow.phone_;
            }
            if (MainWindow.email_ != "")
            {
                Email.Text = MainWindow.email_;
            }
            if (MainWindow.login_ != "")
            {
                Login.Text = MainWindow.login_;
            }

            if (MainWindow.password_ != "")
            {
                Password.Text = MainWindow.password_;

            }



        }



        private void Phone_TextInput(object sender, TextCompositionEventArgs e)
        {

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


    }
}
