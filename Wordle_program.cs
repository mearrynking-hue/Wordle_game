using System;
using System.IO;

class WordleProgram
{
    static void Main()
    {
        //start of the game
        Console.WriteLine("==================\nWELCOME TO WORDLE\n==================");

        bool keepPlaying = true;
        bool hasWon = false;

        while(keepPlaying)
        {
            string secretWord = RandomWord("words.txt");
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
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"You guessed it right! The secret word was {secretWord}!");
                    hasWon = true;
                    Console.ResetColor();
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

            if (!hasWon)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nGame over! You've run out of attemts!");
                Console.WriteLine($"The secret word was {secretWord}");
                Console.ResetColor();
            }

            Console.WriteLine("Do you want to play again? (Y/N): ");
            string answer = Console.ReadLine()?.ToUpper() ?? "";

            if(answer != "Y")
            {
                keepPlaying = false;
                Console.WriteLine("Thank you for playing!");
            }
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