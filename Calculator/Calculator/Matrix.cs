using System;

namespace Calculator
{
    class Matrix<T> where T : Complex, new()
    {
        public T[][] Data { get; private set; }
        public int Size { get; private set; } //будем работать только с квадратными матрицами

        public Matrix(T[][] data)
        {
            Size = 0;
            //если входной массив не квадратный, дополним его нулями до квадратного
            //для этого сначала найдем его размерность:
            foreach (var t in data)
                if (t.Length > Size)
                    Size = t.Length;

            Data = new T[Size][];
            for (int i = 0; i < Size; ++i)
                Data[i] = new T[Size];
            for (int i = 0; i < Size; ++i)
                for (int j = 0; j < Size; ++j)
                    if (i >= data.Length || j >= data[i].Length)
                        Data[i][j] = new T();
                    else
                        Data[i][j] = data[i][j];
        }

        public Matrix(int size)
        {
            Size = size;
            Data = new T[Size][];
            for (int i = 0; i < Size; ++i)
                Data[i] = new T[Size];
            for (int i = 0; i < Size; ++i)
                for (int j = 0; j < Size; ++j)
                    Data[i][j] = new T();
        }

        public void Print()
        {
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    Console.Write("{0} ", Data[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
        {
            var result = new Matrix<T>(a.Size);
            for (int i = 0; i < result.Size; ++i)
                for (int j = 0; j < result.Size; ++j)
                {
                    //
                    result.Data[i][j] = (T) (a.Data[i][j] + b.Data[i][j]);
                }
            return result;
        }
    }
}
