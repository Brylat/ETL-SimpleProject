using System.Threading.Tasks;

namespace Etl.Shared.Factories
{
    public interface ISender
    {
         Task Send(string content);
    }
}