using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Common;
using PaymentGateway.Model;

namespace PaymentGateway.Services.Interface
{
    public interface IPaymentService
    {
        Task<PaymentModel> GetPayment(string cardNumber);
        Task<ApiResponse> CreatePayment([FromBody] PaymentModel payment);
        Task<ApiResponse> UpdatePayment([FromBody] PaymentModel payment);
        Task<ApiResponse> DeletePayment(string cardNumber);
    }
}