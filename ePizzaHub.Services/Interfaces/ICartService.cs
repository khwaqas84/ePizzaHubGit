using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface ICartService :IService<Cart>
    {
        int GetCartCount(Guid CartId);
        Cart AddItem(int UserId, Guid CartId, int ItemId,decimal UnitPrice, int Quantity);
        CartModel GetCartDetails(Guid CartId);
        int DeleteItems(Guid CartId, int ItemId);
        int UpdateQuantity(Guid CartId, int ItemId, int quantity);
        int UpdateCart(Guid CartId, int UserId);
    }
}
