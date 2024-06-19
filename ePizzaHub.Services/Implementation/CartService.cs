using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class CartService : Service<Cart>, ICartService
    {
        ICartRepository _cartRepo;
        IRepository<CartItem> _cartItemRepo;
        IConfiguration _configuration;
        public CartService(ICartRepository cartRepo, IRepository<CartItem> cartItemRepo, IConfiguration configuration) :base(cartRepo)
        {
            _cartRepo= cartRepo;
            _cartItemRepo= cartItemRepo;
            _configuration= configuration;
        }
        public Cart AddItem(int UserId, Guid CartId, int ItemId, decimal UnitPrice, int Quantity)
        {
            try
            {
                Cart cart = _cartRepo.GetCartId(CartId);
                if (cart == null) 
                {
                    cart = new Cart
                    {
                        Id=CartId,
                        UserId=UserId,
                        CreatedDate=DateTime.Now,
                        IsActive=true
                    };
                    CartItem item = new CartItem
                    {
                        ItemId = ItemId,
                        UnitPrice = UnitPrice,
                        Quantity = Quantity,
                        CartId=CartId
                    };
                    cart.CartItems.Add(item);
                   _cartRepo.Add(cart);
                    _cartItemRepo.SaveChanges();
                }
                else
                {
                    CartItem cartItem=cart.CartItems.Where(c=>c.ItemId==ItemId).FirstOrDefault();
                    if (cartItem != null)
                    {
                        cartItem.Quantity += Quantity;
                        _cartItemRepo.Update(cartItem);
                        _cartItemRepo.SaveChanges();
                    }
                    else
                    {
                        cartItem = new CartItem
                        {
                            ItemId = ItemId,
                            UnitPrice = UnitPrice,
                            Quantity = Quantity,
                            CartId = CartId
                        };
                        cart.CartItems.Add(cartItem);
                        _cartItemRepo.Update(cartItem);
                        _cartItemRepo.SaveChanges();

                    }
                }
                return cart;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int DeleteItems(Guid CartId, int ItemId)
        {
            return _cartRepo.DeleteItems(CartId, ItemId);
        }

        public int GetCartCount(Guid CartId)
        {
            var cart= _cartRepo.GetCartId(CartId);
            if(cart != null)
            {
               return cart.CartItems.Count();
            }
            return 0;
        }

        public CartModel GetCartDetails(Guid CartId)
        {
            var model= _cartRepo.GetCartDetails(CartId);
            if (model != null)
            {
                decimal subTotal = 0;
                foreach(var item in model.Items)
                {
                    item.Total = item.UnitPrice*item.Quantity;
                    subTotal += item.Total;
                }
                model.Total = subTotal;
                model.Tax = (subTotal * Convert.ToDecimal(_configuration["Tax:GST"]))/100;
                model.GrandTotal= model.Total+model.Tax;
            }
            return model;
        }

        public int UpdateCart(Guid CartId, int UserId)
        {
            return _cartRepo.UpdateCart(CartId, UserId);
        }

        public int UpdateQuantity(Guid CartId, int ItemId, int quantity)
        {
            return _cartRepo.UpdateQuantity(CartId, ItemId, quantity);
        }
    }
}
