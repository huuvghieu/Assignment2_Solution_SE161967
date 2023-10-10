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
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace eBookStore.Client.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        public BookResponseModel Book { get; set; } = default!;
        public DetailsModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:7209/odata/Books";
        }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            HttpResponseMessage response = await client.GetAsync($"{BookApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Book = JsonSerializer.Deserialize<BookResponseModel>(strData, options);

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
