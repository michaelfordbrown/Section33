using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UtilityLibraries
{
    public struct LetterCount
    {
        public char letter;
        public uint count;
    }

    public class WordCount : IComparable<WordCount>
    {
       public string word { get; set; }
       public uint count { get; set; } 

        public int CompareTo(WordCount other)
        {
            if (this.count > other.count)
            {
                return -1;
            }
            else if (this.count < other.count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public static class DocumentLibrary
    {

        public static string DisplayTopFive(this List<WordCount> CountTable)
        {
            string Result = "";

            int i = 0;
            while ((i < CountTable.Count) && (i < 5))
            {
                Result += CountTable[i].word;
                Result += " (";
                Result += CountTable[i].count;
                Result += ")\n";
                i++;
            }
            return Result;
        }

        static string strWordPattern = @"([a-zA-Z]+)";
        public static string[] MatchWords(this string strInput)
        {
            
            MatchCollection matCollection = Regex.Matches(strInput, strWordPattern);

            string[] strWords = new string[matCollection.Count];

            for(int i = 0; i < matCollection.Count; i++)
            {
                strWords[i] = matCollection[i].Value.ToLower();
            }
            return strWords;

        }
        
        public static Tuple<SortedList<string, uint>, SortedList<string, uint>> CountLettersAndWords(this string[] strWords)
        {
            SortedList<string, uint> sdlWordCount = new SortedList<string, uint>();

            SortedList<string, uint> sdlLetterCount = new SortedList<string, uint>();

            for (int i = 0; i < strWords.Length; i++)
            {
                if (strWords[i].Length > 1)
                {
                    if (!sdlWordCount.ContainsKey(strWords[i]))
                    {
                        sdlWordCount.Add(strWords[i], 1);
                    }
                    else
                    {
                        sdlWordCount[strWords[i]] = sdlWordCount[strWords[i]] + 1;
                    }
                }
                else
                {
                    if (!sdlLetterCount.ContainsKey(strWords[i]))
                    {
                        sdlLetterCount.Add(strWords[i], 1);
                    }
                    else
                    {
                        sdlLetterCount[strWords[i]] = sdlLetterCount[strWords[i]] + 1;
                    }
                }
            }
            return new Tuple<SortedList<string, uint>, SortedList<string, uint>>(sdlLetterCount, sdlWordCount);
        }

        public static List<WordCount> SortByValue(this SortedList<string, uint> sdlWordCount)
        {
            List<WordCount> Words = new List <WordCount>();

            foreach(KeyValuePair<string, uint> sdlWord in sdlWordCount)
            {
                var wd = new WordCount();
                wd.word = sdlWord.Key;
                wd.count = sdlWord.Value;

                Words.Add(wd);
            }

            Words.Sort();

            return Words;
        }

        public static string DisplayTopFive(this string FilePath)
        {
            string text = System.IO.File.ReadAllText(@FilePath);

            var strMatchResult = MatchWords(text);

            var tupslCountResult = CountLettersAndWords(strMatchResult);

            var lscwLettersSortedResult = SortByValue(tupslCountResult.Item1);
            var lscwWordsSortedResult = SortByValue(tupslCountResult.Item2);

            string strTopFiveLetters = DisplayTopFive(lscwLettersSortedResult);
            string strTopFiveWords = DisplayTopFive(lscwWordsSortedResult);
            string strResult = strTopFiveLetters + "\n" + strTopFiveWords;

            return strResult;
        }
    }


}

