using System;
using System.Collections.Generic;

namespace Replacer
{
    public class ConsoleWriter : IWriter
    {
        // Запись строк консоль
        public void Write(IEnumerable<string> strings)
        {
            foreach (var str in strings)
                Console.WriteLine(str);
        }
    }
}