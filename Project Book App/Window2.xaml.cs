using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Book_App
{
    public partial class Window2 : Window
    {
        private Book _selectedBook = null;

        public Window2()
        {
            InitializeComponent();
            RefreshLibrary();
        }

        public void RefreshLibrary(List<Book> library = null)
        {
            if (library == null)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Window3 window3)
                    {
                        library = window3._profile.Library;
                        break;
                    }
                }
            }

            LibraryList.ItemsSource = null;
            LibraryList.ItemsSource = library;
        }

        private void LibraryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LibraryList.SelectedItem is Book book)
            {
                _selectedBook = book;

                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Window3 window3)
                    {
                        var review = window3._profile.Reviews
                            .FirstOrDefault(r => r.Isbn == book.Isbn);

                        if (review != null) 
                        {
                            RatingBar.Value = review.Grade;
                            CommentBox.Text = review.Comment;
                        }
                        else
                        {
                            RatingBar.Value = 0;
                            CommentBox.Text = "";
                        }
                        break;
                    }
                }
            }
        }

        private void CommentBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SaveReview();
        }

        private void SaveReview()
        {
            if (_selectedBook == null)
            {
                MessageBox.Show("Sélectionne un livre dans la liste.",
                    "Aucun livre sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (Window w in Application.Current.Windows)
            {
                if (w is Window3 window3)
                {
                    var review = window3._profile.Reviews
                        .FirstOrDefault(r => r.Isbn == _selectedBook.Isbn);

                    if (review != null)
                    {
                        review.Grade = (int)RatingBar.Value;
                        review.Comment = CommentBox.Text.Trim();
                    }
                    else
                    {
                        window3._profile.Reviews.Add(new BookReview
                        {
                            Isbn = _selectedBook.Isbn,
                            Grade = (int)RatingBar.Value,
                            Comment = CommentBox.Text.Trim()
                        });
                    }

                    ProfileManager.Save(window3._profile);
                    break;
                }
            }

            MessageBox.Show("Avis enregistré pour « " + _selectedBook.Title + " » !",
                "Sauvegardé", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}