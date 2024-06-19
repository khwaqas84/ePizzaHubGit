using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System.CodeDom;
using System.Diagnostics;

namespace ePizzaHub.UI.Controllers
{
    public class HomeController : Controller
    {
       IItemService _itemService;
        IMemoryCache _memoryCache;
        //  IDistributedCache _cacheRedis;
        public HomeController(IItemService itemService)
        {
            _itemService= itemService;
        }

        public IActionResult Index()
        {
            try
            {
                int x = 0, y = 10;
                int z = y / x;


            }catch (Exception ex) 
            { 
                Log.Error(ex,ex.Message);
            }
            var data= _itemService.GetItems();
            //in-memory cache
            //var items = _memoryCache.GetOrCreate("Items", entry =>
            //{
            //    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12);
            //    return _itemService.GetItems();
            //});

            //distributed cache: https://github.com/tporadowski/redis
            //var items = _cacheRedis.GetRecordAsync<IEnumerable<ItemModel>>("Items").Result;
            //if(items == null)
            //{
            //    items = _itemService.GetItems();
            //    _cacheRedis.SetRecordAsync("Items", items);
            //}
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
