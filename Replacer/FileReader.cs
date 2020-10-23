using System.Collections.Generic;
using System.IO;

namespace Replacer
{
    /// <summary>
    /// Читатель из файла
    /// </summary>
    public class FileReader
    {
        private readonly string _filename;

        public FileReader(string filename) => 
            _filename = filename;
        
        /// <summary>
        /// Чтените строк из файла 
        /// </summary>
        /// <returns>Коллекция прочитанных строк</returns>
        public IReadOnlyCollection<string> ReadAll()
            => File.ReadAllLines(_filename);
    }
}