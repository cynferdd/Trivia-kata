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

        private List<Deck> Decks = new List<Deck>();

        public DeckCollection()
        {
            Decks.Add(popDeck);
            Decks.Add(scienceDeck);
            Decks.Add(sportsDeck);
            Decks.Add(rockDeck);
            
        }

        public DeckCollection(string category)
            :this()
        {
            Decks.Add(new Deck(category));
        }

        public DeckCollection(string firstCategory, string secondCategory)
            : this(firstCategory)
        {
            Decks.Add(new Deck(secondCategory));
        }

        public DeckCollection(string firstCategory, string secondCategory, string thirdCategory)
            : this(firstCategory, secondCategory)
        {
            Decks.Add(new Deck(thirdCategory));
        }

        public DeckCollection(string firstCategory, string secondCategory, string thirdCategory, string fourthCategory)
            : this(firstCategory, secondCategory, thirdCategory)
        {
            Decks.Add(new Deck(fourthCategory));
        }

        public DeckCollection(string firstCategory, string secondCategory, string thirdCategory, string fourthCategory, string fifthCategory)
            : this(firstCategory, secondCategory, thirdCategory, fourthCategory)
        {
            Decks.Add(new Deck(fifthCategory));
        }

        public DeckCollection(string firstCategory, string secondCategory, string thirdCategory, string fourthCategory, string fifthCategory, string sixthCategory)
            : this(firstCategory, secondCategory, thirdCategory, fourthCategory, fifthCategory)
        {
            Decks.Add(new Deck(sixthCategory));
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
