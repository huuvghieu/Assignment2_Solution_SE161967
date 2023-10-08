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
    public class UserResponseModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddres { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int RoleId { get; set; }
        public int PublisherId { get; set; }
        public DateTime HireDate { get; set; } = default!;
    }
}
