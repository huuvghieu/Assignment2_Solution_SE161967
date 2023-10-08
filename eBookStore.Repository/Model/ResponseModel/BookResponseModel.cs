using eBookStore.Repository.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.ResponseModel
{
    public class BookResponseModel
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; } = default!;
        public string Type { get; set; } = default!;
        public int PublishserId { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        public decimal Advance { get; set; } = default!;
        public decimal Royalty { get; set; } = default!;
        public int YTDSale { get; set; } = default!;
        public string Note { get; set; } = default!;
        public DateTime PublishedDate { get; set; } = default!;
        public virtual ICollection<BookAuthorResponseModel> BookAuthors { get; set; } = default!;
    }

    public class BookAuthorResponseModel
    {
        public int AuthorId { get; set; }
        public int BookId { get; set; }
        public string AuthorOrder { get; set; } = default!;
        public double RoyaltyPercentage { get; set; } = default!;
    }


}
