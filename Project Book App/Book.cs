using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Book_App
{
    public class Book : IComparable
    {
        //public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        //public string ImageURL { get; set; }

        public Book(string title, string author, string genre, string description)
        {
            Title = title;
            Description = description;
            Author = author;
            Genre = genre;
        }

        public override string ToString()
        {
            return Title;
        }

        public int CompareTo(object obj)
        {
            Book otherBook = obj as Book;

            return this.Title.CompareTo(otherBook.Title);
        }
    }
}
