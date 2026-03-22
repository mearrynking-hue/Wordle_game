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

namespace WorldDesktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CreateGrid();
    }
    
    //Method to create grid for letters
    private void CreateGrid()
    {
        GameGrid.Children.Clear();
        for(int i=0; i<30; i++)
        {
            Border card = new Border
            {
                BorderBrush = new SolidColorBrush(Color.FromRgb(58,58,60)),
                BorderThickness = new Thickness(2),
                Margin = new Thickness(3),
                Background = new SolidColorBrush(Color.FromRgb(18,18,19))
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

    // Method which works when you click on the button
    private void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        string guess = InputBox.Text.ToUpper();

        //checking the length
        if (guess.Length != 5)
        {
            MessageBox.Show("Error: your guess must be no shorter or longer than 5 letters!");
            return;
        }

        InputBox.Clear();
    }
}