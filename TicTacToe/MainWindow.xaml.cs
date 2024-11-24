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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isPlayerTurn = true;
        private int playerScore = 0;
        private int computerScore = 0;
        private int drawScore = 0;
        

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreBoard();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            playerScore = 0;
            computerScore = 0;
            drawScore = 0;
            UpdateScoreBoard();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button.IsEnabled)
            {
                if (isPlayerTurn)
                {
                    button.Foreground = Brushes.Green;
                    button.Content = "X";
                    button.IsEnabled = false;
                    isPlayerTurn = false;

                    CheckGameStatus();

                    ComputerTurn();
                }
            }
        }

        private void ComputerTurn()
        {
            var buttons = new [] {Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8, Btn9};
            System.Random random = new System.Random();
            var randomButtons = buttons.OrderBy(x => random.Next()).ToArray();

            foreach (var button in randomButtons)
            {
                if (button.IsEnabled) 
                {
                    button.Foreground = Brushes.Red;
                    button.Content = "O";
                    button.IsEnabled = false;
                    isPlayerTurn = true;

                    CheckGameStatus();
                    break;
                }
            }
        }

        private void CheckGameStatus() 
        {
            var buttons = new[] { Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8, Btn9 };
            string winner = null;

            //Kontrollerar rader
            if(IsWinningCombination(Btn1, Btn2, Btn3)) winner = GetButtonContent(Btn1);
            else if(IsWinningCombination(Btn4, Btn5, Btn6)) winner = GetButtonContent(Btn4);
            else if(IsWinningCombination(Btn7, Btn8, Btn9)) winner = GetButtonContent(Btn7);

            //Kontrollerar kolumner
            else if(IsWinningCombination(Btn1, Btn4, Btn7)) winner = GetButtonContent(Btn1);
            else if(IsWinningCombination(Btn2, Btn5, Btn8)) winner = GetButtonContent(Btn2);
            else if(IsWinningCombination(Btn3, Btn6, Btn9)) winner = GetButtonContent(Btn3);

            //Kontrollerar diagonaler
            else if (IsWinningCombination(Btn1, Btn5, Btn9)) winner = GetButtonContent(Btn1);
            else if (IsWinningCombination(Btn3, Btn5, Btn7)) winner = GetButtonContent(Btn3);

            if(winner != null)
            {
                if(winner == "X")
                {
                    MessageBox.Show("Du har vunnit! Snyggt jobbat!", "Vinnare", MessageBoxButton.OK, MessageBoxImage.Information);
                    playerScore += 1;
                    UpdateScoreBoard();
                    ResetGame();
                }
                else
                {
                    MessageBox.Show("Datorn har vunnit! Bättre lycka nästa gång", "Vinnare", MessageBoxButton.OK, MessageBoxImage.Information);
                    computerScore += 1;
                    UpdateScoreBoard();
                    ResetGame();
                }
            }

            bool isDraw = true;
            foreach(var button in buttons)
            {
                if (button.IsEnabled)
                {
                    isDraw = false;
                    break;
                }
            }

            if (isDraw)
            {
                MessageBox.Show("Spelet är oavgjort!", "Oavgjort", MessageBoxButton.OK, MessageBoxImage.Information);
                drawScore += 1;
                UpdateScoreBoard();
                ResetGame();
            }
        }

        private bool IsWinningCombination(Button b1, Button b2, Button b3) 
        { 
            return b1.Content == b2.Content && b2.Content == b3.Content && !b1.IsEnabled; 
        }

        private string GetButtonContent(Button button) 
        { 
            if (button.Content == "X")
            {
                return "X";
            }
            else
            {
                return "O";
            } 
        }
        private void ResetGame() 
        { 
            var buttons = new[] { Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8, Btn9 }; 
            foreach (var button in buttons) 
            {  
                button.IsEnabled = true;
                button.Content = null;
            } 
            isPlayerTurn = true; 
        }

        private void UpdateScoreBoard()
        {
            scoreBoard.Text = $"Poäng: Du {playerScore} | Datorn {computerScore} | Oavgjorda {drawScore}";
        }
    }
}