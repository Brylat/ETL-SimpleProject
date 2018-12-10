using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etl.Extract.Service;
using Etl.Load.Service;
using Etl.Logger;
using Etl.Shared;
using Etl.Transform.Service;
using Microsoft.AspNetCore.Mvc;

namespace Etl.Api.Controllers {
        [Route ("api/[controller]")]
        [ApiController]
        public class ValuesController : ControllerBase {
            private readonly ICustomLogger _logger;
            private readonly IExtractor _extractor;

            private readonly ITransformer _transformer;
            private readonly ILoader _loader;
            public ValuesController (ICustomLogger logger, IExtractor extractor, ITransformer transformer, ILoader loader) {
                _logger = logger;
                _extractor = extractor;
                _transformer = transformer;
                _loader = loader;
            }

            // GET api/values
            [HttpGet]
            public async Task<ActionResult<IEnumerable<string>>> Get () {
                _logger.Log ("Witaj");
                await _extractor.Extract (WorkMode.Continuous);
                //_transformer.LoadFromFiles();
                //await _loader.ClearAllData();
                return new string[] { "value1", "value2" };
            }

                // GET api/values/5
                [HttpGet ("{id}")]
                public ActionResult<string> Get (int id) {
                    return "value";
                }

                // POST api/values
                [HttpPost]
                public void Post ([FromBody] string value) { }

                // PUT api/values/5
                [HttpPut ("{id}")]
                public void Put (int id, [FromBody] string value) { }

                // DELETE api/values/5
                [HttpDelete ("{id}")]
                public void Delete (int id) { }
            }
        }