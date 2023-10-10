using eBookStore.Repository.Model.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace eBookStore.Client.Pages.Home
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
			HttpResponseMessage response = await client.GetAsync(BookApiUrl);
			string strData = await response.Content.ReadAsStringAsync();

			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};

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
			Book = items;
		}
    }
}
