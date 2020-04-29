using System;
using System.Collections.Generic;
using System.Text;

namespace Trivia
{
    public interface IGameVisitor
    {
        void PlayerCreation(string playerName, int playerNumber);


        void CurrentPlayerPenaltyBoxState(string playerName, bool stayInPenaltyBox);

        void CurrentPlayerRoll(string playerName, int roll);


        void MovePlayer(string playerName, int playerPosition, string category);


        void AskQuestion(string question);


        void CorrectAnswer(string playerName, int playerPurse);


        void WrongAnswer(string playerName);
    }
}
