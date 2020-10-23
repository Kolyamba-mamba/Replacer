using System;
using System.IO;
using MatthiWare.CommandLine;

namespace Replacer
{
    /// <summary>
    /// Парсер параметров командной строки
    /// </summary>
    public class ConsoleArgsParser
    {
        private readonly string[] _args;

        public ConsoleArgsParser(string[] args) 
            => _args = args;

        /// <summary>
        /// Читатель из файла
        /// </summary>
        /// <exception cref="ArgumentException">Неправильный путь ко входному файлу</exception>
        public FileReader Reader
        {
            get
            {
                var inputFilename = _args[0];
                if(!FilePathIsValid(inputFilename))
                    throw new ArgumentException("Некорректный путь входного файла");

                try
                {
                    // Проверка доступа к файлу с помощью объекта файлового потока
                    using var stream = new FileStream(inputFilename, FileMode.Open, FileAccess.Read);
                    return new FileReader(inputFilename);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Нет доступа ко входному файлу");;
                }
            }
        }

        /// <summary>
        /// Писатель результата
        /// </summary>
        public IWriter Writer
        {
            get
            {
                var outputFilename = _args[1];
                if (!FilePathIsValid(outputFilename))
                    return new ConsoleWriter();
                
                var fileInfo = new FileInfo(outputFilename);
                if (fileInfo.Directory != null && !fileInfo.Directory.Exists)
                    fileInfo.Directory.Create();
                if (!fileInfo.Exists)
                    fileInfo.Create().Close();
                return new FileWriter(outputFilename);
            }
        }

        /// <summary>
        ///  Именованные аргументы командной строки
        /// </summary>
        /// <exception cref="Exception">Некорректные аргументы командной строки</exception>
        public CommandLineOptions Options
        {
            get
            {
                var options = new CommandLineParserOptions {AppName = "Replacer"};
                var parser = new CommandLineParser<CommandLineOptions>(options);
                var parsingResult = parser.Parse(_args);
                if (parsingResult.HelpRequested)
                    return null;
                if (parsingResult.HasErrors && !parsingResult.HelpRequested)
                    throw new Exception("Программа вызвана с некорректными аргументами");

                return parsingResult.Result;
            }
        }
        
        /// <summary>
        /// Проверить правильность пути к файлу
        /// </summary>
        /// <param name="filename">Путь к файлу</param>
        /// <returns>Bool</returns>
        internal static bool FilePathIsValid(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return false;
            if (filename.StartsWith("-"))
                return false;
            try
            {
                // Если путь не валиден, то FileInfo бросает исключение
                new FileInfo(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}