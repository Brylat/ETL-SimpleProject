using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etl.Extract.Service;
using Etl.Logger;
using Microsoft.AspNetCore.Mvc;

namespace Etl.Api.Controllers{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelController : ControllerBase{
        private readonly ICustomLogger _logger;
        private readonly ICarModelExtractor _carModelExtractor;
        public CarModelController(ICustomLogger logger, ICarModelExtractor carModelExtractor){
            _logger = logger;
            _carModelExtractor = carModelExtractor;
        }

        // GET api/carmodel
        [HttpGet("{brand}")]
        public async Task<ActionResult> Get(string brand){
            _logger.Log("Looking for a model list for selected brand: " + brand);
            return Ok(await _carModelExtractor.Extract(brand));
        }
  
    }
}