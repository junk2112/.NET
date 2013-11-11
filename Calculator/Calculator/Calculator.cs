using System;
using System.Globalization;

namespace Calculator
{
    class Calculator
    {
        private readonly string[] _polishNotation;
        private readonly string[] _parsed;
        private static string[] TryToParse(string input)
        {
            int length = 1, start = 0;
            var temp = new string[input.Length];
            var i = 0;
            while (start <= input.Length - 1)
            {
                double x;
                //если не вышли за пределы строки и распарсили, то пробуем парсить строку на символ длиннее
                //или, если символ перед start есть '(', то, возможно, символ в start есть унарный знак
                if (((start + length) <= input.Length) && (double.TryParse(input.Substring(start, length), out x) ||
                    ((start != 0) && ((input[start - 1]) == '(')) && (length == 1) || 
                    ((start == 0) && (length == 1)) || 
                    (input[start + length - 1] == '.') ) )
                {
                    length++;
                }
                else
                {
                    if (length > 1)
                    {
                        if (start + length != input.Length || input[input.Length - 1] == ')')
                        {
                            temp[i] = input.Substring(start, length - 1);
                            i++;
                            start += length - 1;
                        }
                        else
                        {
                            temp[i] = input.Substring(start, length);
                            break;
                        }
                    }
                    else
                    {
                        temp[i++] = input.Substring(start, length);
                        start += length;
                    }
                    length = 1;
                }
            }
            return temp;
        }

        private string[] ToPolish()
        {
            var output = new string[_parsed.Length];
            int i = 0;
            var stack = new Stack<string>();
            foreach (var str in _parsed)
            {
                if (str == null)
                    break;
                double x;
                //почему-то дробные числа записанные через точку - не парсит
                //если писать через запятую - все в порядке
                if (double.TryParse(str, out x))
                {
                    output[i++] = str;
                }
                else
                {
                    if (str == "(")
                    {
                        stack.Push(str);
                    }
                    else
                        if (str == ")")
                        {
                            while (stack.Top() != "(")
                                output[i++] = stack.Pop();
                            stack.Pop();
                        }
                        else
                            if (str == "*" || str == "/" || str == "+" || str == "-" || str == "^")
                            {
                                while (!stack.Empty() && GetPriority(str) <= GetPriority(stack.Top()))
                                {
                                    output[i++] = stack.Pop();
                                }
                                stack.Push(str);
                            }
                }
            }
            while (!stack.Empty())
            {
                output[i++] = stack.Pop();
            }

            return output;
        }

        public int GetPriority (string str) 
        {
            //возвращает приоритет операции
            //для числа приоритет равен -1
            int result;
            switch (str)
            {
                case "+":
                case "-":
                    result = 0;
                    break;
                case "*":                   
                case "/":
                    result = 1;
                    break;
                case "^":
                    result = 2;
                    break;
                default:
                    result = -1;
                    break;
            }
            return result;
        }
        private double GetResult()
        {
            var stack = new Stack<string>();
            //обрабатываем массив 
            foreach (var str in _polishNotation)
            {
                if (str == null)
                    break;
                if (GetPriority(str) == -1)
                    stack.Push(str);
                else
                    if (str == "*" || str == "/" || str == "+" || str == "-" || str == "^")
                    {
                        double first = 0, second = 0;
                        double.TryParse(stack.Pop(), out second);
                        double.TryParse(stack.Pop(), out first);
                        switch (str)
                        {
                            case "+":
                                stack.Push((first + second).ToString(CultureInfo.InvariantCulture));
                                break;
                            case "-":
                                stack.Push((first - second).ToString(CultureInfo.InvariantCulture));
                                break;
                            case "*":
                                stack.Push((first * second).ToString(CultureInfo.InvariantCulture));
                                break;
                            case "/":
                                stack.Push((first / second).ToString(CultureInfo.InvariantCulture));
                                break;
                            case "^":
                                stack.Push(Math.Pow(first, second).ToString(CultureInfo.InvariantCulture));
                                break;
                        }
                    }
                //Console.WriteLine(stack.top());
            }
            double result;
            if ((stack.GetSize() != 1) || !double.TryParse(stack.Top(), out result))
            {
                //Console.WriteLine("Incorrect Input");
                throw new IndexOutOfRangeException();
            }
            return result;
        }

        public Calculator (string input)
        {
            _parsed = TryToParse(input);
            _polishNotation = ToPolish();
            /*foreach (string str in polishNotation)
            {
                if (str == null)
                    break;
                Console.WriteLine(str);
            }*/
            Console.WriteLine("Result is {0}", GetResult());
        }
    }
}
