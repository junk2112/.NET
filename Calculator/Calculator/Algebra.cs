
namespace Calculator
{
    internal abstract class IAlgebra<T>
    {

        public abstract T Umn(T a, T b);
        public abstract T Dev(T a);
        public abstract T Min(T a);
        public abstract T Pow(T a);
    }
}
