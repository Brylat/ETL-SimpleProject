using System.IO;
using Etl.Load.Service;
using Etl.Shared;
using Etl.Shared.Factories;
using Etl.Shared.FileLoader;
using Microsoft.AspNetCore.Hosting;

namespace Etl.Transform.Service {
    public class Transformer : ITransformer {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILoader _loaderService;
        private readonly IFileLoader _fileLoader;
        private ISender _sender;

        public Transformer (IHostingEnvironment hostingEnvironment, ILoader loader, IFileLoader fileLoader) {
            _hostingEnvironment = hostingEnvironment;
            _loaderService = loader;
            _fileLoader = fileLoader;
        }

        public void Recive (string content) {
            InitSender(WorkMode.Continuous);
            Transform (content);
        }

        public void LoadFromFiles() {
            InitSender(WorkMode.Partial);
            //catalog name from config
            foreach(var fileContent in _fileLoader.GetNextFileContent(Path.Combine(_hostingEnvironment.ContentRootPath, "AfterExtract"))){
                Transform(fileContent);
            }
        }

        public void Transform (string content) {
            //transform intoJson, create object template in shared becouse we need the same object in Loader to deserialize
            var newContent = content;
            
            _sender.Send(newContent);
        }

        private void InitSender (WorkMode workMode) {
            var path = Path.Combine (_hostingEnvironment.ContentRootPath, "AfterTransform"); //todo path from config
            _sender = new SenderFactory (workMode, path, _loaderService).GetSender ();
        }
    }
}