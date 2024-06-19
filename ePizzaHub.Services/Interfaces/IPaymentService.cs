using ePizzaHub.Core.Entities;
using Razorpay.Api;


namespace ePizzaHub.Services.Interfaces
{
    public interface IPaymentService
    {
        string CreateOrder(decimal amount, string Currency, string recipent);
        Payment GetPaymentsDetails(string PaymentId);
        bool VerifySignature(string signature, string OrderId, string PaymentId);
        int SavePaymentDetails(PaymentDetail model);
    }
}
