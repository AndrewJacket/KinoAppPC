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

namespace KinoApp
{
    /// <summary>
    /// Логика взаимодействия для RegSuccessfulPage.xaml
    /// </summary>
    public partial class RegSuccessfulPage : Page
    {
        public RegSuccessfulPage()
        {
            InitializeComponent();
        }

        private void RegOkBtn_Click(object sender, RoutedEventArgs e)
        {
            new MenuWindow().Show();
            Helper.CloseWindow(Window.GetWindow(this));
        }
    }
}
