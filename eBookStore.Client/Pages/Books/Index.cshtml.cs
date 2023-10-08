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
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string BookApiUrl = "";
        public IList<BookResponseModel> Book { get; set; } = default!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            BookApiUrl = "https://localhost:7209/odata/Books";
        }


        public async Task OnGetAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(BookApiUrl);
                string strData = await response.Content.ReadAsStringAsync();

                dynamic temp = JObject.Parse(strData);
                var lst = temp.value;
                List<BookResponseModel> items = ((JArray)temp.value).Select(x => new BookResponseModel
                {
                    BookId = (int)x["BookId"],
                    Title = (string)x["Title"],
                    Type = (string)x["Type"],
                    PublishserId = (int)x["PublishserId"],
                    Price = (decimal)x["Price"],
                    Advance = (decimal)x["Advance"],
                    Royalty = (decimal)x["Royalty"],
                    Note = (string)x["Note"],
                    YTDSale = (int)x["YTDSale"],
                    PublishedDate = (DateTime)x["PublishedDate"],
                }).ToList();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                Book = items;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
