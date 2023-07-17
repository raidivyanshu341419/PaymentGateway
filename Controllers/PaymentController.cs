using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PaymentGateway.Common;
using PaymentGateway.DB;
using PaymentGateway.Model;
using PaymentGateway.Model.DbModel;
using PaymentGateway.Services.Interface;
using static PaymentGateway.Common.CommonEnum;

namespace PaymentGateway.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController ]
    
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _payment;

        public PaymentController(IPaymentService payment)
        {
            _payment = payment;
        }

        //Get api/GetPayment
        [Route("GetPayment")]
        [HttpGet]
        public async Task<IActionResult> GetPayment(string cardNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = await _payment.GetPayment(cardNumber);
            return Ok(data);
        }


        //Post api/CreatePayment
        [Route("CreatePayment")]
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentModel payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = await _payment.CreatePayment(payment);
            return Ok(data);
        }

        //Put api/UpdatePayment
        [HttpPut]
        public async Task<IActionResult> UpdatePayment([FromBody] PaymentModel payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = await _payment.UpdatePayment(payment);
            return Ok(data);
        }

        //Delete api/DeletePayment
        [HttpDelete]
        public async Task<IActionResult> DeletePayment(string cardNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = await _payment.DeletePayment(cardNumber);
            return Ok(data);
        }

    }
}
