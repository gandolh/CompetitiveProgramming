using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Data
{
    internal class MyTriple<T1, T2,T3>
    {
        public T1 First { get; set; }
        public T2 Second { get; set; }
        public T3 Third { get; set; }

        public MyTriple(T1 first, T2 second, T3 third)
        {
            First = first;
            Second = second;
            Third = third;
        }

        public override bool Equals(object? obj)
        {
            return obj is MyTriple<T1, T2, T3> triple &&
                   EqualityComparer<T1>.Default.Equals(First, triple.First) &&
                   EqualityComparer<T2>.Default.Equals(Second, triple.Second) &&
                   EqualityComparer<T3>.Default.Equals(Third, triple.Third);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second, Third);
        }


    }
}
