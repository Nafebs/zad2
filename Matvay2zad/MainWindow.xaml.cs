using System;
using System.Collections.Generic;
using System.IO;
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
using Lib;

namespace Matvay2zad
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Lib.VinCalculator.init(getPath());
        }

        public string getPath()
        {
            string[] path = System.IO.Directory.GetCurrentDirectory().Split('\\');
            string newPath = "";

            for (int i = 0; i < path.Length; i++)
            {
                if (i > 0)
                    newPath += "\\";

                if (i > path.Length - 3)
                    break;

                newPath += path[i];
            }

            newPath += "data";
            return newPath;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!Lib.VinCalculator.checkVin(t1.Text))
            {
                MessageBox.Show("Число не обработано");
                return;
            }

            t2.Text = Lib.VinCalculator.getVINCountry(t1.Text);
            
        }
    }
}
