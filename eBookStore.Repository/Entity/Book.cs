using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Entity
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; } 
        public string Title { get; set; } = default!;

        public string Type { get; set; } = default!;
        [ForeignKey("Publisher")]
        public int PublishserId { get; set; }
        public Publisher Publisher { get; set; } = default!;

        public decimal Price { get; set; } = default!;
        public decimal Advance { get; set; } = default!;
        public decimal Royalty { get; set; } = default!;
        public int YTDSale { get; set; } = default!;
        public string Note { get; set; } = default!;
        public DateTime PublishedDate { get; set; } = default!;

        public BookAuthor BookAuthor { get; set; } = default!;

    }
}
