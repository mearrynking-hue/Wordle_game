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
    }
    
    // Mether which works when you click on the button
    private void CheckButton_Click(object sender, RoutedEventArgs e)
    {
        string guess = InputBox.Text.ToUpper();

        //checking the length
        if (guess.Length != 5)
        {
            MessageBox.Show("Error: your guess must be no shorter or longer than 5 letters!");
            return;
        }

        MessageBox.Show($"You typed in: {guess}");
        
        InputBox.Clear();
    }
}