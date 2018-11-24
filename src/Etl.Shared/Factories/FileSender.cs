using System;
using System.IO;

namespace Etl.Shared.Factories {
    public class FileSender : ISender {
        private readonly string _path;

        public FileSender (string path) {
            _path = path;
        }
        public void Send (string content) {
            string filePath;
            do {
                filePath = Path.Combine (_path, string.Format (@"{0}.tmp", Guid.NewGuid ()));
            } while (File.Exists (filePath));

            File.WriteAllText (filePath, content);
        }
    }
}