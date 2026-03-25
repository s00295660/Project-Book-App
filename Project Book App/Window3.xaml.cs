using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Project_Book_App
{
    public partial class Window3 : Window
    {
        // Profil chargé en mémoire
        private UserProfile _profile = new();

        public Window3()
        {
            InitializeComponent();
            LoadProfile();
        }

        // ── Chargement ─────────────────────────────────────────────────────────

        private void LoadProfile()
        {
            _profile = ProfileManager.Load();

            // Remplir le nom d'utilisateur
            ListBox.TextInputEvent = _profile.Username;

            // Remplir la WishList
            WishList.ItemsSource = null;
            WishList.ItemsSource = _profile.WishList;

            // Mettre à jour le compteur de livres lus
            int readCount = _profile.Library.Count(b => b.IsRead);
            ReadBooks.Text = readCount.ToString();
        }

        // ── Sauvegarde ─────────────────────────────────────────────────────────

        private void SaveProfile()
        {
            _profile.Username = UsernameBox.Text.Trim();
            _profile.BooksReadCount = _profile.Library.Count(b => b.IsRead);
            ProfileManager.Save(_profile);
        }

        // ── Événements ─────────────────────────────────────────────────────────

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            SaveProfile();   // ← on sauvegarde avant de quitter

            foreach (Window w in Application.Current.Windows)
            {
                if (w is MainWindow main)
                {
                    // Transmettre la bibliothèque à jour à la MainWindow si besoin
                    main.Show();
                    break;
                }
            }
            this.Hide();
        }

        private void WishList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Logique de sélection à compléter si nécessaire
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            // Marquer le livre sélectionné comme lu
            if (WishList.SelectedItem is BookEntry selected)
            {
                // Déplacer de la WishList vers la Library avec isRead = true
                _profile.WishList.Remove(selected);
                selected.IsRead = true;
                _profile.Library.Add(selected);

                // Rafraîchir l'affichage
                WishList.ItemsSource = null;
                WishList.ItemsSource = _profile.WishList;

                ReadBooks.Text = _profile.Library.Count(b => b.IsRead).ToString();

                SaveProfile();
            }
            else
            {
                // Comportement original si rien n'est sélectionné
                ReadBooks.Text = "0";
            }
        }

        // Sauvegarder aussi si on ferme la fenêtre directement
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            SaveProfile();
            base.OnClosing(e);
        }
    }
}