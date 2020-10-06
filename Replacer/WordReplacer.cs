using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Replacer
{
    public static class WordReplacer
    {
        public static IEnumerable<string> ReplaceWordsInStrings(IReadOnlyCollection<string> lines,
            Dictionary<string, string> wordsDict)
        {
            var linesWithReplacedWords = new List<string>(lines.Count);
            linesWithReplacedWords.AddRange(lines.Select(line 
                => ReplaceWordsInString(line, wordsDict)));
            return linesWithReplacedWords;
        }

        private static string ReplaceWordsInString(string line, Dictionary<string, string> wordDict)
        {
            var word = new StringBuilder();
            var modLine = new StringBuilder();
            foreach (var symbol in line)
            {
                if (char.IsLetter(symbol))
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