using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Replacer;

namespace Test
{
    public class WordReplacerShould
    {
        [Test]
        public void Success_WhenSingleLineSingleWord()
        {
            var dict = new Dictionary<string, string> {{"word1", "repl_word1"}};
            var testStr = new []{"word words word1"};
            var expected = new []{"word words repl_word1"};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            Assert.True(expected.SequenceEqual(result));
        }

        [Test]
        public void Success_WhenEmptyString()
        {
            var dict = new Dictionary<string, string> {{"word1", "repl_word1"}};
            var testStr = new []{""};
            var expected = new []{""};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenEmptyDictionary()
        {
            var dict = new Dictionary<string, string>();
            var testStr = new []{"word words word1"};
            var expected = new []{"word words word1"};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenMultipleWordsInDictionary()
        {
            var dict = new Dictionary<string, string>
            {
                {"word1", "repl_word1"},
                {"word2", "repl_word2"},
                {"word3", "repl_word3"}
            };
            var testStr = new []{"word words word1 word2 word3"};
            var expected = new []{"word words repl_word1 repl_word2 repl_word3"};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenMultipleLines()
        {
            var dict = new Dictionary<string, string> {{"word1", "repl_word1"}};
            var testStr = new Collection<string>
            {
                "word words word1",
                "word1 word words"
            };
            var expected = new []
            {
                "word words repl_word1",
                "repl_word1 word words"
            };

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenMultipleLinesMultipleWords()
        {
            var dict = new Dictionary<string, string> 
            {
                {"word1", "repl_word1"},
                {"word2", "repl_word2"},
                {"word3", "repl_word3"}
            };
            var testStr = new []
            {
                "word words word1",
                "word words word2",
                "word words word3"
            };
            var expected = new []
            {
                "word words repl_word1",
                "word words repl_word2",
                "word words repl_word3"
            };;

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenNoWordInLine()
        {
            var dict = new Dictionary<string, string> {{"word1", "repl_word1"}};
            var testStr = new []{"word words word"};
            var expected = new []{"word words word"};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            
            Assert.True(expected.SequenceEqual(result));
        }
        
        [Test]
        public void Success_WhenLineWithDifferentSeparators()
        {
            var dict = new Dictionary<string, string> {{"word1", "repl_word1"}};
            var testStr = new []{"word words word1!word1      word1??!word1"};
            var expected = new []{"word words repl_word1!repl_word1      repl_word1??!repl_word1"};

            var result = WordReplacer.ReplaceWordsInStrings(testStr, dict);
            
            Assert.True(expected.SequenceEqual(result));
        }
    }
}