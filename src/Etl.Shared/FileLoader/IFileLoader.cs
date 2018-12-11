using System.Collections.Generic;
using System.Threading.Tasks;

namespace Etl.Shared.FileLoader
{
    public interface IFileLoader
    {
         IEnumerable<string> GetNextFileContent(string catalogPath);
         Task CleanFolders(List<string> paths);
    }
}