using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.RequestModel
{
    public class CreateAuthorRequestModel
    {
        public string LastName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Zip { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
    }
}
