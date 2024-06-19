using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class ItemService :  Service<Item>, IItemService
    {
        public ItemService(IRepository<Item> _repo):base(_repo)
        {
            
        }
        public IEnumerable<ItemModel> GetItems()
        {
            return _repository.GetAll().Select(c => new ItemModel
            {
                Name = c.Name,
                UnitPrice = c.UnitPrice,
                ImageUrl = c.ImageUrl,
                Id = c.Id

            });
        }
    }
}
