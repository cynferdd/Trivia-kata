using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Trivia
{
    public class DeckCollection :IReadOnlyCollection<Deck>
    {
        private readonly Deck popDeck = new Deck("Pop");
        private readonly Deck scienceDeck = new Deck("Science");
        private readonly Deck sportsDeck = new Deck("Sports");
        private readonly Deck rockDeck = new Deck("Rock");

        public List<Deck> Decks = new List<Deck>();

        public DeckCollection()
        {
            Decks.Add(popDeck);
            Decks.Add(scienceDeck);
            Decks.Add(sportsDeck);
            Decks.Add(rockDeck);
            
        }

        public int Count => Decks.Count;

        public IEnumerator<Deck> GetEnumerator()
        {
            return Decks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
