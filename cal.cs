using System;
using System.Globalization;
using System.Linq;

namespace Program
{
    class Calculator
    {
        private static readonly NumberFormatInfo numberFormatInfo = new NumberFormatInfo { NumberDecimalSeparator = "." };
        private double memory = 0;
        private double curval = 0;
        private static readonly string[] memoryOperations = { "M+", "M-", "MR" };

        public void Start()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("ERROR: invalid input");
                    continue;
                }
                var tokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 3)
                {
                    if (!TryParseDouble(tokens, out double left) || !TryParseDouble(tokens, out double right))
                    {
                        Console.WriteLine("ERROR: invalid input");
                        continue;
                    }

                    switch (tokens)
                    {
                        case "+":
                            curval = left + right;
                            break;
                        case "-":
                            curval = left - right;
                            break;
                        case "*":
                            curval = left * right;
                            break;
                        case "/":
                            if (right == 0)
                            {
                                Console.WriteLine("ERROR: division by zero");
                                continue;
                            }
                            curval = left / right;
                            break;
                        case "%":
                            curval = left % right;
                            break;
                        case "^":
                            curval = Math.Pow(left, right);
                            break;
                        default:
                            Console.WriteLine("ERROR: invalid input");
                            continue;
                    }
                    Console.WriteLine(curval.ToString(numberFormatInfo));
                }
                else if (tokens.Length == 2 && tokens == "sqrt" && TryParseDouble(tokens, out double val))
                {
                    Console.WriteLine(Math.Sqrt(val).ToString(numberFormatInfo));
                }
                else if (tokens.Length == 1 && memoryOperations.Contains(tokens))
                {
                    switch (tokens)
                    {
                        case "M+":
                            memory += curval;
                            break;
                        case "M-":
                            memory -= curval;
                            break;
                        case "MR":
                            Console.WriteLine(memory.ToString(numberFormatInfo));
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: invalid input");
                }
            }
        }

        private static bool TryParseDouble(string str, out double result)
        {
            return double.TryParse(str, NumberStyles.Float, numberFormatInfo, out result);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            new Calculator().Start();
        }
    }
}
