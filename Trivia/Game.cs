using System;
using System.Collections.Generic;
using System.Linq;
using Trivia;

namespace Trivia
{
    public class Game
    {
        public static void Main() { }

        private const int VictoryPurseAmount = 6;

        private IGameVisitor visitor;
        

        private readonly CircularIterator<Player> playersStatus = new CircularIterator<Player>();

        private readonly Deck popDeck = new Deck("Pop");
        private readonly Deck scienceDeck = new Deck("Science");
        private readonly Deck sportsDeck = new Deck("Sports");
        private readonly Deck rockDeck = new Deck("Rock");

        private readonly CircularIterator<Deck> gameBoard = new CircularIterator<Deck>();


        public Player GetPlayerStatus(int i) => playersStatus[i];

        private Game(IReadOnlyList<string> players)
        {
            DeckCollection decks = new DeckCollection();
            

            gameBoard =
                Enumerable
                    .Repeat(decks, 3)
                    .Flatten()
                    .ToCircular();

            playersStatus = players.Select(p => new Player(p)).ToCircular();

            for (int i = 0; i < playersStatus.Count; i++)
            {
                visitor?.PlayerCreation(playersStatus[i].Name, i);
            }

        }

        
        public void Accept(IGameVisitor logger)
        {
            this.visitor = logger;
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
            visitor?.CurrentPlayerRoll(playersStatus.Current.Name, roll);

            var stayInPenaltyBox = roll % 2 == 0;
            bool isInPenaltyBox = playersStatus.Current.IsInPenaltyBox;

            if (isInPenaltyBox)
            {
                visitor?.CurrentPlayerPenaltyBoxState(playersStatus.Current.Name, stayInPenaltyBox);

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
            Player current = playersStatus.Current;
            current.Position = gameBoard.GetIndex(roll + current.Position);

            visitor?.MovePlayer(current.Name, current.Position, gameBoard[current.Position].Category);
        }

        

        private void AskQuestion()
        {
            string question = gameBoard[playersStatus.Current.Position].PickQuestion();
            visitor?.AskQuestion(question);
        }

        

        public string CurrentCategory()
        {
            return gameBoard[playersStatus.Current.Position].Category;
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

            playersStatus.Current.IncreasePurse();
            visitor?.CorrectAnswer(playersStatus.Current.Name, playersStatus.Current.Purse);
        }

        

        public bool WasWronglyAnswered()
        {
            visitor?.WrongAnswer(playersStatus.Current.Name);
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
