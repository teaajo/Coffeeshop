using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Stripe;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Coffeeshop.Controllers
{
    [ApiController]
    [Route("api/payment")]
    [Produces("application/json", "application/xml")]

    public class PaymentController : ControllerBase
    {
        private readonly IMapper mapper;
       

        public PaymentController(IMapper mapper)
        {
           

            this.mapper = mapper;
           
        }
        [HttpPost]
       
        public ActionResult<StripePaymentRequest> Post([FromBody] StripePaymentRequest paymentRequest)
        {
            StripeConfiguration.SetApiKey("sk_test_51L88RCD3UxX0UV5FLpMhPaTj3NxnbXK7cItzbGyqNNbOk7Ugh3F3LF2D6aF1E3QweRbxNx5m9acbu8aBTZVINVdp00273UliQL");

            // Create a Customer:
            var customerOptions = new CustomerCreateOptions
            {
                Source = paymentRequest.tokenId,//"tok_mastercard",//SourceToken
                Email = "teajovanovic@example.com",
                Name = "tea jovanovic",

            };
            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOptions);

            var myCharge = new ChargeCreateOptions
            {
               Customer = customer.Id,
                Amount = paymentRequest.amount*100,
                Currency = "rsd",

             //   Metadata = new Dictionary<string, string>(),
                
            };

            var chargeService = new ChargeService();
            var i=chargeService.Create(myCharge);

            return Ok(i);


        }

        public class StripePaymentRequest
        {
           public int customer { get; set; }
            public string tokenId { get; set; }
            
            public int amount { get; set; }

            public int orderId { get; set; }
        }
    }
}
