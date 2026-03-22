using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WorldDesktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    int currentAttempt = 0;
    string secretWord = "OCEAN";

    public MainWindow()
    {
        InitializeComponent();
        CreateGrid();
        secretWord = GetRandomWord();
        this.Title = "Wordle answer: " + secretWord; //DEBUG
        InputBox.Focus();
    }
    
    //Method to create grid for letters
    private void CreateGrid()
    {
        GameGrid.Children.Clear();
        for(int i=0; i<30; i++)
        {
            Border card = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(26, 27, 36)),
                BorderThickness = new Thickness(2),
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Color.FromRgb(237, 239, 250))
            };

            card.Child = new TextBlock
            {
                Text = "",
                Foreground = Brushes.White,
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            GameGrid.Children.Add(card);
        }
    }

    //inputting letters to the grid boxes
    private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        string text = InputBox.Text.ToUpper();
        int startIndex = currentAttempt * 5;

        for(int i=0; i<5; i++)
            {
                Border card = (Border)GameGrid.Children[startIndex + i];
                TextBlock txt = (TextBlock)card.Child;

                if(i<text.Length)
                {
                    txt.Text = text[i].ToString();
                    card.BorderBrush = Brushes.Black;
                    txt.Foreground = Brushes.Black;
                }
                else
                {
                    txt.Text = "";
                    card.BorderBrush = new SolidColorBrush(Color.FromRgb(58,58,60));
                }
            }
    }

    //checking letters which was inputted
    private void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        string guess = InputBox.Text.ToUpper();

        //checking the length
        if (guess.Length != 5)
        {
            MessageBox.Show("Error: your guess must be no shorter or longer than 5 letters!");
            return;
        }

        //look at what index raw in grid starts
        int startIndex = currentAttempt * 5;
        
        for(int i=0; i<5; i++)
        {
            Border card = (Border)GameGrid.Children[startIndex + i];
            TextBlock txt = (TextBlock)card.Child;

            txt.Text = guess[i].ToString();

            //colouring letters
            if(guess[i] == secretWord[i])
            {
                card.Background = Brushes.Green;
                txt.Foreground = Brushes.White;
            }
            else if(secretWord.Contains(guess[i]))
            {
                card.Background = Brushes.Gold;
                txt.Foreground = Brushes.Black;
            }
            else
            {
                card.Background = Brushes.DimGray;
                txt.Foreground = Brushes.LightGray;
            }
        }

        //check if player won, lost or still guessing
        if(guess == secretWord)
        {
            EndMessage.Text = "You Won!";
            EndMessage.Foreground = Brushes.Gold;
            EndMessage.Visibility = Visibility.Visible;
            CheckButton.IsEnabled = false;
            return;
        }
        else if(currentAttempt >=5 )
        {
            EndMessage.Text = $"You lost!\nSecret word was {secretWord}";
            EndMessage.Foreground = Brushes.OrangeRed;
            EndMessage.Visibility = Visibility.Visible;
            CheckButton.IsEnabled = false;
        }
        else
        {
            currentAttempt++;
            InputBox.Clear();
        }
    }

    //check button works when user uses enter button
    private void InputBox_KeyDown(object sender, KeyEventArgs e)
    {
        if(e.Key == Key.Enter)
        {
            CheckButton_Click(this, new RoutedEventArgs());
        }
    }

    //Mehod to protect coursor from going away from the input field
    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseDown(e);
        InputBox.Focus();
    }

    //getting random word from the list
    private string GetRandomWord()
    {
        try
        {
            if(!File.Exists("words.txt"))
            {
                MessageBox.Show("File wasn't find! Default word will be used!");
                return "RAVEN";
            }

            string[] words = File.ReadAllLines("words.txt");

            Random rnd = new Random();
            return words[rnd.Next(words.Length)].ToUpper().Trim();
        }
        catch
        {
            return "RAVEN";
        }
    }
}