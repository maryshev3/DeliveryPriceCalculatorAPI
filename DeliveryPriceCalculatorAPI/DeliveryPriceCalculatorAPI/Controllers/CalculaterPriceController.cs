using DeliveryPriceCalculatorAPI.Model;
using DeliveryPriceCalculatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks.Dataflow;

namespace DeliveryPriceCalculatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculaterPriceController : ControllerBase
    {
        private readonly ILogger<CalculaterPriceController> _logger;

        private readonly IConfiguration _configuration;

        private readonly CDEKService _cdekService;

        public CalculaterPriceController(ILogger<CalculaterPriceController> logger, IConfiguration configuration, CDEKService CDEKService)
        {
            _logger = logger;
            _configuration = configuration;
            _cdekService = CDEKService;
        }

        static HttpClient client = new HttpClient();

        [HttpPost(Name = "CalculatePrice")]
         async public Task<ActionResult<double>> CalculatePrice(PackageDelivery Data)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return new DeliveryPrice(_cdekService.Calculator(Data).Result).GetDeliverySumByTarif(Convert.ToInt32(_configuration["CalculatorProperties:TaridId"]));
        }
    }
}