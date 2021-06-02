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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private Members
        //holds the current results of the cells in the active game
        private MarkType[] mResults;

        //True if its player 1's turn (X) or player 2's turn (O)
        private bool mPlayer1Turn;


        //True if the game has ended
        private bool mGameEnded;

        #endregion
        #region Constructor
        //Default Constructor 
        public MainWindow()
        {
            InitializeComponent();

            NewGame();

        }
        #endregion 
        private void NewGame()
        {
            //Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //Make sure Player 1 starts the game
            mPlayer1Turn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;

            });
            //Make sure game hasnt finished 
            mGameEnded = false;
        }

        //Handles button click event
        //param name sender = the button that was clicked
        // param name e = events of the click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Start a new gme on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            //cast the sender to a button
            var button = (Button)sender;

            //Find the button position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            //Dont do anything if the cell has a value already
            if (mResults[index] != MarkType.Free)
                return;

            //set the cel value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            //change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle the players turns
            mPlayer1Turn ^= true;

            //check for a winner
            CheckForWinner();
        }
        private void CheckForWinner()
        {
            #region Horizontal Wins
            //Check for horizontal winners
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            //Row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            //Row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion


            #region Vertical Wins
            //Check for vertical winners
            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            #endregion

            #region Diagonal Wins
            //Check for diagonal winners
            // Top Left Bottom Right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            //Top right to Bottom Left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //Game end
                mGameEnded = true;
                //Highlight winner cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }
            #endregion
            #region No Winners
            //check for no winner and full board
            if ( !mResults.Any(result => result == MarkType.Free))
            {
                //Game ended
                mGameEnded = true;

                // Turn all cell orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    
                    button.Background = Brushes.Orange;
                   
                });
            }
                #endregion
        }
          
              


    }
}
