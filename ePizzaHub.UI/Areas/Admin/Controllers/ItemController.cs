using ePizzaHub.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ePizzaHub.UI.Areas.Admin.Controllers
{
    public class ItemController : BaseController
    {
        HttpClient client;
        IConfiguration configuration;
        public ItemController(IConfiguration configuration)
        {
            client = new HttpClient();
            this.configuration = configuration;
            client.BaseAddress = new Uri(this.configuration["ApiAddress"]);
        }

        public IActionResult Index()
        {
            IEnumerable<Item> items = null;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", CurrentUser.Token);

            var response = client.GetAsync(client.BaseAddress + "/Item/GetItems").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                items = JsonSerializer.Deserialize<IEnumerable<Item>>(data);
            }
            return View(items);
        }
    }
}
