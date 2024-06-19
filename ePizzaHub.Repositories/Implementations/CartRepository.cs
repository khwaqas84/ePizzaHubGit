using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Implementations
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
       
        public CartRepository(AppDbContext context):base(context) 
        {
           
        }
        public int DeleteItems(Guid CartId, int ItemId)
        {
            var item=_context.CartItems.Where(c=>c.CartId==CartId&&c.ItemId==ItemId).FirstOrDefault();
            if (item != null)
            {
                _context.CartItems.Remove(item);
                return _context.SaveChanges();
            }
            return 0;
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model = (from cart in _context.Carts
                         where cart.Id == CartId && cart.IsActive== true
                         select new CartModel
                         {
                             Id = cart.Id,
                             UserId = cart.UserId,
                             CreatedDate = cart.CreatedDate,
                             Items = (from item in _context.Items
                                      join cartItem in _context.CartItems on item.Id equals cartItem.ItemId
                                      where cartItem.CartId == CartId
                                      select new ItemModel
                                      {
                                          Id = cartItem.Id,
                                          Name= item.Name,
                                          ItemId=item.Id,
                                          UnitPrice=item.UnitPrice,
                                          Quantity=cartItem.Quantity,
                                          Total=item.UnitPrice*cartItem.Quantity,
                                          ImageUrl=item.ImageUrl

                                      }).ToList()

                         }).FirstOrDefault();
            return model;
        }

        public Cart GetCartId(Guid CartId)
        {
            return _context.Carts.Include(c=>c.CartItems).Where(c=>c.Id==CartId ).FirstOrDefault();
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            var cart=_context.Carts.Where(c=>c.Id==CartId).FirstOrDefault();
            if (cart != null)
            {
                cart.UserId = UserId;
                return _context.SaveChanges();
            }
            return 0;
        }

        public int UpdateQuantity(Guid CartId, int ItemId, int quantity)
        {
            var cart = _context.Carts.Where(c => c.Id == CartId).FirstOrDefault();
            if(cart != null)
            {
                var item=_context.CartItems.Where(c=>c.CartId==CartId&&c.ItemId== ItemId).FirstOrDefault();
                if(item != null)
                {
                    item.Quantity += quantity;
                    return _context.SaveChanges();
                }
            }
            return 0;
        }
    }
}
