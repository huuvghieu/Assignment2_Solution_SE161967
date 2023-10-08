using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Entity
{
    public class BookAuthor
    {

        public int AuthorId { get; set; }
        public Author Author { get; set; } = default!;
        public int BookId { get; set; }
        public Book Book { get; set; } = default!;

        public string AuthorOrder { get; set; } = default!;
        public double RoyaltyPercentage { get; set; } = default!;
    }
}
