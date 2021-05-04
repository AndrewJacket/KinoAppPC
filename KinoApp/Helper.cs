using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace KinoApp
{
    class Helper
    {
        public static void CloseWindow(Window x)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            // int count = Application.Current.Windows;
            foreach (Window w in Application.Current.Windows)
            {
                //Form f = Application.OpenForms[i];
                if (w.GetType().Assembly == currentAssembly && w == x)
                {
                    w.Close();
                }
            }
        }
    }
}