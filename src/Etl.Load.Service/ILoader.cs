using System.Threading.Tasks;
using Etl.Shared.Factories;

namespace Etl.Load.Service
{
    public interface ILoader : IInputConnector
    {
         Task Load(string content);
         Task LoadFromFiles();
    }
}