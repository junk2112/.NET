using System;
using System.Globalization;

namespace Calculator
{
    class Complex : IEquatable<Complex>
    {
        public double Real { get; private set; }
        public double Im { get; private set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) 
                return false;
            if (ReferenceEquals(this, obj)) 
                return true;
            return GetType() == obj.GetType() && Equals((Complex) obj);
        }

        public bool Equals(Complex other)
        {
            return Math.Abs(Real - other.Real) < Double.Epsilon && Math.Abs(Im - other.Im) < Double.Epsilon;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Real.GetHashCode()*397) ^ Im.GetHashCode();
            }
        }

        public static explicit operator Complex(double real)
        {
            return new Complex(real, 0);
        }

        public static explicit operator Complex(int real)
        {
            return new Complex(real, 0);
        }

        public static implicit operator Complex(Real real)
        {
            return new Complex(real.X, 0);
        }

        public Complex()
        {
            Real = 0;
            Im = 0;
        }

        public Complex(double real, double im)
        {
            Real = real;
            Im = im;
        }

        public Complex(Complex c)
        {
            Real = c.Real;
            Im = c.Im;
        }

        public Complex(Real c)
        {
            Real = c.X;
            Im = 0;
        }

        public override string ToString()
        {
            if (double.IsNaN(Real) || double.IsNaN(Im))
                return "NaN";
            return "(" + Real.ToString(CultureInfo.InvariantCulture) + ")+(" + Im.ToString(CultureInfo.InvariantCulture) + ")i";
        }

        public double Module(Complex c)
        {
            if (c != null)
                return Math.Pow(c.Real*c.Real + c.Im*c.Im, 0.5);
            throw new Exception();
        }

        public static Complex operator + (Complex a, Complex b)
        {
            return new Complex(a.Real + b.Real, a.Im + b.Im);
        }

        public static Complex operator - (Complex a, Complex b)
        {
            return new Complex(a.Real - b.Real, a.Im - b.Im);
        }

        public static Complex operator * (Complex a, Complex b)
        {
            return new Complex(a.Real * b.Real - a.Im * b.Im, a.Real * b.Im + a.Im * b.Real);
        }

        public static Complex operator / (Complex a, Complex b)
        {
            return new Complex((a.Real*b.Real + a.Im*b.Im) / (b.Real*b.Real + b.Im*b.Im),
                               (a.Im*b.Real - a.Real*b.Im) / (b.Real*b.Real + b.Im*b.Im));
        }

        public static Complex operator ^ (Complex a, Real b)
        {
            var result = new Complex(a);
            for (int i = 1; i < b.X; ++i)
            {
                result *= a;
            }
            return result;
        }

        public static Boolean operator == (Complex a, object b)
        {
            return Equals(a, b);
        }

        public static Boolean operator != (Complex a, object b)
        {
            return Equals(a, b);
        }
    }
}
