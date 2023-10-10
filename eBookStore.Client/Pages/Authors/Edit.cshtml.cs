using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using System.Net.Http.Headers;
using eBookStore.Repository.Model.ResponseModel;
using System.Text.Json;
using eBookStore.Client.Helpers;

namespace eBookStore.Client.Pages.Authors
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null;
        private string AuthorApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "https://localhost:7209/odata/Authors";
        }

        [BindProperty]
        public AuthorResponseModel Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            HttpResponseMessage response = await client.GetAsync($"{AuthorApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Author = JsonSerializer.Deserialize<AuthorResponseModel>(strData, options);

            if (Author == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.PutAsJsonAsync($"{AuthorApiUrl}/{Author.AuthorId}", Author);
                if (response.IsSuccessStatusCode)
                {
                    var rs = await response.Content.ReadFromJsonAsync<PublisherResponseModel>();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
