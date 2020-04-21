using System;
using System.Collections.Generic;
using System.Linq;
using trivia;

namespace Trivia
{
    public class Game
    {
        public static void Main() { }

        private const int VictoryPurseAmount = 6;
        private static readonly Category pop = new Category("Pop");
        private static readonly Category science = new Category("Science");
        private static readonly Category sports = new Category("Sports");
        private static readonly Category rock = new Category("Rock");

        



        private readonly CircularIterator<Player> playersStatus = new CircularIterator<Player>();

        private readonly Deck popDeck = new Deck(pop);
        private readonly Deck scienceDeck = new Deck(science);
        private readonly Deck sportsDeck = new Deck(sports);
        private readonly Deck rockDeck = new Deck(rock);

        private readonly IReadOnlyDictionary<Category,Deck> deckByCategory;

        private readonly CircularIterator<Category> gameBoard = new CircularIterator<Category>();


        public Player GetPlayerStatus(int i) => playersStatus[i];

        private Game(IReadOnlyList<string> players)
        {

            var decks = new[] { popDeck, scienceDeck, sportsDeck, rockDeck };
            deckByCategory = decks.ToDictionary(d => d.Category);

            gameBoard =
                Enumerable
                    .Repeat(decks.Select(d => d.Category), 3)
                    .Flatten()
                    .ToCircular();

            playersStatus = players.Select(p => new Player(p)).ToCircular();

            for (int i = 0; i < playersStatus.Count; i++)
            {

                Console.WriteLine(playersStatus[i].Name + " was Added");
                Console.WriteLine("They are player number " + i);
            }

        }

        public Game(string player1, string player2)
            :this(new[] { player1, player2}) { }


        public Game(string player1, string player2, string player3)
            : this(new[] { player1, player2, player3 }) { }

        public Game(string player1, string player2, string player3, string player4)
            : this(new[] { player1, player2, player3, player4 }) { }

        public Game(string player1, string player2, string player3, string player4, string player5)
            : this(new[] { player1, player2, player3, player4, player5 }) { }

        public Game(string player1, string player2, string player3, string player4, string player5, string player6)
            : this(new[] { player1, player2, player3, player4, player5, player6 }){}

        
        

        public void Roll(DiceRoll diceRoll)
        {
            int roll = (int)diceRoll;
            Console.WriteLine(playersStatus.Current.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            var stayInPenaltyBox = roll % 2 == 0;
            bool isInPenaltyBox = playersStatus.Current.IsInPenaltyBox;

            if (isInPenaltyBox)
            {
                Console.WriteLine(
                    playersStatus.Current.Name +
                    " is " +
                    (stayInPenaltyBox ? "not" :"" ) +
                    "getting out of the penalty box");

                playersStatus.Current.IsInPenaltyBox = stayInPenaltyBox;

            }

            if (!isInPenaltyBox || !stayInPenaltyBox)
            {
                Move(roll);

                AskQuestion();
            }

        }


        private void Move(int roll)
        {
            playersStatus.Current.Position = gameBoard.GetIndex(roll + playersStatus.Current.Position);
            

            Console.WriteLine(playersStatus.Current.Name
                                        + "'s new location is "
                                        + playersStatus.Current.Position);
            Console.WriteLine("The category is " + CurrentCategory());
        }

        private void AskQuestion()
        {
            Console.WriteLine(
                deckByCategory[CurrentCategory()]
                .PickQuestion());
        }


        public Category CurrentCategory()
        {
            return gameBoard[playersStatus.Current.Position];
        }

        public bool WasCorrectlyAnswered()
        {
            bool continueGame = true;

            if (!playersStatus.Current.IsInPenaltyBox)
            {
                CorrectAnswerStatement();
                continueGame = ShouldContinueGame();

            }
            
            GoToNextPlayer();
            return continueGame;

            
        }

        private void CorrectAnswerStatement()
        {
            Console.WriteLine("Answer was correct!!!!");

            playersStatus.Current.IncreasePurse();

            Console.WriteLine(playersStatus.Current.Name
                    + " now has "
                    + playersStatus.Current.Purse
                    + " Gold Coins.");
        }

        public bool WasWronglyAnswered()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(playersStatus.Current.Name + " was sent to the penalty box");
            playersStatus.Current.IsInPenaltyBox = true;
            GoToNextPlayer();
            return true;
        }

        private void GoToNextPlayer()
        {
            playersStatus.Move(1);
        }

        private bool ShouldContinueGame()
        {
            return !(playersStatus.Current.Purse == VictoryPurseAmount);
        }

        
    }

}
