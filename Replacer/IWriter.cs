using System.Collections.Generic;

namespace Replacer
{
    public interface IWriter
    {
        public void Write(IEnumerable<string> strings);
    }
}