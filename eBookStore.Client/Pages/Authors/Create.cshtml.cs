using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eBookStore.Repository.Context;
using eBookStore.Repository.Entity;
using System.Net.Http.Headers;
using eBookStore.Repository.Model.ResponseModel;

namespace eBookStore.Client.Pages.Authors
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null;
        private string AuthorApiUrl = "";
        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "https://localhost:7209/odata/Authors";
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuthorResponseModel Author { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.PostAsJsonAsync(AuthorApiUrl, Author);
                if (response.IsSuccessStatusCode)
                {
                    var rs = await response.Content.ReadFromJsonAsync<AuthorResponseModel>();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
