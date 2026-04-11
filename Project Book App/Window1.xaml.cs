using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Project_Book_App
{
    public partial class Window1 : Window
    {
        public List<Book> allBook = new List<Book>();
        private List<Book> _displayedBooks = new List<Book>();
        private Book _selected = null;
        private bool _isLoading = true;

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoading = true;

            allBook = BookRepository.LoadAll();

            Genres.ItemsSource = BookRepository.GetGenres();
            Authors.ItemsSource = BookRepository.GetAuthors();

            _isLoading = false;

            RefreshListBox(allBook);
        }


        private void RefreshListBox(List<Book> books)
        {
            _displayedBooks = books;
            BookList.ItemsSource = books.Select(b => b.Title).ToList();
        }

        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookList.SelectedIndex < 0 || BookList.SelectedIndex >= _displayedBooks.Count)
                return;

            _selected = _displayedBooks[BookList.SelectedIndex];

            TitleBlock.Text = _selected.Title;
            DescriptionBlock.Text = _selected.Description;

            LoadCoverImage(_selected.CoverUrl);
        }


        private void LoadCoverImage(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    ImageBookCover.Source = null;
                    return;
                }

                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(url, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                bitmap.EndInit();

                ImageBookCover.Source = bitmap;
            }
            catch
            {
                ImageBookCover.Source = null;
            }
        }

        private void Genres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoading) ApplyFilters();
        }

        private void Authors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isLoading) ApplyFilters();
        }

        private void ApplyFilters()
        {
            string genre = Genres.SelectedItem as string;
            string author = Authors.SelectedItem as string;

            var filtered = BookRepository.FilterBy(genre: genre, author: author);
            RefreshListBox(filtered);

            TitleBlock.Text = string.Empty;
            DescriptionBlock.Text = string.Empty;
            ImageBookCover.Source = null;
            _selected = null;
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

        private void AddToWishList_Click(object sender, RoutedEventArgs e)
        {
            if (_selected == null) return;

            foreach (Window w in Application.Current.Windows)
            {
                if (w is Window3 window3)
                {
                    window3._profile.WishList.Add(_selected);
                    window3.WishList.ItemsSource = null;
                    window3.WishList.ItemsSource = window3._profile.WishList;
                    ProfileManager.Save(window3._profile);

                    MessageBox.Show(
                        "« " + _selected.Title + " » ajouté à la Wish List !",
                        "Wish List",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            }
        }
    }
}