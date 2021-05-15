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
    /// Логика взаимодействия для SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {
        public static string Date { get; set; }
        public static string DateToday { get; set; }
        public SessionPage()
        {
            InitializeComponent();
            //dateCheck = false;

            Date = DateTime.Now.ToString();
            Date = Date.Substring(0, Date.LastIndexOf(' ') + 1);
            DateToday = DateTime.Now.ToString();
            DateToday = DateToday.Substring(0, DateToday.LastIndexOf(' ') + 1);
            SessionFrame.Navigate(new DateSessionPage());
        }


        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {

            if (DateSession.SelectedDate.HasValue)
            {
                Date = DateSession.SelectedDate.Value.ToString();
                Date = Date.Substring(0, Date.LastIndexOf(' ') + 1);
                SessionFrame.Navigate(new DateSessionPage());
            }
            else
            {

            }
        }
    }
}
