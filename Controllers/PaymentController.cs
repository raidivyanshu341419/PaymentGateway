using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentGateway.Common;
using PaymentGateway.DB;
using PaymentGateway.DbModel;
using PaymentGateway.Model;
using static PaymentGateway.Common.CommonEnum;

namespace PaymentGateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController ]
    
    public class PaymentController : ControllerBase
    {
        private readonly PaymentContext _payment;

        public PaymentController(PaymentContext paymentContext)
        {
            _payment = paymentContext;
        }

        
        [HttpGet]
        public async Task<PaymentModel> GetPayment(string cardNumber)
        {
            PaymentModel payment = new PaymentModel();
            try
            {
                var paymentData =await _payment.Payments.Where(x => x.CardNumber == cardNumber).FirstOrDefaultAsync();
                if (paymentData != null)
                {
                    payment.Id = paymentData.Id;    
                    payment.CardNumber = paymentData.CardNumber;
                    payment.ExpiryMonth = paymentData.ExpiryMonth;
                    payment.ExpiryYear = paymentData.ExpiryYear;
                    payment.CardHolderName = paymentData.CardHolderName;
                    payment.CVV = paymentData.CVV;
                    payment.Amount = paymentData.Amount;

                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
            }
            return payment;
        }

        [HttpPost]
        public async Task<ApiResponse> CreatePayment([FromBody] PaymentModel payment)
        {
            ApiResponse response = new ApiResponse();
            var paymentData = _payment.Payments.Where(x => x.CardNumber == payment.CardNumber).FirstOrDefault();
            try
            {
                if (paymentData == null)
                {
                    if (payment.Amount <= 0)
                    {
                        response.Code = (int)ResponseEnum.invalid_request;
                        response.Message = "Invalid payment amount.";
                    }
                    else
                    {
                        var user = _payment.Users.FirstOrDefault(x => x.UserId == payment.UserId);
                        if (user != null)
                        {
                            Payment model = new Payment()
                            {
                                CardNumber = payment.CardNumber,
                                CardHolderName = payment.CardHolderName,
                                ExpiryMonth = payment.ExpiryMonth,
                                ExpiryYear = payment.ExpiryYear,
                                CVV = payment.CVV,
                                Amount = payment.Amount,
                                GetUser = user
                            };
                            await _payment.Payments.AddAsync(model);
                            if (await _payment.SaveChangesAsync() > 0)
                            {
                                response.Code = (int)ResponseEnum.success;
                                response.Message = "Payment Added Successfully";
                            }
                        }
                    }
                }
                else
                {
                    response.Code = (int)ResponseEnum.not_found;
                    response.Message = "Payment Data not Found!";
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
        
        [HttpPut]
        public async Task<ApiResponse> UpdatePayment([FromBody] PaymentModel payment)
        {
            ApiResponse response = new ApiResponse();
            var paymentData = _payment.Payments.FirstOrDefault(x => x.CardNumber == payment.CardNumber);

            try
            {
                if (paymentData != null)
                {
                    if (payment.Amount <= 0)
                    {
                        response.Code = (int)ResponseEnum.invalid_request;
                        response.Message = "Invalid payment amount.";
                    }
                    else
                    {
                        paymentData.Amount = payment.Amount;
                        paymentData.CardHolderName = payment.CardHolderName;
                        paymentData.ExpiryMonth = payment.ExpiryMonth;
                        paymentData.ExpiryYear = payment.ExpiryYear;
                        paymentData.CVV = payment.CVV;
                        if (await _payment.SaveChangesAsync() > 0)
                        {
                            response.Code = (int)ResponseEnum.success;
                            response.Message = "Payment Updated Successfully";
                        }
                    }
                }
                else
                {
                    response.Code = (int)ResponseEnum.not_found;
                    response.Message = "Payment Data not Found!";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        [HttpDelete]
        public async Task<ApiResponse> DeletePayment(string cardNumber)
        {
            ApiResponse response = new ApiResponse();
            var paymentData = _payment.Payments.FirstOrDefault(x => x.CardNumber == cardNumber);
            try
            {
                if (paymentData != null)
                {
                    _payment.Payments.Remove(paymentData);
                    if (await _payment.SaveChangesAsync() > 0)
                    {
                        response.Code = (int)ResponseEnum.success;
                        response.Message = "Payment Deleted Successfully";
                    }
                }
                else
                {
                    response.Code = (int)ResponseEnum.not_found;
                    response.Message = "Payment Data not Found!";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

    }
}
