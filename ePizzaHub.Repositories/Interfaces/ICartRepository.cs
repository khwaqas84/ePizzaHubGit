using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface ICartRepository:IRepository<Cart>
    {
        Cart GetCartId(Guid id);
        CartModel GetCartDetails(Guid id);
        int DeleteItems(Guid CartId,int ItemId);
        int UpdateQuantity(Guid CartId,int ItemId,int quantity);
        int UpdateCart(Guid CartId,int UserId);
    }
}
