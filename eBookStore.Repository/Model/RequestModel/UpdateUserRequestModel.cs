using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.RequestModel
{
    public class UpdateUserRequestModel
    {
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int PublisherId { get; set; }
        public DateTime HireDate { get; set; } = default!;
    }
}
