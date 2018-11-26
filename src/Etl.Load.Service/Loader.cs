using System.IO;
using Etl.Shared.FileLoader;
using Microsoft.AspNetCore.Hosting;

namespace Etl.Load.Service
{
    public class Loader : ILoader
    {
        private readonly IFileLoader _fileLoader;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Loader(IFileLoader fileLoader, IHostingEnvironment hostingEnvironment)
        {
            _fileLoader = fileLoader;
            _hostingEnvironment = hostingEnvironment;
        }
        public void Load(string content)
        {
            throw new System.NotImplementedException();
        }

        public void Recive(string content)
        {
            Load(content);
        }

        public void LoadFromFiles() {
            //catalog name from config
            foreach(var fileContent in _fileLoader.GetNextFileContent(Path.Combine(_hostingEnvironment.ContentRootPath, "AfterTransform"))){
                Load(fileContent);
            }
        }
    }
}