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
using eBookStore.Client.Helpers;
using System.Text.Json;

namespace eBookStore.Client.Pages.Publishers
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null;
        private string PublisherApiUrl = "";

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = "https://localhost:7209/odata/Publishers";
        }

        [BindProperty]
        public PublisherResponseModel Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            HttpResponseMessage response = await client.GetAsync($"{PublisherApiUrl}/{id}");
            string strData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            Publisher = JsonSerializer.Deserialize<PublisherResponseModel>(strData, options);

            if (Publisher == null)
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
                HttpResponseMessage response = await client.PutAsJsonAsync($"{PublisherApiUrl}/{Publisher.PublisherId}", Publisher);
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
