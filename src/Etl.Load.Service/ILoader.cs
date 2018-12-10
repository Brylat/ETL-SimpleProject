using System.Collections.Generic;
using System.Threading.Tasks;
using Etl.Shared.Entity;
using Etl.Shared.Factories;

namespace Etl.Load.Service
{
    public interface ILoader : IInputConnector
    {
        Task ClearAllData();
        Task<List<CarEntity>> GetAllCars();
         Task Load(string content);
         Task LoadFromFiles();
    }
}