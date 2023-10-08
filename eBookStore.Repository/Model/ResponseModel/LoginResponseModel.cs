using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStore.Repository.Model.ResponseModel
{
    public class LoginResponseModel
    {
        [Key]
        public string Token { get; set; }
    }
}
