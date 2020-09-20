using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Replacer
{
    public class FileWriter : IWriter
    {
        private readonly FileInfo _file;

        public FileWriter(string filename) => 
            _file = new FileInfo(filename);
        
        // Запись строк в файл
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