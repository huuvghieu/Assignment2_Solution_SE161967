using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string FirstName { get; set; } = default!;
        public string MiddleName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddres { get; set; } = default!;
        public string Source { get; set; } = default!;
        public string Password { get; set; } = default!;
        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; } = default!;
        public DateTime HireDate { get; set; } = default!;
    }
}
