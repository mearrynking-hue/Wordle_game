using System;
using System.IO;

class WordleProgram
{
    static void Main()
    {
        //start of the game
        string secretWord = RandomWord("words.txt");
        Console.WriteLine("==================\nWELCOME TO WORDLE\n==================");
        Console.WriteLine($"[DEBUG] Hidden word is: {secretWord}");
        Console.WriteLine("I thought of a 5-letter word. Can you guess it?");
    }

    static string RandomWord(string filePath)
    {
        //check if file exists
        if(!File.Exists(filePath))
        {
            Console.WriteLine("Error: 'words.txt' not found! Using default word.");
            return "RAVEN";
        }
         
        string[] words = File.ReadAllLines(filePath);

        //if file is empty, then we return default word
        if(words.Length == 0) return "RAVEN";

        Random rand = new Random();
        int index = rand.Next(words.Length);

        return words[index].Trim().ToUpper();
    }


}