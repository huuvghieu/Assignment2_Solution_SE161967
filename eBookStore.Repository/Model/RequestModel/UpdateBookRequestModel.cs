using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.RequestModel
{
    public class UpdateBookRequestModel
    {
        public string Type { get; set; } = default!;
        public int PublishserId { get; set; }
        public decimal Price { get; set; } = default!;
        public decimal Advance { get; set; } = default!;
        public decimal Royalty { get; set; } = default!;
        public int YTDSale { get; set; } = default!;
        public string Note { get; set; } = default!;
        public DateTime PublishedDate { get; set; } = default!;
    }
}
