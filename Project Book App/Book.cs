using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Book_App
{
    public class Book : IComparable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Isbn { get; set; }
        public string CoverUrl { get; set; }

        public Book(string title, string author, string genre, string description,
                    string isbn = "", string coverUrl = "")
        {
            Title = title;
            Author = author;
            Genre = genre;
            Description = description;
            Isbn = isbn;
            CoverUrl = coverUrl;
        }

        public Book() { }

        public override string ToString() => Title;

        public int CompareTo(object obj)
        {
            Book otherBook = obj as Book;
            return this.Title.CompareTo(otherBook.Title);
        }
    }
}