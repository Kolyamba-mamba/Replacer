using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (var (word, replacement) in wordDict)
            {
                var wordIndex = line.IndexOf(word, StringComparison.Ordinal);
                while (wordIndex != -1)
                {
                    var nextIndex = wordIndex;
                    if (IsWord(line, wordIndex, word.Length))
                        // Заменить слово
                        line = line.Remove(wordIndex, word.Length).Insert(wordIndex, replacement);
                    else
                        nextIndex += word.Length;

                    wordIndex = line.IndexOf(word, nextIndex, StringComparison.Ordinal);
                }
            }

            return line;
        }
        
        private static bool IsWord(string line, int index, int wordLength)
        {
            // Левая граница слова валидна, если это начало строки или символ перед началом - небуквенный
            var isLeftBorderValid = index == 0
                                    || !char.IsLetter(line[index - 1]);
            // Правая граница слова валидна, если это конец строки или символ после конца - небуквенный
            var isRightBorderValid = index + wordLength == line.Length
                                     || !char.IsLetter(line[index + wordLength]);
            return isLeftBorderValid && isRightBorderValid;
        }
    }
}