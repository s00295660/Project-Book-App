using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Book_App
{
    public class UserProfile
    {
        public string Username { get; set; } = "";
        public List<Book> WishList { get; set; } = new List<Book>();
        public List<Book> Library { get; set; } = new List<Book>();
        public List<BookReview> Reviews { get; set; } = new List<BookReview>();
    }
}
