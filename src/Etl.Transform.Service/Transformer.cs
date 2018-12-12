using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Etl.Load.Service;
using Etl.Shared;
using Etl.Shared.Factories;
using Etl.Shared.FileLoader;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenScraping;
using OpenScraping.Config;

namespace Etl.Transform.Service
{
    public class Transformer : ITransformer
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoader _loaderService;
        private readonly IFileLoader _fileLoader;
        private ISender _sender;

        private readonly Dictionary<string, string> jsonDictionary = new Dictionary<string, string>() {
            {"Offer", "Oferta od"},
            {"Category", "Kategoria"},
            {"Brand", "Marka pojazdu"},
            {"Model", "Model pojazdu"},
            {"Version", "Wersja"},
            {"ProductionYear", "Rok produkcji"},
            {"Mileage", "Przebieg"},
            {"Capacity", "Pojemność skokowa"},
            {"Fuel", "Rodzaj paliwa"},
            {"HorsePower", "Moc"},
            {"Transmission", "Skrzynia biegów"},
            {"DrivingGear", "Napęd"},
            {"Type", "Typ"},
            {"DoorsNumber", "Liczba drzwi"},
            {"SeatsNumber", "Liczba miejsc"},
            {"Colour", "Kolor"},
            {"IsMetallic", "Metalik"},
            {"Condition", "Stan"},
            {"FirstRegistration", "Pierwsza rejestracja"},
            {"IsRegisteredInPoland", "Zarejestrowany w Polsce"},
            {"CountryOfOrigin", "Kraj pochodzenia"},
            {"IsFirstOwner", "Pierwszy właściciel"},
            {"NoAccidents", "Bezwypadkowy"},
            {"ServiceHistory", "Serwisowany w ASO"},
            {"VIN", "VIN"},
            {"ParticleFilter", "Filtr cząstek stałych"}
        };

        public Transformer(IHostingEnvironment hostingEnvironment, ILoader loader, IFileLoader fileLoader)
        {
            _hostingEnvironment = hostingEnvironment;
            _loaderService = loader;
            _fileLoader = fileLoader;
        }

        public async Task Recive (string content) {
            await InitSender(WorkMode.Continuous);
            await Transform (content);
        }

        public async Task LoadFromFiles() {
            await InitSender(WorkMode.Partial);
            //catalog name from config
            foreach(var fileContent in _fileLoader.GetNextFileContent(Path.Combine(_hostingEnvironment.ContentRootPath, "AfterExtract"))){
                await Transform(fileContent);
            }
        }

        public async Task Transform (string content) {
            //transform intoJson, create object template in shared becouse we need the same object in Loader to deserialize
            var configJson = GenerateJson();
            var config = StructuredDataConfig.ParseJsonString(configJson);
            var openScraping = new StructuredDataExtractor(config);
            var scrapingResults = openScraping.Extract(content);

            await _sender.Send(scrapingResults.ToString());
        }

        private string GenerateJson(){
            var jsonObject = new JObject();
            foreach (var pair in jsonDictionary)
            {
                jsonObject.Add(new JProperty(pair.Key, $"//li[contains(@class, \'offer-params__item\')]//span[@class=\'offer-params__label\' and contains(text(), \'{pair.Value}\')]/following-sibling::div"));
            }

            jsonObject.Add(new JProperty("Equipment", "//div[contains(@class, \'offer-features__row\')]//li[@class=\'offer-features__item\']"));
            jsonObject.Add(new JProperty("Description", "//div[contains(@class, \'offer-description\')]"));
            jsonObject.Add(new JProperty("Price", "//span[contains(@class, \'offer-price__number\')]"));

            return jsonObject.ToString();
        }

        private async Task InitSender(WorkMode workMode)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "AfterTransform"); //todo path from config
            _sender = await new SenderFactory(workMode, path, _loaderService).GetSender();
        }
    }
} 