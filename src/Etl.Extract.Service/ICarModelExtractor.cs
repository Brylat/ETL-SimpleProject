using System.Collections.Generic;
using System.Threading.Tasks;
using Etl.Extract.Service.dto;

namespace Etl.Extract.Service
{
    public interface ICarModelExtractor
    {
         Task<List<CarModelDto>> Extract(string carBrand);
    }
}