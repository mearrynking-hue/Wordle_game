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
            //getting random word from the list
            string secretWord = RandomWord("words.txt");
            Console.WriteLine($"[DEBUG] Hidden word is: {secretWord}");
            Console.WriteLine("I thought of a 5-letter word. Can you guess it?");

            //loop for checking letters in the answeers
            for (int i=1; i<=6; i++)
            {
                Console.WriteLine($"Attempt {i} of 6: ");
                string guess = (Console.ReadLine() ?? "").ToUpper();

                //check number of letters in the answer
                if (guess.Length<5 || guess.Length>5)
                {
                    Console.WriteLine("Error: your guess must be no shorter or longer than 5 letters!");
                    i--;
                    continue;
                }

                //check if answer is a secret word
                if (guess == secretWord)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"You guessed it right! The secret word was {secretWord}!");
                    hasWon = true;
                    Console.ResetColor();
                    break;
                }

                //check if letter in the answer on the right place or in the answer, but on the wrong place
                for(int j=0; j<5; j++)
                {
                    //if letter on the right place
                    if (guess[j] == secretWord[j])
                    {   
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(guess[j]);
                    }
                    //if letter on the wron place
                    else if (secretWord.Contains(guess[j]))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(guess[j]);
                    }
                    //if letter not in the secret word
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(guess[j]);
                    }
                }

                Console.ResetColor();
                Console.WriteLine();
            }

            //check if player has won
            if (!hasWon)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nGame over! You've run out of attemts!");
                Console.WriteLine($"The secret word was {secretWord}");
                Console.ResetColor();
            }

            //asking player if they want to continue
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

        //if file is not empty, then get a random word
        Random rand = new Random();
        int index = rand.Next(words.Length);

        return words[index].Trim().ToUpper();
    }

}