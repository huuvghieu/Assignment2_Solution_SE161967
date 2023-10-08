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
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Net.Http.Headers;

namespace eBookStore.Client.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string PublisherApiUrl = "";
        public IList<PublisherResponseModel> Publisher { get; set; } = default!;
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            PublisherApiUrl = "https://localhost:7209/odata/Publishers";
        }


        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(PublisherApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<PublisherResponseModel> items = ((JArray)temp.value).Select(x => new PublisherResponseModel
            {
                PublisherId = (int)x["PublisherId"],
                PublisherName = (string)x["PublisherName"],
                City = (string)x["City"],
                State = (string)x["State"],
                Country = (string)x["Country"],
            }).ToList();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Publisher = items;
        }
    }
}
