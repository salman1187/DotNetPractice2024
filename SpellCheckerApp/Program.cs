using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SpellCheckerLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string outputFile = "C:\\Users\\MOHAMMAD SALMAN\\Documents\\outwords.txt";
            string inputFile = "C:\\Users\\MOHAMMAD SALMAN\\Documents\\inputwords.txt";
            SpellCheckerApp sc = new SpellCheckerApp();
            sc.InputFile = inputFile;
            sc.OutputFile = outputFile; 
            Stopwatch sw = Stopwatch.StartNew();
            sc.StartSpellCheck(inputFile, outputFile);
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }
    class SpellCheckerApp
    {
        public string InputFile;
        public string OutputFile;
        public InputFileLoader InputFileloader = new InputFileLoader();
        public SpellChecker Spellchecker = new SpellChecker();
        public void StartSpellCheck(string inputFile, string outputFile)
        {
            string inputString;
            using(StreamReader reader = new StreamReader(inputFile))
            {
                inputString = reader.ReadToEnd();
            }

            List<string> AllWords = InputFileloader.Loader(inputString);
            //foreach(string word in AllWords)
            Parallel.ForEach(AllWords, word =>
            {
                List<string> ans = Spellchecker.Check(word);
                if (ans.Count > 0)
                {
                    using (StreamWriter writer = new StreamWriter(outputFile, true))
                    {
                        writer.WriteLine($"{word}: ");
                        foreach (string w in ans)
                            writer.WriteLine(w);
                    }
                }
            });
        }
    }
    class InputFileLoader
    {
        public List<string> Loader(string inputFile)
        {
            List<string> words = new List<string>();
            string[] parts = Regex.Split(inputFile, @"\s+");
            foreach(string part in parts)
                if (!string.IsNullOrWhiteSpace(part))
                    words.Add(part);

            return words;
        }
    }
    class SpellChecker
    {
        public LevenshteinDistance Ldistance = new LevenshteinDistance();
        DictionaryFileLoader Dictionaryfileloader = new DictionaryFileLoader();
        public List<string> Check(string str)
        {
           
            List<string> ans = new List<string>();
            //do
            string inputString;
            using (StreamReader reader = new StreamReader("C:\\Users\\MOHAMMAD SALMAN\\Documents\\words.txt"))
            {
                inputString = reader.ReadToEnd();
            }
            List<string> correctWords = Dictionaryfileloader.Load(inputString);
            Dictionary<string, int> wordDistances = new Dictionary<string, int>();
            SortedSet<KeyValuePair<String, int>> pq = new SortedSet<KeyValuePair<String, int>>(new DistanceComparer());
            foreach (string word in correctWords)
            {
                wordDistances[word] = Math.Abs(Ldistance.CalculateDistance(str, word));
                if (wordDistances[word] == 0)
                    return ans;
            }

            foreach (var entry in wordDistances)
            {
                pq.Add(entry);
            }

            var enumerator = pq.GetEnumerator();
            int count = 0;
            while (enumerator.MoveNext() && count < 10)
            {
                ans.Add(enumerator.Current.Key);
                count++;
            }

            return ans; 
        }
    }
    class LevenshteinDistance
    {
        public int CalculateDistance(string str1, string str2)
        {
            int[,] distanceMatrix = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 0; i <= str1.Length; i++)
            {
                distanceMatrix[i, 0] = i;
            }
            for (int j = 0; j <= str2.Length; j++)
            {
                distanceMatrix[0, j] = j;
            }

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                    distanceMatrix[i, j] = Math.Min(Math.Min(
                        distanceMatrix[i - 1, j] + 1,        // deletion
                        distanceMatrix[i, j - 1] + 1),      // insertion
                        distanceMatrix[i - 1, j - 1] + cost); // substitution
                }
            }
            return distanceMatrix[str1.Length, str2.Length];
        }
    }
    class DictionaryFileLoader
    {
        public List<string> Load(string dictionaryFile)
        {
            List<string> words = new List<string>();
            string[] parts = Regex.Split(dictionaryFile, @"\s+");
            foreach (string part in parts)
                if (!string.IsNullOrWhiteSpace(part))
                    words.Add(part);

            return words;
        }
    }
    class DistanceComparer : IComparer<KeyValuePair<String, int>>
    {
        public int Compare(KeyValuePair<String, int> a, KeyValuePair<String, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }
    }

}
