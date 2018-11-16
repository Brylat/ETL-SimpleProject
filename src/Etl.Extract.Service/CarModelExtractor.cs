using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using Etl.Logger;
using Etl.Extract.Service.dto;

namespace Etl.Extract.Service
{
    public class CarModelExtractor : ICarModelExtractor
    {
        private readonly ICustomLogger _logger;
        private readonly string BASIC_URL = "https://www.otomoto.pl/osobowe/";

        public CarModelExtractor(ICustomLogger logger){
            _logger = logger;
        }

        public async Task<List<CarModelDto>> Extract(string carBrand){
            var brandUrl = BASIC_URL + carBrand;
            var config = Configuration.Default.WithDefaultLoader().WithCss();
            var document = await BrowsingContext.New(config).OpenAsync(brandUrl);
            return await getBrandData(document, carBrand);
        }


        private async Task<List<CarModelDto>> getBrandData(AngleSharp.Dom.IDocument document, string carBrand){
            var config = Configuration.Default.WithDefaultLoader().WithCss();
            return await generateModelList(document.QuerySelectorAll("ul.modelsLinks li a"), carBrand);
        }

        private async Task<List<CarModelDto>> generateModelList(AngleSharp.Dom.IHtmlCollection<AngleSharp.Dom.IElement> data, string carBrand){
            _logger.Log($"Returned {data.Length} model entries for selected brand: {carBrand}");
            var carModelList = new List<CarModelDto>();
            foreach (var item in data){
                var carModelDto = new CarModelDto();
                carModelDto.modelName = item.TextContent.Replace("  ", string.Empty).ToString();
                carModelDto.modelValue = item.GetAttribute("title").ToLower().Replace("  " + carBrand.ToLower().ToString() + " ", string.Empty).Replace(" ", "-").ToString();
                carModelList.Add(carModelDto);
            }
            return carModelList;
        }
    }
}