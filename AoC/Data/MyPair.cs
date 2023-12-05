using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Data
{
    internal class MyPair<T1,T2>
    {
        public T1 first;
        public T2 second;

        public MyPair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public override bool Equals(object? obj)
        {
            return obj is MyPair<T1, T2> pair &&
                   EqualityComparer<T1>.Default.Equals(first, pair.first) &&
                   EqualityComparer<T2>.Default.Equals(second, pair.second);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(first, second);
        }
    }
}
