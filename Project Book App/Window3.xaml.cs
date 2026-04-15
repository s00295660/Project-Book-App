using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Book_App
{
    public partial class Window3 : Window
    {
        public UserProfile _profile;

        public Window3()
        {
            InitializeComponent();
            _profile = new UserProfile();
        }

        private void UsernameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LoadUser();
        }

        private void LoadUser()
        {
            string name = UsernameBox.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            var found = ProfileManager.Find(name);

            if (found != null)
            {
                _profile = found;
                MessageBox.Show("Bienvenue " + _profile.Username + " !", "Profil chargé",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                _profile = new UserProfile { Username = name };
                ProfileManager.Save(_profile);
                MessageBox.Show("Nouveau profil créé pour " + name + " !", "Nouveau profil",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            WishList.ItemsSource = null;
            WishList.ItemsSource = _profile.WishList;
            ReadBooks.Text = _profile.Library.Count.ToString();

            foreach (Window w in Application.Current.Windows)
            {
                if (w is Window2 window2)
                {
                    window2.RefreshLibrary(_profile.Library);
                    break;
                }
            }
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            if (WishList.SelectedItem is Book selected)
            {
                if (!_profile.Library.Contains(selected))
                {
                    _profile.Library.Add(selected);
                }

                _profile.WishList.Remove(selected);
                WishList.ItemsSource = null;
                WishList.ItemsSource = _profile.WishList;

                ProfileManager.Save(_profile);

                foreach (Window w in Application.Current.Windows)
                {
                    if (w is Window2 window2)
                    {
                        window2.RefreshLibrary(_profile.Library);
                        break;
                    }
                }

                ReadBooks.Text = _profile.Library.Count.ToString();

                MessageBox.Show("« " + selected.Title + " » ajouté à la Library !",
                    "Library", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                MessageBox.Show("Sélectionne un livre dans la Wish List.",
                    "Aucun livre sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ProfileManager.Save(_profile);

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

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            ProfileManager.Save(_profile);
            base.OnClosing(e);
        }
    }
}