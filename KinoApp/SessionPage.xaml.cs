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
        public static bool dateCheck { get; set; }
        public SessionPage()
        {
            InitializeComponent();
            //dateCheck = false;
           
                Date = DateTime.Now.ToString();
                Date = Date.Substring(0, Date.LastIndexOf(' ') + 1);
                SessionFrame.Navigate(new DateSessionPage());
        }


        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (DateSession.DisplayDate == null)
            {
                Date = DateTime.Now.ToString();
                Date = Date.Substring(0, Date.LastIndexOf(' ') + 1);
                SessionFrame.Navigate(new DateSessionPage());
            }
            else
            if (DateSession.DisplayDate != null)
            {
                Date = DateSession.Text;
                Date = Date.Substring(0, Date.LastIndexOf(' ') + 1);
            }
        }
    }
}
