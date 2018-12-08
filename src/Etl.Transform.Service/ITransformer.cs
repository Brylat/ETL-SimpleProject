using System.Threading.Tasks;
using Etl.Shared.Factories;

namespace Etl.Transform.Service
{
    public interface ITransformer : IInputConnector
    {
         Task Transform(string content);
         Task LoadFromFiles();
    }
}