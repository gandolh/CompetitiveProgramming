using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;

namespace AoC.Data
{
    internal class Vector2<T1> 
    {
        public T1 first { get; set; }
        public T1 second { get; set; }

        public Vector2(T1 first, T1 second)
        {
            this.first = first;
            this.second = second;
        }

        private Vector2()
        {
        }

        public Vector2(Vector2<T1> v)
        {
            this.first = v.first;
            this.second = v.second;
        }

        public T1 this[int key]
        {
            get => GetValue(key);
        }

        private T1 GetValue(int key)
        {
            if (key == 0) return first;
            if (key == 1) return second;
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2<T1> vector &&
                   EqualityComparer<T1>.Default.Equals(first, vector.first) &&
                   EqualityComparer<T1>.Default.Equals(second, vector.second);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(first, second);
        }

        public static Vector2<T1> operator +(Vector2<T1> vector1, Vector2<T1> vector2)
        {
            dynamic first = vector1.first;
            dynamic second = vector1.second;
            dynamic otherFirst = vector2.first;
            dynamic otherSecond = vector2.second;

            return new Vector2<T1>(first + otherFirst, second + otherSecond);
        }

    }
}
