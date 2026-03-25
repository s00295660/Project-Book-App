using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Project_Book_App
{
    // ─── Modèle JSON ───────────────────────────────────────────────────────────

    public class UserProfile
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = "";

        [JsonPropertyName("wishList")]
        public List<BookEntry> WishList { get; set; } = new();

        [JsonPropertyName("library")]
        public List<BookEntry> Library { get; set; } = new();

        [JsonPropertyName("booksReadCount")]
        public int BooksReadCount { get; set; } = 0;
    }

    public class BookEntry
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("author")]
        public string Author { get; set; } = "";

        [JsonPropertyName("isbn")]
        public string Isbn { get; set; } = "";

        [JsonPropertyName("coverUrl")]
        public string CoverUrl { get; set; } = "";

        // Utilisé seulement pour la bibliothèque (pas la wish-list)
        [JsonPropertyName("isRead")]
        public bool IsRead { get; set; } = false;

        // Affichage dans les ListBox
        public override string ToString() => $"{Title} — {Author}";
    }

    // ─── Service de sauvegarde / chargement ────────────────────────────────────

    public static class ProfileManager
    {
        // Fichier stocké à côté de l'exécutable
        private static readonly string FilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "user_profile.json");

        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true
        };

        /// <summary>Charge le profil depuis le JSON. Retourne un profil vide si le fichier n'existe pas.</summary>
        public static UserProfile Load()
        {
            if (!File.Exists(FilePath))
                return new UserProfile();

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<UserProfile>(json, Options) ?? new UserProfile();
            }
            catch
            {
                return new UserProfile();   // fichier corrompu → profil vide
            }
        }

        /// <summary>Sauvegarde le profil dans le JSON.</summary>
        public static void Save(UserProfile profile)
        {
            string json = JsonSerializer.Serialize(profile, Options);
            File.WriteAllText(FilePath, json);
        }
    }
}
