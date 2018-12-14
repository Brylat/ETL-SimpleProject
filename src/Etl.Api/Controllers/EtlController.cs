using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Etl.Extract.Service;
using Etl.Load.Service;
using Etl.Logger;
using Etl.Shared;
using Etl.Shared.FileLoader;
using Etl.Transform.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Etl.Api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class EtlController : ControllerBase {
        private readonly ICustomLogger _logger;
        private readonly IExtractor _extractor;
        private readonly ITransformer _transformer;
        private readonly ILoader _loader;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileLoader _fileLoader;

        public EtlController (ICustomLogger logger, IExtractor extractor, ITransformer transformer, ILoader loader, IHostingEnvironment hostingEnvironment, IFileLoader fileLoader) {
            _logger = logger;
            _extractor = extractor;
            _transformer = transformer;
            _loader = loader;
            _hostingEnvironment = hostingEnvironment;
            _fileLoader = fileLoader;
        }

        [HttpPost ("fullEtl")]
        public async Task<IActionResult> fullEtl ([FromBody] EtlCommand command) {
            await _extractor.Extract (WorkMode.Continuous, command.Url);
            return Ok ();
        }

        [HttpPost ("onlyExtract")]
        public async Task<IActionResult> OnlyExtract ([FromBody] EtlCommand command) {
            await _extractor.Extract (WorkMode.Partial, command.Url);
            return Ok ();
        }

        [HttpGet ("onlyTransform")]
        public async Task<IActionResult> OnlyTransform () {
            await _transformer.LoadFromFiles ();
            return Ok ();
        }

        [HttpGet ("onlyLoad")]
        public async Task<IActionResult> OnlyLoad () {
            await _loader.LoadFromFiles ();
            return Ok ();
        }

        [HttpGet ("getAllCars")]
        public async Task<IActionResult> GetAllCars () {
            var cars = await _loader.GetAllCars ();
            return Ok (cars);
        }

        [HttpGet ("downloadAsCsv")]
        public async Task<IActionResult> DownloadAsCsv () {
            var fileName = Guid.NewGuid ().ToString ();
            var path = Path.Combine (_hostingEnvironment.ContentRootPath, fileName);
            using (StreamWriter writer = new StreamWriter (Path.Combine (path), false)) {
                var records = _loader.GetAllCars ();
                var csv = new CsvWriter (writer);
                csv.WriteRecords (await records);
            }
            var memory = new MemoryStream ();
            using (var stream = new FileStream (path, FileMode.Open)) {
                await stream.CopyToAsync (memory);
            }
            System.IO.File.Delete (path);
            memory.Position = 0;
            var downloadName = DateTime.Now.ToString () + ".csv";
            return File (memory, "text/csv", downloadName);
        }

        [HttpGet ("cleanTmpFolders")]
        public async Task<IActionResult> CleanTmpFolders () {
            var pathList = new List<string> () {
                Path.Combine (_hostingEnvironment.ContentRootPath, "AfterExtract"),
                Path.Combine (_hostingEnvironment.ContentRootPath, "AfterTransform")
            };
            await _fileLoader.CleanFolders (pathList);
            return Ok ();
        }

        [HttpGet ("cleanDatabase")]
        public async Task<IActionResult> CleanDatabase () {
            await _loader.ClearAllData ();
            return Ok ();
        }
    }
    public class EtlCommand {
        public string Url { get; set; }
    }
}