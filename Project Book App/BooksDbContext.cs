using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Project_Book_App
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext() : base("MyBookData") { }
       
        public DbSet<Book> Books { get; set; }
    }
}
