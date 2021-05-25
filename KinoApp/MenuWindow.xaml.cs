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

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SessionPage());
            Manager.MainFrame = MainFrame;
        }

        private void LogoBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SessionPage());
        }

        private void SessionBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SessionPage());
        }

        private void ContactsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ContactsPage());
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AccountPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            ExitFrame.Navigate(new ExitPage());
            /*LogoBtn.IsEnabled = false;
            SessionBtn.IsEnabled = false;
            TodayBtn.IsEnabled = false;
            SoonBtn.IsEnabled = false;
            ContactsBtn.IsEnabled = false;
            AccountBtn.IsEnabled = false;
            ExitBtn.IsEnabled = false;
            MainFrame.IsEnabled = false;*/
        }
    }
}
