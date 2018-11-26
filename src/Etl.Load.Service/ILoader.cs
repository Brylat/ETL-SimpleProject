using Etl.Shared.Factories;

namespace Etl.Load.Service
{
    public interface ILoader : IInputConnector
    {
         void Load(string content);
         void LoadFromFiles();
    }
}