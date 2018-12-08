using System.IO;
using Etl.Shared.FileLoader;
using Microsoft.AspNetCore.Hosting;
using Etl.Load.Service.BaseContext;
using Etl.Shared.Entity;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Etl.Load.Service
{
    public class Loader : ILoader
    {
        private readonly IFileLoader _fileLoader;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly BaseContext.Context _context;

        public Loader(IFileLoader fileLoader, IHostingEnvironment hostingEnvironment, BaseContext.Context context)
        {
            _fileLoader = fileLoader;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public async Task Load(string content)
        {
            var car = JsonConvert.DeserializeObject<CarEntity>(content);
            await _context.AddAsync<CarEntity>(car);
        }

        public async Task Recive(string content)
        {
            await Load(content);
        }

        public async Task LoadFromFiles() {
            //catalog name from config
            foreach(var fileContent in _fileLoader.GetNextFileContent(Path.Combine(_hostingEnvironment.ContentRootPath, "AfterTransform"))){
                await Load(fileContent);
            }
        }
    }
}