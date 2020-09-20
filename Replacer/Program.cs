using System;

// Реализовать программу, заменяющую слова из черного списка на звездочки либо на слова из словаря,
// результат записать в другой файл либо вывести на консоль. 
// Параметры:
// •	путь к входному файлу (первый позиционный параметр);
// •	путь к выходному файлу (второй позиционный параметр), если не задан – результат выводится на консоль;
// •	список заменяемых слов: (-r, -relpace) – путь к файлу, содержащему список заменяемых слов
// •	символ для замены (-w, -with) – строка, 
// •	признак использования словаря (-d, -dict) – если задан, то –w не допустим,
//         а список –r должен в каждой строке через пробел содержать заменяемое слово и слово,
//         на которое производится замена.


namespace Replacer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var argParser = new ConsoleArgsParser(args);
                var options = argParser.Options;
                if (options == null)
                    return;
                var reader = argParser.Reader;
                var writer = argParser.Writer;

                var wordsDict = options.GetWordsFromOptions();
                var inputData = reader.ReadAll();

                var replacedData = WordReplacer.ReplaceWordsInStrings(inputData, wordsDict);
            
                writer.Write(replacedData);
            }
            
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }
    }
}