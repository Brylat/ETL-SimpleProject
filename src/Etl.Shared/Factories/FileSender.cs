using System;
using System.IO;
using System.Threading.Tasks;

namespace Etl.Shared.Factories {
    public class FileSender : ISender {
        private readonly string _path;

        public FileSender (string path) {
            _path = path;
        }
        public async Task Send (string content) {
            if (string.IsNullOrEmpty(content)) return;
            if (!Directory.Exists(_path)) Directory.CreateDirectory (_path);
            var filePath = Path.Combine (_path, string.Format (@"{0}.tmp", Guid.NewGuid ()));
            File.WriteAllText (filePath, content);
            await Task.CompletedTask;
        }
    }
}