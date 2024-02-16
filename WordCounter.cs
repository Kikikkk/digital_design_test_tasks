using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: WordCounter <inputFilePath> <outputFilePath>");
            return;
        }

        string inputFilePath = args[0];
        string outputFilePath = args[1];

        try
        {
            Dictionary<string, int> wordCount = CountWords(inputFilePath);

            WriteWordCountToFile(wordCount, outputFilePath);

            Console.WriteLine("Word count successfully saved to {0}", outputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: {0}", ex.Message);
        }
    }

    static Dictionary<string, int> CountWords(string filePath)
    {
        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] words = line.Split(new char[] { ' ', '\t', ',', '.', ':', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    string cleanedWord = CleanWord(word);
                    if (!string.IsNullOrEmpty(cleanedWord))
                    {
                        if (wordCount.ContainsKey(cleanedWord))
                        {
                            wordCount[cleanedWord]++;
                        }
                        else
                        {
                            wordCount[cleanedWord] = 1;
                        }
                    }
                }
            }
        }

        return wordCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    static string CleanWord(string word)
    {
        return new string(word.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray()).ToLower();
    }

    static void WriteWordCountToFile(Dictionary<string, int> wordCount, string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var pair in wordCount)
            {
                writer.WriteLine($"{pair.Key}\t\t{pair.Value}");
            }
        }
    }
}
