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

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
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
                new MenuWindow().Show();
                this.Close();
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

        
    }
}