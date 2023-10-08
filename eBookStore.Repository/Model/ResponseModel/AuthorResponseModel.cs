using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBookStore.Repository.Entity;

namespace eBookStore.Repository.Model.ResponseModel
{
    public class AuthorResponseModel
    {
        [Key]
        public int AuthorId { get; set; }
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Zip { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public virtual ICollection<BookAuthorResponseModel> BookAuthors { get; set; } = default!;
    }
}
