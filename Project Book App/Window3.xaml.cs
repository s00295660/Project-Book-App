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

namespace Project_Book_App
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public List<Book> WishListt = new List<Book>();

        public Window3()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow)
                {
                    w.Show();
                    break;
                }
            }
            this.Hide();
        }

        private void WishList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            ReadBooks.Text = "0";
        }
    }
}
