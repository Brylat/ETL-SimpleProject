using Etl.Shared;

namespace Etl.Extract.Service
{
    public interface IExtractor
    {
         void Extract(WorkMode workMode);
    }
}