using System.Threading.Tasks;

namespace Etl.Shared.Factories
{
    public interface IInputConnector
    {
         Task Recive(string content);
    }
}