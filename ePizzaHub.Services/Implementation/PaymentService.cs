using ePizzaHub.Core.Entities;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;


namespace ePizzaHub.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        RazorpayClient _client;
        IConfiguration _configuration;
        IRepository<PaymentDetail> _paymentRepo;
        ICartRepository _cartRepo;
        public PaymentService(IConfiguration configuration, IRepository<PaymentDetail> paymentRepo, ICartRepository cartRepo) 
        {
            _configuration=configuration;
            _paymentRepo = paymentRepo;
            _cartRepo = cartRepo;
            _client = new RazorpayClient(_configuration["RazorPay:Key"], _configuration["RazorPay:Secret"]);
        }
        public string CreateOrder(decimal amount, string Currency, string recipent)
        {
            Dictionary<string,object> options = new Dictionary<string,object>();
            options.Add("amount", Convert.ToInt32(amount));
            options.Add("receipt", recipent);
            options.Add("currency", "INR");
            Razorpay.Api.Order order = _client.Order.Create(options);
            return order["id"].ToString();

        }

        public Payment GetPaymentsDetails(string PaymentId)
        {
            return _client.Payment.Fetch(PaymentId);
        }

        public int SavePaymentDetails(PaymentDetail model)
        {
            _paymentRepo.Add(model);
            Cart cart = _cartRepo.GetCartId(model.CartId);
            cart.IsActive = false;
            return _paymentRepo.SaveChanges();
        }

        public bool VerifySignature(string signature, string orderId, string paymentId)
        {
            string payload = string.Format("{0}|{1}", orderId, paymentId);
            string secret = RazorpayClient.Secret;
            string actualSignature = getActualSignature(payload, secret);
            return actualSignature.Equals(signature);
        }

        private static string getActualSignature(string payload, string secret)
        {
            byte[] secretBytes = StringEncode(secret);
            HMACSHA256 hashHmac = new HMACSHA256(secretBytes);
            var bytes = StringEncode(payload);

            return HashEncode(hashHmac.ComputeHash(bytes));
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
