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
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace eBookStore.Client.Pages.Authors
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string AuthorApiUrl = "";
        public IList<AuthorResponseModel> Author { get; set; } = default!;
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            AuthorApiUrl = "https://localhost:7209/odata/Authors";
        }

        public async Task OnGetAsync()
        {
            HttpResponseMessage response = await client.GetAsync(AuthorApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<AuthorResponseModel> items = ((JArray)temp.value).Select(x => new AuthorResponseModel
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

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Author = items;
        }
    }
}
