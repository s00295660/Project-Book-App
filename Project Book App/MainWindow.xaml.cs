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

namespace Project_Book_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



       

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Window1 mainWindow = new Window1();
            Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {
            Window2 mainWindow = new Window2();
            Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            Window3 mainWindow = new Window3();
            Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

    }
}
