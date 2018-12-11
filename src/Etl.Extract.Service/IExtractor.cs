using System.Threading.Tasks;
using Etl.Shared;

namespace Etl.Extract.Service
{
    public interface IExtractor
    {
         Task Extract(WorkMode workMode, string basicUrl);
    }
}