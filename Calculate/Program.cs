using Calculate.Infrastructure;
using CalculateTest.Abstractions;
using CalculateTest.Abstractions.Operation;
using System;
using System.Globalization;

namespace CalculateTest
{
    public class Program
    {
        public static void Main()
        {
            Calculate calc = getCalculator();

            while (true)
            {
                Console.Write("Введите выражение: ");
                string expression = Console.ReadLine();
                if (expression == "exit")
                    return;

                try
                {
                    decimal result = calc.Calculated(expression);
                    Console.WriteLine("Результат: " + result.ToString(CultureInfo.InvariantCulture));
                }
                catch (CalculateException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine();
            }
        }

        private static Calculate getCalculator()
        {
            var lexer = new Parser();
            var operations = new IOperation[]
            {
                new PlusOperation(),
                new MinusOperation(),
                new MultiplyOperation(),
                new DivideOperation()
            };

            var compiler = new Compiler(operations);
            return new Calculate(lexer, compiler);
        }
    }
}
