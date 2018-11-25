using System;
using System.IO;

namespace Etl.Shared.Factories {
    public class FileSender : ISender {
        private readonly string _path;

        public FileSender (string path) {
            _path = path;
            Directory.CreateDirectory (_path);
        }
        public void Send (string content) {
            var filePath = Path.Combine (_path, string.Format (@"{0}.tmp", Guid.NewGuid ()));
            File.WriteAllText (filePath, content);
        }
    }
}