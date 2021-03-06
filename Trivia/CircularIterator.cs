﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class CircularIterator<T>
    {
        private readonly IReadOnlyList<T> source;

        

        public T Current => this.source[index];
        public T this[int i] => this.source[i % Count];

        private int index;

        public int Count => this.source.Count;
        public CircularIterator(params T[] source)
            :this(source.ToList())
        {

        }

        public CircularIterator(IReadOnlyList<T> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.index = 0;
        }

        public void Move(int step)
        {
            this.index = (index + step) % Count;
        }

        public int GetIndex(int i)
        {
            return i % Count;
        }

    }
}
