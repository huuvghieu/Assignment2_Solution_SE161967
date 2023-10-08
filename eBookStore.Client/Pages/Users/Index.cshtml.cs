using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.ResponseModel;
using System.Net.Http.Headers;

namespace eBookStore.Client.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = "";
        public IList<UserResponseModel> User { get; set; } = default!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:7209/odata/Users";
        }

        public async Task OnGetAsync()
        {

        }
    }
}
