using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using Etl.Logger;
using Etl.Shared;
using Etl.Shared.Factories;
using Microsoft.AspNetCore.Hosting;

namespace Etl.Extract.Service {
    public class Extractor : IExtractor {

        private readonly ICustomLogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private ISender _sender;

        public Extractor (ICustomLogger logger, IHostingEnvironment hostingEnvironment) {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public async void Extract () {
            InitSender(WorkMode.Partial); //set from method param
            var basicUrl = "https://www.otomoto.pl/osobowe/aixam/";
            var numberOfPage = await GetNumberOfPages (basicUrl);
            _logger.Log ($"Number of pages: {numberOfPage}");
            var articlesUrl = new List<string> ();
            for (int i = 1; i <= numberOfPage; i++) {
                articlesUrl.AddRange (await GetArticlesUrlFromPage (basicUrl + "?page=" + i));
            }
            _logger.Log ($"Number of articles: {articlesUrl.Count}");

            foreach (var url in articlesUrl) {
                _sender.Send (await GetArticleContent (url));
            }
        }
        
        private async Task<string> GetArticleContent (string url) {
            var config = Configuration.Default.WithDefaultLoader ()
                .WithCss ();
            // .WithJavaScript();
            var document = await BrowsingContext.New (config).OpenAsync (url);
            return document.All
                .Where (x => x.ClassName == "offer-content__main-column").Single ().OuterHtml;
        }

        private async Task<List<string>> GetArticlesUrlFromPage (string url) {
            var config = Configuration.Default.WithDefaultLoader ()
                .WithCss ();
            var document = await BrowsingContext.New (config).OpenAsync (url);
            var content = document.All
                .Where (x => x.LocalName == "article").Select (x => x.GetAttribute ("data-href")).ToList ();

            return await Task.FromResult (content);
        }

        private async Task<int> GetNumberOfPages (string url) {
            var config = Configuration.Default.WithDefaultLoader ()
                .WithCss ();
            var document = await BrowsingContext.New (config).OpenAsync (url);
            var bodySection = document.All
                .Where (x => x.Id == "body-container").Single ();
            var paggerRow = bodySection
                .Children.Where (x => x.ClassName == "container-fluid container-fluid-sm")
                .Single ()
                .Children.Where (x => x.ClassName == "row").ElementAt (1);

            if (!paggerRow.HasChildNodes) {
                return await Task.FromResult<int> (1);
            } else {
                var numberOfPages = paggerRow.Children.Where (x => x.ClassName == "om-pager rel").Single ()
                    .Children.Where (x => x.ClassName != "next abs").Last ()
                    .Children.First ()
                    .Children.First ()
                    .InnerHtml;

                int integerNumber = 0;
                if (Int32.TryParse (numberOfPages, out integerNumber)) {
                    return await Task.FromResult<int> (integerNumber);
                }
            }
            return await Task.FromResult<int> (0);
        }

        private void InitSender(WorkMode workMode) {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, "AfterExtract"); //todo path from config
            _sender = new SenderFactory(workMode, path).GetSender();
        }
    }
}