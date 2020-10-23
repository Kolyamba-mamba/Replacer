using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replacer
{
    /// <summary>
    /// Заменитель слов
    /// </summary>
    public static class WordReplacer
    {
        /// <summary>
        /// Заменить все слова из словаря во всех строках
        /// </summary>
        /// <param name="lines">Коллекция строк</param>
        /// <param name="wordsDict">Словарь с заменителями</param>
        /// <returns>Коллекция строк с замененными словами</returns>
        public static IEnumerable<string> ReplaceWordsInStrings(IReadOnlyCollection<string> lines,
            Dictionary<string, string> wordsDict)
        {
            var linesWithReplacedWords = new List<string>(lines.Count);
            linesWithReplacedWords.AddRange(lines.Select(line 
                => ReplaceWordsInString(line, wordsDict)));
            return linesWithReplacedWords;
        }

        /// <summary>
        /// Заменить слова в одной строке
        /// </summary>
        /// <param name="line">Изменяемая строка</param>
        /// <param name="wordDict">Словарь с заменителями</param>
        /// <returns>Строка с замененными словами</returns>
        private static string ReplaceWordsInString(string line, Dictionary<string, string> wordDict)
        {
            var word = new StringBuilder();
            var modLine = new StringBuilder();
            foreach (var symbol in line)
            {
                if (char.IsLetter(symbol) || char.IsNumber(symbol))
                    word.Append(symbol);
                else
                {
                    modLine.AddWord(word, wordDict);
                    word.Clear();
                    modLine.Append(symbol);
                }
            }
            modLine.AddWord(word, wordDict);
            return modLine.ToString();
        }

        /// <summary>
        /// Добавить слово из изменяемой строки в измененную
        /// </summary>
        /// <param name="line">Измененная строка</param>
        /// <param name="word">Слово из изменяемой строки</param>
        /// <param name="wordDict">Словарь с заменителями</param>
        private static void AddWord(this StringBuilder line, StringBuilder word, Dictionary<string, string> wordDict)
        {
            var strWord = word.ToString();
            if (wordDict.ContainsKey(strWord))
                line.Append(wordDict[strWord]);
            else
                line.Append(word);
        }
    }
}