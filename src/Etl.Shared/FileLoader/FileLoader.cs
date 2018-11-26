using System.Collections.Generic;
using System.IO;

namespace Etl.Shared.FileLoader
{
    public class FileLoader : IFileLoader
    {
        public IEnumerable<string> GetNextFileCntent(string catalogPath)
        {
            var fileList = Directory.EnumerateFiles(catalogPath);
            foreach(var file in fileList) {
                yield return File.ReadAllText(Path.Combine(catalogPath, file));
            }
        }
    }
}