using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace trivia
{
    public class Category
    {
        public string Name { get; }

        public Category(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj) => 
            obj is Category category && 
            Name == category.Name;

        public override int GetHashCode() => 
            363513814 + EqualityComparer<string>.Default.GetHashCode(Name);

        public override string ToString() => Name;
    }
}
