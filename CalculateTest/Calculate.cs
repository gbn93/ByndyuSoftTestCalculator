using Calculate.Infrastructure;
using CalculateTest.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculateTest
{
    class Calculate
    {
        private readonly IParser _parser;
        private readonly IResolve _resolve;

        public Calculate(IParser parser, IResolve resolve)
        {
            _parser = parser;
            _resolve = resolve;
        }

        public decimal Calculated(string expression)
        {
            Lexem[] lexem = _parser.Parse(expression);
            if (lexem.Length == 0)
                throw new CalculateException("Пустое выражение");

            ICommand[] commands = _resolve.Compile(lexem);

            var stack = new Stack<decimal>();
            foreach (var c in commands)
                c.Calculate(stack);

            if (stack.Count == 0)
                throw new CalculateException("В выражении не хватает чисел");

            if (stack.Count > 1)
                throw new CalculateException("В выражении не хватает операторов");

            return stack.Pop();
        }
    }
}
