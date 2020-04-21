
using Xunit;


namespace Trivia.Test
{
    public class GameTest
    {
        
        

        [Fact]
        public void ShouldGetOutOfPenaltyBox_WhenRollIsOddAndAnswerCorrect()
        {
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            // Isabelle
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            // Patrick
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            Assert.False(game.GetPlayerStatus(0).IsInPenaltyBox);
            

        }

        [Fact]
        public void ShouldGainCoin_WhenAnswerIsCorrect()
        {
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();

            Assert.Equal(1, game.GetPlayerStatus(0).Purse);
        }

        [Fact]
        public void ShouldGainCoin_WhenFirstAnswerIsWrongAndSecondAnswerIsCorrect()
        {
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            // Isabelle
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            // Patrick
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();

            Assert.Equal(1, game.GetPlayerStatus(0).Purse);
            Assert.Equal(1, game.GetPlayerStatus(1).Purse);
        }

        [Fact]
        public void ShouldHavePopCategory_WhenRoll4()
        {
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Four);
            var actual = game.CurrentCategory();

            Assert.Equal("Pop", actual);
        }

        [Fact]
        public void ShouldHaveScienceCategory_WhenRoll1()
        {
            
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.One);
            var actual = game.CurrentCategory();

            Assert.Equal("Science", actual);
        }

        [Fact]
        public void ShouldHaveSportCategory_WhenRoll2()
        {
            
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Two);
            var actual = game.CurrentCategory();

            Assert.Equal("Sports", actual);
        }
        [Fact]
        public void ShouldHaveRockCategory_WhenRoll3()
        {
            
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Three);
            var actual = game.CurrentCategory();

            Assert.Equal("Rock", actual);
        }

        [Fact]
        public void ShouldBeOutOfPenaltyBox_WhenRoll1AndOutOfPenaltyBox()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            Assert.False(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeOutOfPenaltyBox_WhenRoll2AndOutOfPenaltyBox()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.Two);
            Assert.False(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeOutOfPenaltyBox_WhenCorrectlyAnsweredAndOutOfPenaltyBox()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            Assert.False(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeInPenaltyBox_WhenWronglyAnsweredAndOutOfPenaltyBox()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            Assert.True(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeInPenaltyBox_WhenRoll1AndInPenaltyBoxAndWrongAnswer()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            Assert.True(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeOutOfPenaltyBox_WhenRoll1AndInPenaltyBoxAndCorrectAnswer()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            Assert.False(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

        [Fact]
        public void ShouldBeOutOfPenaltyBox_WhenRoll2AndInPenaltyBox()
        {
            Game game = new Game("Patrick", "Isabelle");
            game.Roll(DiceRoll.One);
            game.WasWronglyAnswered();
            game.Roll(DiceRoll.One);
            game.WasCorrectlyAnswered();
            game.Roll(DiceRoll.Two);
            Assert.True(game.GetPlayerStatus(0).IsInPenaltyBox);
        }

    }
}
