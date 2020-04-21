using System;
using System.IO;
using System.Text;
using Xunit;
using Assent;
using Assent.Reporters;
using Assent.Reporters.DiffPrograms;
using trivia;

namespace Trivia
{
    public class TriviaTests
    {
        [Fact]
        public void RefactoringTests()
        {
            //var output = new StringBuilder();
            //Console.SetOut(new StringWriter(output));

            //Game aGame = new Game();
            //Console.WriteLine(aGame.IsPlayable());
            //aGame.Add("Chet");
            //aGame.Add("Pat");
            //aGame.Add("Sue");

            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);
            //aGame.Roll(DiceRoll.One);

            //aGame.WasCorrectlyAnswered();
            //aGame.WrongAnswer();

            //aGame.Roll(2);

            //aGame.Roll(6);

            //aGame.WrongAnswer();

            //aGame.Roll(2);

            //aGame.Roll(2);


            //aGame.WrongAnswer();

            //aGame.WasCorrectlyAnswered();
            //aGame.Roll(DiceRoll.One);
            //aGame.WasCorrectlyAnswered();

            //var configuration = BuildConfiguration();
            //this.Assent(output.ToString(), configuration);
        }

        

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
            Category pop = new Category("Pop");
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Four);
            Category actual = game.CurrentCategory();

            Assert.Equal(pop, actual);
        }

        [Fact]
        public void ShouldHaveScienceCategory_WhenRoll1()
        {
            Category science = new Category("Science");
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.One);
            Category actual = game.CurrentCategory();

            Assert.Equal(science, actual);
        }

        [Fact]
        public void ShouldHaveSportCategory_WhenRoll2()
        {
            Category sport = new Category("Sports");
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Two);
            Category actual = game.CurrentCategory();

            Assert.Equal(sport, actual);
        }
        [Fact]
        public void ShouldHaveRockCategory_WhenRoll3()
        {
            Category rock = new Category("Rock");
            Game game = new Game("Patrick", "Isabelle");
            // Patrick
            game.Roll(DiceRoll.Three);
            Category actual = game.CurrentCategory();

            Assert.Equal(rock, actual);
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

        private static Configuration BuildConfiguration()
        {
            return 
                new Configuration()
                
            // Uncomment this block if an exception 
            // « Could not find a diff program to use »
            // is thrown and if you have VsCode installed.
            // Otherwise, use other DiffProgram with its full path
            // as parameter.
            // See  https://github.com/droyad/Assent/wiki/Reporting
//                    .UsingReporter(
//                        new DiffReporter(
//                            new []
//                            {
                                // For linux
//                                new VsCodeDiffProgram(new []
//                                {
//                                    "/usr/bin/code"
//                                })
                
                                // For Windows
//                                new VsCodeDiffProgram(), 
//                            }))
                ;
        }
    }
}
