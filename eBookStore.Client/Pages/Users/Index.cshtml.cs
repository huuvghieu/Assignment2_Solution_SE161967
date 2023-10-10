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
using eBookStore.Client.Helpers;

namespace eBookStore.Client.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string UserApiUrl = "";
        public IList<UserResponseModel> User { get; set; } = default!;

        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserApiUrl = "https://localhost:7209/odata/Users";
        }

        public async Task OnGetAsync()
        {
            var jwtToken = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "JWTToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            HttpResponseMessage response = await client.GetAsync(UserApiUrl);
            string strData = await response.Content.ReadAsStringAsync();

            dynamic temp = JObject.Parse(strData);
            var lst = temp.value;
            List<UserResponseModel> items = ((JArray)temp.value).Select(x => new UserResponseModel
            {
                UserId = (int)x["UserId"],
                PublisherId = (int)x["PublisherId"],
                FirstName = (string)x["FirstName"],
                MiddleName = (string)x["MiddleName"],
                LastName = (string)x["LastName"],
                EmailAddres = (string)x["EmailAddres"],
                Source = (string)x["Source"],
                Password = (string)x["Password"],
                RoleId = (int)x["RoleId"],
                HireDate = (DateTime)x["HireDate"]
            }).ToList();
            User = items;
        }
    }
}
