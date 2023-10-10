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
using Newtonsoft.Json.Linq;

namespace eBookStore.Client.Pages.Books
{
    public class EditModel : PageModel
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        private string PublisherApiUrl = "";

        public List<SelectListItem> Publishers { get; set; } = default!;

        public EditModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:7209/odata/Books";
            PublisherApiUrl = "https://localhost:7209/odata/Publishers";


        }

        [BindProperty]
        public BookResponseModel Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            #region Publishers
            HttpResponseMessage responsePub = await client.GetAsync(PublisherApiUrl);
            string strDataPub = await responsePub.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strDataPub);
            List<PublisherResponseModel> items = ((JArray)temp.value).Select(x => new PublisherResponseModel
            {
                PublisherId = (int)x["PublisherId"],
                PublisherName = (string)x["PublisherName"],
                City = (string)x["City"],
                State = (string)x["State"],
                Country = (string)x["Country"],
            }).ToList();
            Publishers = items.Select(x => new SelectListItem
            {
                Text = x.PublisherName,
                Value = x.PublisherId.ToString(),
            }).ToList();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "PublisherList", this.Publishers);
            #endregion
            var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                HttpResponseMessage response = await client.PutAsJsonAsync($"{BookApiUrl}/{Book.BookId}", Book);
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
