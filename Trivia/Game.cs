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

        

        private readonly CircularIterator<Deck> gameBoard = new CircularIterator<Deck>();


        public Player GetPlayerStatus(int i) => playersStatus[i];

        private Game(IReadOnlyList<string> players, DeckCollection decks)
        {
            
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

        public static IDeckBuilder OfTwoPlayers(string player1, string player2) => 
            new Builder(player1, player2);
        public static IDeckBuilder OfThreePlayers(string player1, string player2, string player3) => 
            new Builder(player1, player2, player3);
        public static IDeckBuilder OfFourPlayers(string player1, string player2, string player3, string player4) => 
            new Builder(player1, player2, player3, player4);
        public static IDeckBuilder OfFivePlayers(string player1, string player2, string player3, string player4, string player5) => 
            new Builder(player1, player2, player3, player4, player5);
        public static IDeckBuilder OfSixPlayers(string player1, string player2, string player3, string player4, string player5, string player6) => 
            new Builder(player1, player2, player3, player4, player5, player6);

        public interface IBuilder
        {
            

            Game Build();
        }

        

        public interface IDeckBuilder: IBuilder
        {
            IBuilder Decks(DeckCollection deckCollection);
        }


        private class Builder : IBuilder, IDeckBuilder
        {
            private DeckCollection deckCollection = new DeckCollection();
            IReadOnlyList<string> players;

            public Builder(params string[] players)
            {
                this.players = players;
            }
            public IBuilder Decks(DeckCollection deckCollection)
            {
                this.deckCollection = deckCollection;
                return this;
            }

            public Game Build()
            {
                return new Game(players, this.deckCollection);
            }

            
        }
    }

}
