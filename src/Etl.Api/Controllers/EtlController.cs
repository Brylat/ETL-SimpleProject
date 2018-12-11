using System;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Etl.Extract.Service;
using Etl.Load.Service;
using Etl.Logger;
using Etl.Shared;
using Etl.Transform.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Etl.Api.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class EtlController : ControllerBase
    {
        private readonly ICustomLogger _logger;
        private readonly IExtractor _extractor;
        private readonly ITransformer _transformer;
        private readonly ILoader _loader;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EtlController (ICustomLogger logger, IExtractor extractor, ITransformer transformer, ILoader loader, IHostingEnvironment hostingEnvironment) {
            _logger = logger;
            _extractor = extractor;
            _transformer = transformer;
            _loader = loader;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet ("fullEtl")]
        public async Task<IActionResult> fullEtl ([FromBody]string url) {
            await _extractor.Extract(WorkMode.Continuous, url);
            return Ok();
        }

        [HttpGet ("onlyExtract")]
        public async Task<IActionResult> OnlyExtract ([FromBody]string url) {
            await _extractor.Extract(WorkMode.Partial, url);
            return Ok();
        }

        [HttpGet ("onlyTransform")]
        public async Task<IActionResult> OnlyTransform () {
            await _transformer.LoadFromFiles();
            return Ok();
        }

        [HttpGet ("onlyLoad")]
        public async Task<IActionResult> OnlyLoad () {
            await _loader.LoadFromFiles();
            return Ok();
        }

        [HttpGet ("getAllCars")]
        public async Task<IActionResult> GetAllCars () {
            var cars = await _loader.GetAllCars();
            return Ok(cars);
        }

        [HttpGet ("downloadAsCsv")]
        public async Task<IActionResult> DownloadAsCsv () {
            var fileName = Guid.NewGuid().ToString();
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, fileName);
            using (StreamWriter writer = new StreamWriter (Path.Combine(path), false)) {
                var records = await _loader.GetAllCars();
                var csv = new CsvWriter (writer);
                csv.WriteRecords (records);
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            System.IO.File.Delete(path);
            memory.Position = 0;
            var downloadName = DateTime.Now.ToString()+".csv";
            return File(memory, "text/csv", downloadName);
        }
    }
}