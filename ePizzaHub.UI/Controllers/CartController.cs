using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class CartController : BaseController
    {
        ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        Guid CartId
        {
            get
            {
                if (HttpContext.Request.Cookies["CId"] == null)
                {
                    Guid cartid = Guid.NewGuid();
                    HttpContext.Response.Cookies.Append("CId", cartid.ToString());
                    return cartid;
                }
                else
                {
                    return Guid.Parse(HttpContext.Request.Cookies["CId"]);
                }
            }
        }
        public IActionResult Index()
        {
            CartModel cartModel = _cartService.GetCartDetails(CartId);
            return View(cartModel);
        }
        [Route("Cart/AddToCart/{ItemId}/{unitPrice}/{Quantity}")]
        public IActionResult AddToCart(int ItemId, decimal unitPrice, int Quantity)
        {
            int UserId = CurrentUser != null ? CurrentUser.Id : 0;
            Cart cart = _cartService.AddItem(UserId, CartId, ItemId, unitPrice, Quantity);
            if (cart != null)
            {
                return Json(new { status = "success", count = cart.CartItems.Count() });
            }
            return Json(new { status = "failed", count = 0 });
        }
        [Route("Cart/UpdateQuantity/{ItemId}/{Quantity}")]
        public IActionResult UpdateQuantity(int ItemId, int Quantity)
        {
            int result=_cartService.UpdateQuantity(CartId, ItemId, Quantity);
            
            return Json(result);
            
        }
        [Route("Cart/DeleteItem/{ItemId}")]
        public IActionResult DeleteItem(int ItemId)
        {
            int result = _cartService.DeleteItems(CartId, ItemId);

            return Json(result);

        }

        public IActionResult GetCartCount()
        {
            if (CartId != null)
            {
                int result = _cartService.GetCartCount(CartId);

                return Json(result);
            }
            return Json(0);

        }
        [HttpPost]
        public IActionResult Checkout(AddressModel model)
        {
            if (ModelState.IsValid)
            {
                CartModel cartModel = _cartService.GetCartDetails(CartId);
                if (CurrentUser != null && cartModel != null)
                {
                    cartModel.UserId = CurrentUser.Id;
                    _cartService.UpdateCart(cartModel.Id, CurrentUser.Id);
                }
                TempData.Set("Address", model);
                TempData.Set("Cart", cartModel);
                return RedirectToAction("Index", "Payment");
            }
            return View();
        }

        public IActionResult Checkout()
        {
            
            return View();
        }

    }

    
}
