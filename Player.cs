using System;
using System.Collections.Generic;
using System.Text;

namespace trivia
{
    public class Player
    {
        public string Name { get; }

        public int Purse { get; private set; }

        public bool IsInPenaltyBox { get; set; }

        public int Position { get; set; }

        public Player(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Name = name;
        }

        public void IncreasePurse()
        {
            Purse++;
        }
    }
}
