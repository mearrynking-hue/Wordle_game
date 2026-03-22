using System;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Runtime.Versioning;

class WordleProgram
{
    static void Main()
    {
        //start of the game
        string secretWord = RandomWord("words.txt");
        Console.WriteLine("==================\nWELCOME TO WORDLE\n==================");
        Console.WriteLine($"[DEBUG] Hidden word is: {secretWord}");
        Console.WriteLine("I thought of a 5-letter word. Can you guess it?");

        for (int i=1; i<=6; i++)
        {
            Console.WriteLine($"Attempt {i} of 6: ");
            string guess = (Console.ReadLine() ?? "").ToUpper();

            if (guess.Length<5 || guess.Length>5)
            {
                Console.WriteLine("Error: your guess must be no shorter or longer than 5 letters!");
                i--;
                continue;
            }

            if (guess == secretWord)
            {
                Console.WriteLine($"You guessed it right! The secret word was {secretWord}!");
                break;
            }

            for(int j=0; j<5; j++)
            {
                if (guess[j] == secretWord[j])
                {
                    //if letter on the right place
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(guess[j]);
                }
                else if (secretWord.Contains(guess[j]))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(guess[j]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(guess[j]);
                }
            }

            Console.ResetColor();
            Console.WriteLine();
        }
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