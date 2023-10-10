using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using eBookStore.Repository.Model.ResponseModel;
using System.Net.Http.Headers;
using eBookStore.Client.Helpers;

namespace eBookStore.Client.Pages.Publishers
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null;
        private string PublisherApiUrl = "";

        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = "https://localhost:7209/odata/Publishers";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PublisherResponseModel Publisher { get; set; }
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.PostAsJsonAsync(PublisherApiUrl, Publisher);
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
