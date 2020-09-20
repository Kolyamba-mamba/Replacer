using MatthiWare.CommandLine.Core.Attributes;

namespace Replacer
{
    public class CommandLineOptions
    {
        [Required]
        [Name("r", "replace")]
        [Description("Путь к файлу заменяемых слов")]
        public string ReplaceFilePath { get; set; }

        [Name("w", "with")]
        [Description("Слово, которым заменить")]
        [DefaultValue(null)]
        public string WordToReplaceWith { get; set; }

        [Name("d", "dict")]
        [Description("Является файл словарём или нет")]
        [DefaultValue(false)]
        public bool IsReplaceFileADictionary { get; set; }
    }
}