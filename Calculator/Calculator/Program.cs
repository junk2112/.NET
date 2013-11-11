using System;


namespace Calculator
{
    class Program
    {
        static void Main()
        {
            //try
            //{
            //    string input;
            //    while ((input = Console.ReadLine()) != "exit")
            //        new Calculator(input);
            //}
            //catch (IndexOutOfRangeException)
            //{
            //    Console.WriteLine("Incorrect input");
            //}

            string str;
            var matrix = new Complex[2][];
            matrix[0] = new Complex[2];
            matrix[1] = new Complex[2];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    str = Console.ReadLine();
                    double a, b;
                    double.TryParse(str, out a);
                    str = Console.ReadLine();
                    double.TryParse(str, out b);
                    matrix[i][j] = new Complex(a, b);
                }
            var m = new Matrix<Complex>(matrix);
            var result = m + m;
            result.Print();
        }
    }
}
