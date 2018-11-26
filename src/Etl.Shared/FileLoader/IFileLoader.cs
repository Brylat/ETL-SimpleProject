using System.Collections.Generic;

namespace Etl.Shared.FileLoader
{
    public interface IFileLoader
    {
         IEnumerable<string> GetNextFileContent(string catalogPath);
    }
}