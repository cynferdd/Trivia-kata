using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Deck
    {
        public string Category { get; }

        private readonly Stack<string> questions = new Stack<string>();

        public Deck(string categoryName)
        {
            Category = categoryName;
            
            this.questions = new Stack<string>(
                Enumerable.Range(0, 50)
                    .Select(i => $"{Category} Question {i}"));
        }

        public string PickQuestion() => questions.Pop();


    }
}
