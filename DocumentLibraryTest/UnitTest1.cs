using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UtilityLibraries;

namespace DocumentLibraryTest
{
    [TestClass]
    public class DisplayTopFiveWordsTestMethod
    {
        [TestMethod]
        public void TestDisplayTopFiveWords()
        {
            Console.WriteLine("Test Display Top Five Words\n");
            //ARRANGE
            string ExpectedResult = "the (21)\nof (13)\nand (15)\nshe (20)\nalice (6)\n";

            List<WordCount> CountTable = new List<WordCount>()
            {
                new WordCount() { word = "the", count = 21 },
                new WordCount() { word = "of", count = 13 },
                new WordCount() { word = "and", count = 15 },
                new WordCount() { word = "she", count = 20 },
                new WordCount() { word = "alice", count = 6 }
            };

            //ACT
           var ActualResult = CountTable.DisplayTopFive();
           Console.WriteLine(ActualResult);

            //ASSERT
           StringAssert.Contains(ActualResult, ExpectedResult);
        }

        [TestMethod]
        public void TestDisplayTopFiveLetters()
        {
            Console.WriteLine("Test Display Top Five Letters\n");
            //ARRANGE
            string ExpectedResult = "a (56)\ns (34)\no (32)\ne (27)\nt (9)\n";

            List<WordCount> CountTable = new List<WordCount>()
            {
                new WordCount() {word = "a", count =  56 },
                new WordCount() {word = "s", count = 34 },
                new WordCount() {word = "o", count = 32 },
                new WordCount() {word = "e", count = 27 },
                new WordCount(){word = "t", count =  9 }
            };

            //ACT
            string ActualResult = CountTable.DisplayTopFive();
            Console.WriteLine(ActualResult);

            //ASSERT
            StringAssert.Contains(ActualResult, ExpectedResult);
        }

        [TestMethod]
        public void TestMatchWords()
        {
            Console.WriteLine("Test Match Words\n");
            //ARRANGE
            //REQ: Ignore punctuation including quotes, parentheses, question marks, exclamation marks, commas, semicolons, full stops - e.g treat "hello!" as the word "hello"
            //REQ: Single letters are included in the letter count Whitespace is ignored
            //REQ: Whitespace is not included in the letter count
            //REQ: Ignore different casing -e.g.hello and Hello are the same word
            //REQ: Hyphen separated words should appear as two separate words -e.g.sugar - free is sugar and free

            string strExpectedResult = "([a-z]+)";

            string strPhrase = "hello! world! \"Hello-World\"?\n\'HELLO WORLD\';   hOW ARE you doing tod4y? I'am doing,   fine as today I am sugar-free,     tomorrow is a different matter.";

            //ACT
            string[] ActualResult = strPhrase.MatchWords();

            //ASSERT
            Console.WriteLine("Input Phrase: {0}", strPhrase);
            foreach (string strS in ActualResult)
            {
                Console.WriteLine("Actual Result: {0}", strS);
                StringAssert.Matches(strS, new Regex(@strExpectedResult));
            }
        }

        [TestMethod]
        public void TestCountLettersAndWords()
        {
            Console.WriteLine("Test CountLettersAndWords\n");
            //ARRANGE
            // REQ: Do not count single letters as words - e.g ignore the letter a, treat "hello" as the word hello
            string[] strWords = { "alice", "a", "alice", "a", "alice", "a", "alice", "a", "alice",
                                            "wonderland", "i", "wonderland", "i", "wonderland"};

            int ExpectedWordsCount = 2;

            string ExpectedWord1 = "alice";
            uint ExpectedWord1Count = 5;

            string ExpectedWord2 = "wonderland";
            uint ExpectedWord2Count = 3;

            int ExpectedLettersCount = 2;

            string ExpectedLetter1 = "a";
            uint ExpectedLetter1Count = 4;

            string ExpectedLetter2 = "i";
            uint ExpectedLetter2Count = 2;

            //ACT
            var ActualResult = strWords.CountLettersAndWords();

            //ASSERT
            Console.WriteLine(" {0} = {1}", ActualResult.Item1.Keys[0], ActualResult.Item1.Values[0]);

            Assert.AreEqual(ActualResult.Item1.Count, ExpectedLettersCount);
            Assert.AreEqual(ActualResult.Item1.Keys[0], ExpectedLetter1);
            Assert.AreEqual(ActualResult.Item1.Values[0], ExpectedLetter1Count);

            Console.WriteLine(" {0} = {1}", ActualResult.Item1.Keys[1], ActualResult.Item1.Values[1]);
            Assert.AreEqual(ActualResult.Item1.Keys[1], ExpectedLetter2);
            Assert.AreEqual(ActualResult.Item1.Values[1], ExpectedLetter2Count);

            Console.WriteLine(" {0} = {1}", ActualResult.Item2.Keys[0], ActualResult.Item2.Values[0]);
            Assert.AreEqual(ActualResult.Item2.Count, ExpectedWordsCount);
            Assert.AreEqual(ActualResult.Item2.Keys[0], ExpectedWord1);
            Assert.AreEqual(ActualResult.Item2.Values[0], ExpectedWord1Count);

            Console.WriteLine(" {0} = {1}", ActualResult.Item2.Keys[1], ActualResult.Item2.Values[1]);
            Assert.AreEqual(ActualResult.Item2.Keys[1], ExpectedWord2);
            Assert.AreEqual(ActualResult.Item2.Values[1], ExpectedWord2Count);

        }

        [TestMethod]
        public void TestSortWordsByValue()
        {
            Console.WriteLine("Test SortWordsByValue\n");
            //ARRANGE
            SortedList<string, uint> sdlWordsCounted = new SortedList<string, uint> ();
            sdlWordsCounted.Add("alice", 2);
            sdlWordsCounted.Add("was", 4);
            sdlWordsCounted.Add("beginning", 1);
            sdlWordsCounted.Add("to", 9);
            sdlWordsCounted.Add("get", 6);
            sdlWordsCounted.Add("a", 7);
            sdlWordsCounted.Add("i", 3);

            int ExpectedLength = sdlWordsCounted.Count;

            //ACT
            List<WordCount> ActualResult;
            ActualResult = sdlWordsCounted.SortByValue();

            //ASSERT
            foreach (var item in ActualResult)
            {
                Console.WriteLine("{0} \t\t= {1}",item.word, item.count);
            }

            Assert.AreEqual(ActualResult.Count, ExpectedLength);
            for (int i = 0; i < ActualResult.Count; i++)
            {
                if (i > 0)
                {
                    Assert.IsTrue(ActualResult[i - 1].count >= ActualResult[i].count);
                }
            }
        }
    }
}
