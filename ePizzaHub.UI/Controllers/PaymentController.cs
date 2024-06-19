using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Implementation;
using ePizzaHub.Services.Interfaces;
using ePizzaHub.UI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Controllers
{
    public class PaymentController : BaseController
    {
        IPaymentService _paymentService;
        IConfiguration _configuration;
        IOrderService _orderService;
        public PaymentController(IPaymentService service, IConfiguration configuration, IOrderService orderService)
        {
            _paymentService = service;
            _configuration = configuration;
            _orderService = orderService;
        }
        public IActionResult Index()
        {
            PaymentModel _paymentModel = new PaymentModel();
            CartModel _cartModel = TempData.Peek<CartModel>("Cart");
            if(_cartModel != null)
            {
                _paymentModel.Cart = _cartModel;
                _paymentModel.GrandTotal = _cartModel.GrandTotal;
                _paymentModel.Currency = "INR";
                _paymentModel.Description = string.Join(",",_cartModel.Items.Select(i=>i.Name));
                _paymentModel.Receipt= Guid.NewGuid().ToString();
                _paymentModel.RazorpayKey = _configuration["RazorPay:Key"];
                _paymentModel.OrderId= _paymentService.CreateOrder(_paymentModel.GrandTotal, _paymentModel.Currency, _paymentModel.Receipt);

            }
            return View(_paymentModel);
        }

        [HttpPost]
        public IActionResult Status(IFormCollection form)
        {
            try
            {
                if (form.Keys.Count > 0)
                {
                    string orderId = form["rzp_orderid"];
                    string paymentId = form["rzp_paymentid"];
                    string signature = form["rzp_signature"];
                    string transactionId = form["Receipt"];
                    string currency = form["Currency"];

                    bool isValid = _paymentService.VerifySignature(signature, orderId, paymentId);
                    if (isValid)
                    {
                        CartModel cart = TempData.Peek<CartModel>("Cart");
                        PaymentDetail model = new PaymentDetail();

                        model.CartId = cart.Id;
                        model.Total = cart.Total;
                        model.Tax = cart.Tax;
                        model.Currency = currency;
                        model.GrandTotal = cart.GrandTotal;
                        model.CreatedDate = DateTime.Now;

                        model.Status = "Success";
                        model.TransactionId = transactionId;
                        model.Id = paymentId;
                        model.Email = CurrentUser.Email;
                        model.UserId = CurrentUser.Id;

                        int status = _paymentService.SavePaymentDetails(model);
                        if (status > 0)
                        {
                            Response.Cookies.Delete("CId");
                            TempData.Remove("Cart");

                            AddressModel addressModel = TempData.Get<AddressModel>("Address");
                            _orderService.PlaceOrder(CurrentUser.Id, orderId, paymentId, cart, addressModel);

                            TempData.Set("PaymentDetails", model);
                            return RedirectToAction("Receipt");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Your payment has been failed. You can contact us at support@dotnettricks.com.";
            }
            return View();
        }

        public IActionResult Receipt()
        {
            PaymentDetail paymentDetail = TempData.Get<PaymentDetail>("PaymentDetails");
            return View(paymentDetail);
        }
    }
}
