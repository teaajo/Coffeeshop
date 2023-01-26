using Coffeeshop.Data;
using Coffeeshop.Models;
using Coffeeshop.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Coffeeshop.Controllers.PaymentController;

namespace Coffeeshop.Controllers
{
    [ApiController]
    [Produces("application/json", "application/xml")]
    public class WebhookController : ControllerBase
    {
   

        private readonly IPorudzbinaRepository porudzbinaRepository;



        public WebhookController(IPorudzbinaRepository porudzbinaRepository)
        {
            this.porudzbinaRepository = porudzbinaRepository;

        }    
        [HttpPost]
        [Route("api/webhook")]
        public async Task<IActionResult> Index()
        {
        
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                else if (stripeEvent.Type == Events.ChargeSucceeded)
                {
                    var paymentCharge = stripeEvent.Data.Object as Charge;

                    try
                    {

                       Porudzbine current = porudzbinaRepository.GetHighId();
                       
                        if (current == null)
                        {
                            return NotFound();
                        }
                        current.Status = "Placeno";
                        porudzbinaRepository.SaveChanges();

                        

                    }
                    catch (Exception)
                    {

                        return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


                    }

                    return Ok(json);

                }
                

                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok(json);
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }
        /*
        [HttpPut]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PorudzbineConfirmation> UpdateStatusPorudzbine(Porudzbine porudzbine)
        {


            try
            {

                var oldporudzbina = porudzbinaRepository.GetById(porudzbine.Id);
                if (oldporudzbina == null)
                {

                    return NotFound();
                }
                mapper.Map(porudzbine, oldporudzbina);
                porudzbinaRepository.SaveChanges();

                return Ok(mapper.Map<PorudzbineConfirmation>(oldporudzbina));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update error");


            }


        }*/


        public class Payment
    {

        string tokenId { get; set; }
        long amount { get; set; }
        string currency { get; set; }
        string email { get; set; }
        string date { get; set; }

       
    }



    }





}
