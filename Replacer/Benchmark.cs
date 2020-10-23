using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;

namespace Replacer
{
    public class BenchmarkCountLineToDictSize
    {
        public int LineCount { get; set; }
        public int DictLineCount { get; set; }
        public long Time { get; set; }
        
        public BenchmarkCountLineToDictSize(int lineCount, int dictLineCount, long time)
        {
            LineCount = lineCount;
            DictLineCount = dictLineCount;
            Time = time;
        }
    }
    
    public class BenchmarkCountWordToDictSize
    {
        public int WordCount { get; set; }
        public int DictLineCount { get; set; }
        public long Time { get; set; }
        
        public BenchmarkCountWordToDictSize(int wordCount, int dictLineCount, long time)
        {
            WordCount = wordCount;
            DictLineCount = dictLineCount;
            Time = time;
        }
    }
    
    public class Benchmark
    {
        private static readonly Random Rnd = new Random();
        private static StringBuilder GetRandomWord(int len)
        {
            const string alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            var word = new StringBuilder(len-1);
            for (var i = 0; i < len; i++)
            {
                var position = Rnd.Next(0, alphabet.Length - 1);
                word.Append(alphabet[position]);
            }

            return word;
        }

        private static string GetRandomLine(int wordCount)
        {
            var line = new StringBuilder();
            for (var i = 0; i < wordCount; i++)
            {
                var size = Rnd.Next(5, 10);
                line.Append(GetRandomWord(size));
                line.Append(' ');
            }

            return line.ToString();
        }

        private static Dictionary<string, string> GenerateDict(int dictLineCount)
        {
            var dict = new Dictionary<string, string>();
            for (var i = 0; i < dictLineCount; i++)
            {
                var size = Rnd.Next(5, 10);
                var key = GetRandomWord(size).ToString();
                while (dict.ContainsKey(key))
                    key = GetRandomWord(size).ToString();
                dict.Add(key, GetRandomWord(size).ToString());
            }

            return dict;
        }
        
        private static IReadOnlyCollection<string> GenerateText(int lineCount, int wordCount)
        {
            var text = new Collection<string>();
            for (var i = 0; i < lineCount; i++) text.Add(GetRandomLine(wordCount));

            return text;
        }

        private static List<BenchmarkCountLineToDictSize> GetResultBenchLineCount()
        {
            var replaceTimer = new Stopwatch();
            var countLineToDictSizes = new List<BenchmarkCountLineToDictSize>();
            for (var lineCount = 1000; lineCount <= 1000000; lineCount *= 10)
            for (var dictLineCount = 1000; dictLineCount <= 128000; dictLineCount *= 2)
            {
                var text = GenerateText(lineCount, 100);
                var wordsDict = GenerateDict(dictLineCount);
            
                replaceTimer.Start();
                var replacedData = WordReplacer.ReplaceWordsInStrings(text, wordsDict);
                replaceTimer.Stop();
                
                countLineToDictSizes.Add(new BenchmarkCountLineToDictSize(lineCount, dictLineCount, replaceTimer.ElapsedMilliseconds));
                
                Console.WriteLine($"Строк: {lineCount}\n" +
                                  $"Строк в словаре: {dictLineCount}\n" +
                                  $"Замена слов заняла: {replaceTimer.ElapsedMilliseconds} мс\n");
            }

            return countLineToDictSizes;
        }

        private static List<BenchmarkCountWordToDictSize> GetResultBenshWordCount()
        {
            var replaceTimer = new Stopwatch();
            var benchmarkCountWordToDictSizes = new List<BenchmarkCountWordToDictSize>();
            for (var wordCount = 1000; wordCount <= 100000000; wordCount *=10)
            for (var dictLineCount = 1000; dictLineCount <= 128000; dictLineCount *= 2)
            {
                var text = GenerateText(1, wordCount);
                var wordsDict = GenerateDict(dictLineCount);
            
                replaceTimer.Start();
                var replacedData = WordReplacer.ReplaceWordsInStrings(text, wordsDict);
                replaceTimer.Stop();
                
                benchmarkCountWordToDictSizes.Add(new BenchmarkCountWordToDictSize(wordCount, dictLineCount, replaceTimer.ElapsedMilliseconds));
                
                Console.WriteLine($"Слов: {wordCount}\n" +
                                  $"Строк в словаре: {dictLineCount}\n" +
                                  $"Замена слов заняла: {replaceTimer.ElapsedMilliseconds} мс\n");
            }

            return benchmarkCountWordToDictSizes;
        }
        
        // public static void Main()
        // {
        //     const string pathLineCsvFile = "C:\\Users\\Nikolay\\Desktop\\BenchResult\\linebenchresult.csv";
        //     const string pathWordCsvFile = "C:\\Users\\Nikolay\\Desktop\\BenchResult\\wordbenchresult.csv";
        //
        //     var lineBench = GetResultBenchLineCount();
        //     using var swl = new StreamWriter(pathLineCsvFile);
        //     using var csvLineWriter = new CsvWriter(swl, CultureInfo.InvariantCulture);
        //     csvLineWriter.WriteRecords(lineBench);
        //
        //     var wordBench = GetResultBenshWordCount();
        //     using var sww = new StreamWriter(pathWordCsvFile);
        //     using var csvWordWriter = new CsvWriter(sww, CultureInfo.InvariantCulture);
        //     csvWordWriter.WriteRecords(wordBench);
        // }
    }
}