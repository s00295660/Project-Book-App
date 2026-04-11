using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Project_Book_App
{
    public static class BookRepository
    {
        private static List<Book> _cache;

        public static List<Book> LoadAll()
        {
            if (_cache != null) return _cache;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;

            string[] searchPaths =
            {
                Path.Combine(baseDir, "books.json"),                        
                Path.Combine(baseDir, @"..\..\books.json"),                
                Path.Combine(baseDir, @"..\..\..\books.json"),              
                Path.Combine(Directory.GetCurrentDirectory(), "books.json"),
            };

            foreach (var path in searchPaths)
            {
                string full = Path.GetFullPath(path);
                if (!File.Exists(full)) continue;

                var json = File.ReadAllText(full, System.Text.Encoding.UTF8);
                _cache = JsonConvert.DeserializeObject<List<Book>>(json)
                         ?? new List<Book>();
                return _cache;
            }

            string tried = string.Join("\n", searchPaths.Select(p => Path.GetFullPath(p)));
            System.Windows.MessageBox.Show(
                "books.json introuvable.\n\nChemins testés :\n" + tried,
                "BookRepository",
                System.Windows.MessageBoxButton.OK,
                System.Windows.MessageBoxImage.Warning);

            _cache = new List<Book>();
            return _cache;
        }

        public static List<string> GetGenres()
            => LoadAll().Select(b => b.Genre).Distinct().OrderBy(g => g).ToList();

        public static List<string> GetAuthors()
            => LoadAll().Select(b => b.Author).Distinct().OrderBy(a => a).ToList();

        public static List<Book> FilterBy(string genre = null, string author = null, string search = null)
        {
            var query = LoadAll().AsEnumerable();

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(search))
                query = query.Where(b =>
                    b.Title.Contains(search) ||
                    b.Author.Contains(search));

            return query.ToList();
        }
    }
}