using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Replacer
{
    /// <summary>
    /// Писатель строк в файл
    /// </summary>
    public class FileWriter : IWriter
    {
        private readonly FileInfo _file;

        public FileWriter(string filename) => 
            _file = new FileInfo(filename);
        
        /// <summary>
        /// Запись строк в файл 
        /// </summary>
        /// <param name="strings">Коллекция строк</param>
        public void Write(IEnumerable<string> strings)
        {
            using var fileStream = _file.OpenWrite();
            foreach (var str in strings)
            {
                var bytesToWrite = Encoding.Default.GetBytes(str + "\n");
                fileStream.Write(bytesToWrite, 0, bytesToWrite.Length);
            }
        }
    }
}