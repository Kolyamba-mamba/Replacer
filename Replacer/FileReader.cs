using System.Collections.Generic;
using System.IO;

namespace Replacer
{
    public class FileReader
    {
        private readonly string _filename;

        public FileReader(string filename) => 
            _filename = filename;
        
        // Чтените строк из файла
        public IReadOnlyCollection<string> ReadAll()
            => File.ReadAllLines(_filename);
    }
}