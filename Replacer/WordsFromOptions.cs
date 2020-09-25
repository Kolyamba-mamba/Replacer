using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Replacer
{
    public static class WordsFromOptions
    {
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
                var splittedElem = line.Split(' ');
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