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
using eBookStore.Client.Helpers;
using System.Text.Json;
using eBookStore.Repository.Model.RequestModel;
using Newtonsoft.Json.Linq;

namespace eBookStore.Client.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        private string PublisherApiUrl = "";
        private string AuthorApiUrl = "";

        [BindProperty]
        public BookResponseModel Book { get; set; } = default!;
        public List<SelectListItem> Publishers { get; set; } = default!;
        public List<SelectListItem> Authors { get; set; } = default!;
        [BindProperty]
        public int AuthorId { get; set; } = default!;
        [BindProperty]
        public string AuthorOrder { get; set; } = default!;
        [BindProperty]
        public double RoyaltyPercentage { get; set; } = default!;

        public CreateModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:7209/odata/Books";
            PublisherApiUrl = "https://localhost:7209/odata/Publishers";
            AuthorApiUrl = "https://localhost:7209/odata/Authors";
        }

        public async Task<IActionResult> OnGetAsync()
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

            #region Authors
            HttpResponseMessage responseAuthors = await client.GetAsync(AuthorApiUrl);
            string strDataAuthors = await responseAuthors.Content.ReadAsStringAsync();

            dynamic tempAuthor = JObject.Parse(strDataAuthors);
            List<AuthorResponseModel> itemAuthors = ((JArray)tempAuthor.value).Select(x => new AuthorResponseModel
            {
                AuthorId = (int)x["AuthorId"],
                LastName = (string)x["LastName"],
                FirstName = (string)x["FirstName"],
                Phone = (string)x["Phone"],
                Address = (string)x["Address"],
                City = (string)x["City"],
                State = (string)x["State"],
                Zip = (string)x["Zip"],
                EmailAddress = (string)x["EmailAddress"],
            }).ToList();

            Authors = itemAuthors.Select(x => new SelectListItem
            {
                Text = x.LastName,
                Value = x.AuthorId.ToString(),
            }).ToList();

            SessionHelper.SetObjectAsJson(HttpContext.Session, "AuthorList", this.Authors);
            #endregion
            return Page();
        }




        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                Book.BookAuthor = new BookAuthorResponseModel();
                Book.BookAuthor.AuthorId = AuthorId;
                Book.BookAuthor.AuthorOrder = AuthorOrder;
                Book.BookAuthor.RoyaltyPercentage = RoyaltyPercentage;
                HttpResponseMessage response = await client.PostAsJsonAsync(BookApiUrl, Book);
                if (response.IsSuccessStatusCode)
                {
                    var rs = await response.Content.ReadFromJsonAsync<BookResponseModel>();
                    return RedirectToPage("./Index");
                }
            }
            return Page();
        }
    }
}
