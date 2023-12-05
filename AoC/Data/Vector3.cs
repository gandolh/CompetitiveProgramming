using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Data
{
    internal class Vector3<T1> : MyTriple<T1, T1, T1>
    {
        public Vector3(T1 first, T1 second, T1 third) : base(first, second, third)
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector3<T1> vector &&
                   base.Equals(obj) &&
                   EqualityComparer<T1>.Default.Equals(First, vector.First) &&
                   EqualityComparer<T1>.Default.Equals(Second, vector.Second) &&
                   EqualityComparer<T1>.Default.Equals(Third, vector.Third);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), First, Second, Third);
        }
    }
}
