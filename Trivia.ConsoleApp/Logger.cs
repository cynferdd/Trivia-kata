using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia.ConsoleApp
{
    public class Logger : IGameVisitor
    {
        public void PlayerCreation(string playerName, int playerNumber)
        {
            Console.WriteLine(playerName + " was Added");
            Console.WriteLine("They are player number " + playerNumber);
        }

        public void CurrentPlayerPenaltyBoxState(string playerName, bool stayInPenaltyBox)
        {
            Console.WriteLine(
                                playerName +
                                " is " +
                                (stayInPenaltyBox ? "not" : "") +
                                "getting out of the penalty box");
        }

        public void CurrentPlayerRoll(string playerName, int roll)
        {
            Console.WriteLine(playerName + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
        }
        public void MovePlayer(string playerName, int playerPosition, string category)
        {
            Console.WriteLine(playerName
                            + "'s new location is "
                            + playerPosition);
            Console.WriteLine("The category is " + category);
        }
        public void AskQuestion(string question)
        {
            Console.WriteLine(question);
        }

        public void CorrectAnswer(string playerName, int playerPurse)
        {
            Console.WriteLine("Answer was correct!!!!");
            Console.WriteLine(playerName
                    + " now has "
                    + playerPurse
                    + " Gold Coins.");
        }

        public void WrongAnswer(string playerName)
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(playerName + " was sent to the penalty box");
        }

        
    }
}
