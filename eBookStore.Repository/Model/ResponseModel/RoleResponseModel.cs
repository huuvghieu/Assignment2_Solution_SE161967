using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.ResponseModel
{
    public class RoleResponseModel
    {
        [Key]
        public int RoleId { get; set; }
        public string Description { get; set; } = default!;
    }
}
