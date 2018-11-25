using Etl.Shared.Factories;

namespace Etl.Transform.Service
{
    public interface ITransformer : IInputConnector
    {
         void Transform(string content);
    }
}