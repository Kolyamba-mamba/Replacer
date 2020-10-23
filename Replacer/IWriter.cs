using System.Collections.Generic;

namespace Replacer
{
    /// <summary>
    /// Интерфейс для записи строк
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// Запись строк
        /// </summary>
        /// <param name="strings">Коллекция строк</param>
        public void Write(IEnumerable<string> strings);
    }
}