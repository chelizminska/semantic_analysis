using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.EntityClient;
using System.IO;

namespace Semantic_analysis.Models
{
    public class TextAnalysis
    {
        public string Text { get; set; }
        public TextAnalysis(string text)
        {
            this.Text = text;
        }

        public int GetLength()
        {
            return Text.Length;
        }

        public int GetLengthWithNoSpaces()
        {
            int SpaceCount = 0;
            for (int i = 0; i < Text.Length; i++)
                if (Text[i] == ' ')
                    SpaceCount++;
            return GetLength() - SpaceCount;
        }

        private bool isAvaliableSymbol(char symbol)
        {
            if (symbol == '-' || symbol == '_' || char.IsLetterOrDigit(symbol))
                return true;
            else
                return false;
        }

        public int GetNumberOfWords()
        {
            bool isWord = false;
            int WordCount = 0;
            int i = 0;
            while (i < GetLength())
            {
                if (!isWord)
                {
                    if (isAvaliableSymbol(Text[i]))
                    {
                        WordCount++;
                        isWord = true;
                    }
                    i++;
                }
                else
                {
                    if (!isAvaliableSymbol(Text[i]))
                    {
                        isWord = false;
                    }
                    i++;
                }
            }

            return WordCount;
        }

        private List<string> FindAllWords()
        {
            bool isWord = false;
            int WordCount = 0;
            int i = 0;
            List<string> WordsList = new List<string>();
            string Word = "";
            while (i < GetLength())
            {
                if (!isWord)
                {
                    if (isAvaliableSymbol(Text[i]))
                    {
                        Word += Text[i];
                        WordCount++;
                        isWord = true;
                    }
                    i++;
                    if (i == GetLength())
                        WordsList.Add(Word);
                }
                else
                {
                    if (!isAvaliableSymbol(Text[i]))
                    {
                        WordsList.Add(Word);
                        Word = "";
                        isWord = false;
                    }
                    else
                        Word += Text[i];
                    i++;
                    if (i == GetLength())
                        WordsList.Add(Word);
                }
            }
            return WordsList;
        }

        public int GetNumberStopWords()
        {
            int NumberOfStopWords = 0;
            List<string> WordsList = FindAllWords();
            StopWordsContext stopWordsContext = new StopWordsContext();
            List<StopWords> stopWordsObjList = stopWordsContext.stopWords.ToList();
            List<string> stopWordsList = new List<string>();

            foreach (var i in stopWordsObjList)
            {
                stopWordsList.Add(i.StopWord);
            }

            foreach (var i in WordsList)
            {
                if (stopWordsList.Contains(i))
                {
                    NumberOfStopWords++;
                }
            }

            stopWordsContext.Dispose();
            return NumberOfStopWords;
        }

        public string GetWaterValue()
        {
            return ((double)GetNumberStopWords() / GetNumberOfWords() * 100).ToString("##.##") + " %";
        }
        
        public int GetNumberUniqeWords()
        {
            int UniqeWordsValue = 0;
            List<string> UniqeWords = new List<string>();
            List<string> Buf = FindAllWords();
            foreach (var i in Buf)
            {
                if (!UniqeWords.Contains(i))
                {
                    UniqeWordsValue++;
                    UniqeWords.Add(i);
                }
            }
            return UniqeWordsValue;
        }

        public List<string> GetAllUniqeWords()
        {
            List<string> UniqeWords = new List<string>();
            List<string> Buf = FindAllWords();
            foreach (var i in Buf)
            {
                if (!UniqeWords.Contains(i))
                {
                    UniqeWords.Add(i);
                }
            }
            return UniqeWords;
        }

    }
}