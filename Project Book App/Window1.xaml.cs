using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string[] genres = { "fantasy", "science-fiction", "thriller", "fantastic", "novel", "dark romance", "romance", "historical", "horror", "police", "young-adult" };
            Genres.ItemsSource = genres;

            string[] authors = { };
            Authors.ItemsSource = authors;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Visibility = Visibility.Hidden;
            mainWindow.Show();
        }


        private void AddToWishList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Genres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedGenre = Genres.SelectedItem as string;
        }
    }
}
