using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Calculator 
{
    class Real : IComparable, IEquatable<Real>, IEquatable<double>//, IAlgebra<Real>
    {
        public double X { get; private set; }

        public static implicit operator Real(double x)
        {
            return new Real(x);
        }
        

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            return obj.GetType() == GetType() && Equals((Real) obj);
        }

        public bool Equals(Real other)
        {
            return Math.Abs(X - other.X) < Double.Epsilon;
        }

        public bool Equals(double other)
        {
            return Math.Abs(X - other) < Double.Epsilon;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode();
        }

        public Real()
        {
            X = 0;
        }

        public Real(double x)
        {
            X = x;
        }

        public Real(Real x)
        {
            X = x.X;
        }

        public override string ToString()
        {
            return X.ToString(CultureInfo.InvariantCulture);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            if (X > ((Real)obj).X)
                return 1;
            if (X < ((Real) obj).X)
                return -1;
            return 0;
        }

        public static Boolean operator < (Real a, Real b)
        {
            return a.X < b.X;
        }

        public static Boolean operator > (Real a, Real b)
        {
            return a.X > b.X;
        }

        public static Boolean operator == (Real a, object b)
        {
            return Equals(a, b);
        }

        public static Boolean operator != (Real a, object b)
        {
            return Equals(a, b);
        }

        public static Real Sum(Real a, Real b)
        {
            return new Real(a.X + b.X);
        }

        public static Real operator +(Real a, Real b)
        {
            return Sum(a, b);
        }

        public static Real Umn(Real a, Real b)
        {
            return new Real(a.X * b.X);
        }

        public static Real operator *(Real a, Real b)
        {
            return Umn(a, b);
        }

        public static Real Dev(Real a, Real b)
        {
            return new Real(a.X / b.X);
        }

        public static Real operator /(Real a, Real b)
        {
            return Dev(a, b);
        }

        public static Real Min(Real a, Real b)
        {
            return new Real(a.X - b.X);
        }

        public static Real operator -(Real a, Real b)
        {
            return Min(a, b);
        }

        public static Real Pow(Real a, Real b)
        {
            return new Real(Math.Pow(a.X, b.X));
        }

        public static Real operator ^(Real a, Real b)
        {
            return Pow(a, b);
        }
    }
}
