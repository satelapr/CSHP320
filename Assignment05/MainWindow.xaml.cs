using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum cellValue
        {
            empty,
            X,
            O
        }

        private bool player1TurnFlag;
        private cellValue[] gameBoard;

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        private void uxNewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void uxExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                string selectedButtonTag = button.Tag.ToString();
                if (!string.IsNullOrWhiteSpace(selectedButtonTag))
                {
                    List<int> cellRowColumnValues = selectedButtonTag.Split(',').Select(i => int.Parse(i)).ToList();
                    int row = cellRowColumnValues[0];
                    int column = cellRowColumnValues[1];

                    var index = column + (row * 3);

                  
                    if (player1TurnFlag && string.IsNullOrWhiteSpace(button.Content.ToString()))
                    {
                        gameBoard[index] = cellValue.X;
                        button.Content = "X";
                        player1TurnFlag = false;
                        uxTurn.Text = "O's turn";
                        button.IsEnabled = false;
                    }
                    else if (string.IsNullOrWhiteSpace(button.Content.ToString()))
                    {
                        gameBoard[index] = cellValue.O;
                        button.Content = 'O';
                        player1TurnFlag = true;
                        uxTurn.Text = "X's turn";
                        button.IsEnabled = false;
                    }
                    CheckForWinner();
                }
            }

        }

        private void NewGame()
        {
            gameBoard = new cellValue[9];

            for (var i = 0; i < gameBoard.Length; i++)
                gameBoard[i] = cellValue.empty;

            // Start of the game - First Player turn
            player1TurnFlag = true;

            foreach (Button button in uxGrid.Children)
            {
                button.Content = string.Empty;
                button.IsEnabled = true;
            }
        }

        private void CheckForWinner()
        {
            if (gameBoard[0] != cellValue.empty && (gameBoard[0] & gameBoard[1] & gameBoard[2]) == gameBoard[0])
            {
                PerformWinnerActions();
            }
            if (gameBoard[3] != cellValue.empty && (gameBoard[3] & gameBoard[4] & gameBoard[5]) == gameBoard[3])
            {
                PerformWinnerActions();
            }
            if (gameBoard[6] != cellValue.empty && (gameBoard[6] & gameBoard[7] & gameBoard[8]) == gameBoard[6])
            {
                PerformWinnerActions();
            }
            if (gameBoard[0] != cellValue.empty && (gameBoard[0] & gameBoard[4] & gameBoard[8]) == gameBoard[0])
            {
                PerformWinnerActions();
            }
            if (gameBoard[2] != cellValue.empty && (gameBoard[2] & gameBoard[4] & gameBoard[6]) == gameBoard[2])
            {
                PerformWinnerActions();
            }
            if (gameBoard[0] != cellValue.empty && (gameBoard[0] & gameBoard[3] & gameBoard[6]) == gameBoard[0])
            {
                PerformWinnerActions();
            }
            if (gameBoard[1] != cellValue.empty && (gameBoard[1] & gameBoard[4] & gameBoard[7]) == gameBoard[1])
            {
                PerformWinnerActions();
            }
            if (gameBoard[2] != cellValue.empty && (gameBoard[2] & gameBoard[5] & gameBoard[8]) == gameBoard[2])
            {
                PerformWinnerActions();
            }
        }

        private string ReturnWinnerMessage()
        {
            return player1TurnFlag ? "O is a winner" : "X is a winner";
        }

        private void PerformWinnerActions()
        {
            uxTurn.Text = ReturnWinnerMessage();
            foreach (Button button in uxGrid.Children)
            {
                button.IsEnabled = false;
            }
        }
    }
}
