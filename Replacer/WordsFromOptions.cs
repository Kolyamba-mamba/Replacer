using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Replacer
{
    /// <summary>
    /// Получатель слов из командной строки
    /// </summary>
    public static class WordsFromOptions
    {
        /// <summary>
        /// Получение словаря из аргументов командной строки
        /// </summary>
        /// <param name="options">Объект именованных аргументов командной строки</param>
        /// <returns>Словарь вида слово : значение</returns>
        /// <exception cref="Exception">Если опции --with и --dict были написаны вместе ИЛИ если доступа к файлу-словарю нет</exception>
        public static Dictionary<string, string> GetWordsFromOptions(this CommandLineOptions options)
        {
            if (options.IsReplaceFileADictionary && options.WordToReplaceWith != '\0')
                throw new Exception("Опции -w / --with и -d / --dict не могут использоваться одновременно");
            if (!ConsoleArgsParser.FilePathIsValid(options.ReplaceFilePath))
                throw new Exception("Путь к файлу с заменяемыми словами - неверный");

            var replacedLines = File.ReadAllLines(options.ReplaceFilePath);
            var dict = new Dictionary<string, string>();

            foreach (var line in replacedLines)
            {
                var splittedElem = line.Split(':');
                string newWord;
                if (options.IsReplaceFileADictionary)
                    newWord = splittedElem.Last();
                else
                {
                    var elemLength = splittedElem.First().Length;
                    newWord = new string(options.WordToReplaceWith, elemLength);
                }

                dict[splittedElem.First()] = newWord;
            }

            return dict;
        }
    }
}