using System.IO;
using Etl.Shared.FileLoader;
using Microsoft.AspNetCore.Hosting;
using Etl.Load.Service.BaseContext;
using Etl.Shared.Entity;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Etl.Logger;

namespace Etl.Load.Service
{
    public class Loader : ILoader
    {
        private readonly IFileLoader _fileLoader;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly BaseContext.Context _context;
        private readonly ICustomLogger _logger;

        public Loader(IFileLoader fileLoader, IHostingEnvironment hostingEnvironment, BaseContext.Context context, ICustomLogger logger)
        {
            _fileLoader = fileLoader;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _logger = logger;
        }

        public async Task<List<CarEntity>> GetAllCars()
        {
            return await Task.FromResult(_context.Cars.ToList());
        }
        public async Task Load(string content)
        {
            try
            {
                var car = JsonConvert.DeserializeObject<CarEntity>(content);
                _context.Add<CarEntity>(car);
                await _context.SaveChangesAsync();
            }
            catch
            {
                _logger.Log("Error while parsing or save to database for one item.");
            }
        }

        public async Task<int> GetNumgerOfRecords()
        {
            return await Task.FromResult(_context.Cars.Count());
        }
        public async Task ClearAllData()
        {
            _context.Database.ExecuteSqlCommand(@"TRUNCATE TABLE ""Cars""");
            _logger.Log("Database was cleaned.");
            await Task.CompletedTask;
        }
        public async Task Recive(string content)
        {
            await Load(content);
        }

        public async Task LoadFromFiles()
        {
            var beforeLoadCount = await GetNumgerOfRecords();
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "AfterTransform");
            foreach (var fileContent in _fileLoader.GetNextFileContent(path))
            {
                await Load(fileContent);
            }
            var afterLoadCount = await GetNumgerOfRecords();
            _logger.Log($"Load {afterLoadCount - beforeLoadCount} records to database");
            await _fileLoader.CleanFolders(new List<string>() {path});
        }
    }
}