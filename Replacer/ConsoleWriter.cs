using System;
using System.Collections.Generic;

namespace Replacer
{
    /// <summary>
    /// Писатель строк в консоль
    /// </summary>
    public class ConsoleWriter : IWriter
    {
        /// <summary>
        /// Запись строк консоль 
        /// </summary>
        /// <param name="strings">Коллекция строк</param>
        public void Write(IEnumerable<string> strings)
        {
            foreach (var str in strings)
                Console.WriteLine(str);
        }
    }
}