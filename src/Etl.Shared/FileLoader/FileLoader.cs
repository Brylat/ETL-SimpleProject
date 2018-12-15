using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Etl.Logger;

namespace Etl.Shared.FileLoader
{
    public class FileLoader : IFileLoader
    {
        private readonly ICustomLogger _logger;

        public FileLoader(ICustomLogger logger)
        {
            _logger = logger;
        }
        public IEnumerable<string> GetNextFileContent(string catalogPath)
        {
            _logger.Log($"Trying load file from: {catalogPath}");
            if(!Directory.Exists(catalogPath)){
                _logger.Log($"Catalog not exist");
                yield break;
            }
            var fileList = Directory.EnumerateFiles(catalogPath);
            _logger.Log($"Found {fileList.Count()} files in catalog: {catalogPath}");
            foreach(var file in fileList) {
                yield return File.ReadAllText(Path.Combine(catalogPath, file));
            }
        }

        public async Task CleanFolders(List<string> paths)
        {
            foreach(var path in paths){
                if(Directory.Exists(path)){
                    Directory.Delete(path, true);
                    _logger.Log($"Delete folder with content: {path}");
                }
            }
            await Task.CompletedTask;
        }
    }
}