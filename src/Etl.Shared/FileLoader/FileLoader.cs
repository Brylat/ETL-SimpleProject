using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Etl.Shared.FileLoader
{
    public class FileLoader : IFileLoader
    {
        public IEnumerable<string> GetNextFileContent(string catalogPath)
        {
            var fileList = Directory.EnumerateFiles(catalogPath);
            foreach(var file in fileList) {
                yield return File.ReadAllText(Path.Combine(catalogPath, file));
            }
        }

        public async Task CleanFolders(List<string> paths)
        {
            foreach(var path in paths){
                Directory.Delete(path, true);
            }
            await Task.CompletedTask;
        }
    }
}