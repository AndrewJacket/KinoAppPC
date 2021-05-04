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
///<summary>
/// Логика взаимодействия для ExitWindow.xaml
///</summary>
public partial class ExitPage : Page
    {
        public ExitPage()
        {
            InitializeComponent();
        }


        private void RelogBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }

        private void ExitAppBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            /*MenuWindow.LogoBtn.IsEnabled = true;
            MenuWindow.SessionBtn.IsEnabled = true;
            MenuWindow.TodayBtn.IsEnabled = true;
            MenuWindow.SoonBtn.IsEnabled = true;
            MenuWindow.ContactsBtn.IsEnabled = true;
            MenuWindow.AccountBtn.IsEnabled = true;
            MenuWindow.ExitBtn.IsEnabled = true;
            MenuWindow.MainFrame.IsEnabled = true;*/

        }

    }
}