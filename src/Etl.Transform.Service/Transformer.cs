using Etl.Shared.Factories;

namespace Etl.Transform.Service
{
    public class Transformer : ITransformer
    {
        public void Recive(string content)
        {
            Transform(content);
        }

        public void Transform(string content)
        {
            throw new System.NotImplementedException();
        }
    }
}