using System;
using System.Collections.Generic;
using System.Linq;

namespace trivia
{
    public class Deck
    {
        public Category Category { get; }

        private readonly Stack<string> questions = new Stack<string>();

        public Deck(Category category)
        {
            Category = category;
            
            this.questions = new Stack<string>(
                Enumerable.Range(0, 50)
                    .Select(i => $"{Category.Name} Question {i}"));
        }

        public string PickQuestion() => questions.Pop();


    }
}
