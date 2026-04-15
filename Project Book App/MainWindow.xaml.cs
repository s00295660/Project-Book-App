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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Window1 _searchWindow = null;
        private Window2 _libraryWindow = null;
        private Window3 _profileWindow = null;

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (_searchWindow == null)
                _searchWindow = new Window1();

            _searchWindow.Show();
            this.Hide();
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {
            if(this._libraryWindow == null)
                _libraryWindow = new Window2();

            _libraryWindow.Show();
            this.Hide();
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            if (this._profileWindow == null)
                _profileWindow = new Window3();

            _profileWindow.Show();
            this.Hide();

        }

    }
}
